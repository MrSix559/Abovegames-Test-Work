using System.Collections;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Tools
{
    [HideMonoScript]
    public class FpsShow : MonoBehaviour
    {
        [SerializeField, Required, BoxGroup("Global Fps Settings")] private TextMeshProUGUI _fpsText;
        [SerializeField, BoxGroup("Global Fps Settings")] private float _updateInterval = 0.5f;
        [SerializeField] private FpsColorRule[] _fpsColorRules;

        private UnityEngine.Coroutine _fpsCoroutine;

        private void OnEnable()
        {
            _fpsCoroutine = StartCoroutine(UpdateFps());
        }

        private void OnDisable()
        {
            if(_fpsCoroutine != null )
                StopCoroutine(_fpsCoroutine);
        }

        private IEnumerator UpdateFps()
        {
            WaitForSecondsRealtime delay = new WaitForSecondsRealtime(_updateInterval);
            while(true)
            {
                float fps = 1.0f / Time.unscaledDeltaTime;
                _fpsText.text = $"FPS: {Mathf.RoundToInt(fps)}";
                ApplyColorFps(fps);
                yield return delay;
            }
        }

        private void ApplyColorFps(float currentFps)
        {
            foreach (var fpsRules in _fpsColorRules)
            {
                if(fpsRules.IsInRange(currentFps))
                {
                    _fpsText.color = fpsRules.ColorFps;
                    break;
                }
            }
        }
        public void SetEnabledText(bool status) => _fpsText.gameObject.SetActive(status);
    }

    [System.Serializable]
    public struct FpsColorRule
    {
        public float max;
        public Color ColorFps;

        public bool IsInRange(float fps)
        {
            return fps <= max;
        }
    }
}
