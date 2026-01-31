using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Tools.EventBus
{
    [HideMonoScript]
    public class BusOutputInt : MonoBehaviour
    {
        [SerializeField] private string _key;
        [SerializeField, FoldoutGroup("Event")] private UnityEvent<int> _onInvoke;

        private void OnEnable() => EBus.Subscribe<int>(_key, OnInvoke);
        private void OnDisable() => EBus.UnSubscribe<int>(_key, OnInvoke);
        private void OnInvoke(int value) => _onInvoke?.Invoke(value);
    }
}