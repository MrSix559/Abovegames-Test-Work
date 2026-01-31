using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Tools.EventBus
{
    [HideMonoScript]
    public class BusOutputBool : MonoBehaviour
    {
        [SerializeField] private string _key;
        [SerializeField, FoldoutGroup("Event")] private UnityEvent<bool> _onInvoke;

        private void OnEnable() => EBus.Subscribe<bool>(_key, OnInvoke);
        private void OnDisable() => EBus.UnSubscribe<bool>(_key, OnInvoke);
        private void OnInvoke(bool value) => _onInvoke?.Invoke(value);
    }
}