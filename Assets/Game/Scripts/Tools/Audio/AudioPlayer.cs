using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;

namespace Tools
{
    [HideMonoScript]
    public class AudioPlayer : MonoBehaviour
    {
        private enum LoadType
        {
            Addressable,
            Local
        }
        #region LoadType
        [SerializeField, BoxGroup("Load Type", false)] private LoadType _loadType;
        [SerializeField, BoxGroup("Load Type"), ShowIf("_loadType", LoadType.Addressable), HideIf("_loadType", LoadType.Local)] private string _id;
        [SerializeField, BoxGroup("Load Type"), ShowIf("_loadType", LoadType.Local), HideIf("_loadType", LoadType.Addressable)] private AudioClip _clip;
        #endregion
        #region Volume
        [SerializeField, BoxGroup("Volume"), Range(0, 1)] private float _volume;
        #endregion
        #region Custom Pith
        [SerializeField, BoxGroup("Custom Pith", false)] private bool _enabledCustomPith;
        [SerializeField, BoxGroup("Custom Pith"), ShowIf(nameof(_enabledCustomPith)), MinValue(-3), MaxValue(3)] private float _minPith = -3;
        [SerializeField, BoxGroup("Custom Pith"), ShowIf(nameof(_enabledCustomPith)), MinValue(-3), MaxValue(3)] private float _maxPith = 3;
        #endregion
        #region Pitch
        [SerializeField, BoxGroup("Pitch", false)] private bool _pitch;
        [SerializeField, BoxGroup("Pitch"), MinMaxSlider(nameof(_minPith), nameof(_maxPith)), ShowIf(nameof(_pitch))] private Vector2 _pitchRange;
        #endregion
        [SerializeField, BoxGroup("Play on awake", false)] private bool _playAwake;
        [SerializeField, BoxGroup("Loop", false)] private bool _loop;
        private PoolComponents<AudioSource> _pool;

        private void Awake()
        {
            if (_loadType == LoadType.Local) InitializeLocal();
            if (_loadType == LoadType.Addressable) InitializeAddressable().Forget();
            if(_playAwake) Play();
        }
        private void InitializeLocal() => CreatePool(_clip);
        private async UniTask InitializeAddressable()
        {
            try
            {
                _clip = await Addressables.LoadAssetAsync<AudioClip>(_id).ToUniTask();
                CreatePool(_clip);
            }
            catch(Exception e)
            {
                Debug.LogError($"AudioPlayer failed to load addressable: {e}");
            }
        }

        private void CreatePool(AudioClip clip)
        {
            var prefab = new GameObject(name: clip.name).AddComponent<AudioSource>();
            prefab.clip = clip;
            prefab.loop = _loop;
            prefab.volume = _volume;
            _pool = new PoolComponents<AudioSource>(prefab, transform);
        }
        public async void Play()
        {
            if (_pool == null)
            {
                Debug.LogError("AudioPlayer: Pool not initialized yet");
                return;
            }

            var source = _pool.Get();
            if (_pitch) source.pitch = UnityEngine.Random.Range(_pitchRange.x, _pitchRange.y);
            source.Play();
            if (!source.loop) await ReturnToPoolWhenFinished(source, source.GetCancellationTokenOnDestroy());
        }
        private async UniTask ReturnToPoolWhenFinished(AudioSource source, CancellationToken token)
        {
            if(source == null) return;
            try
            {
                await UniTask.WaitUntil(() => !source.isPlaying, cancellationToken: token);
                _pool.Release(source);
            }
            catch (OperationCanceledException) {}
        }
        public void SetVolume(float volume)
        {
            foreach(AudioSource s in _pool.GetAll())
                s.volume = volume;
        }
        private void OnDestroy()
        {
            if (_pool != null)
                _pool.Clear();

            if (_loadType == LoadType.Addressable && _clip != null)
            {
                Addressables.Release(_clip);
                _clip = null;
            }
        }
    }
}