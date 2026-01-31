using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Tools.EventBus
{
    [HideMonoScript]
    public class BusOutputFloat : MonoBehaviour
    {
        [SerializeField] private string _key;
        [SerializeField, FoldoutGroup("Event")] private UnityEvent<float> _onInvoke;

        private void OnEnable() => EBus.Subscribe<float>(_key, OnInvoke);
        private void OnDisable() => EBus.UnSubscribe<float>(_key, OnInvoke);
        private void OnInvoke(float value) => _onInvoke?.Invoke(value);
    }
}