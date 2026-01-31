using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Tools.EventBus
{
    [HideMonoScript]
    public class BusOutputString : MonoBehaviour
    {
        [SerializeField] private string _key;
        [SerializeField, FoldoutGroup("Event")] private UnityEvent<string> _onInvoke;

        private void OnEnable() => EBus.Subscribe<string>(_key, OnInvoke);
        private void OnDisable() => EBus.UnSubscribe<string>(_key, OnInvoke);
        private void OnInvoke(string value) => _onInvoke?.Invoke(value);
    }
}