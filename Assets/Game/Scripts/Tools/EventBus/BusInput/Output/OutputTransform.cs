using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Tools.EventBus
{
    [HideMonoScript]
    public class BusOutputTransform : MonoBehaviour
    {
        [SerializeField] private string _key;
        [SerializeField, FoldoutGroup("Event")] private UnityEvent<Transform> _onInvoke;

        private void OnEnable() => EBus.Subscribe<Transform>(_key, OnInvoke);
        private void OnDisable() => EBus.UnSubscribe<Transform>(_key, OnInvoke);
        private void OnInvoke(Transform value) => _onInvoke?.Invoke(value);
    }
}