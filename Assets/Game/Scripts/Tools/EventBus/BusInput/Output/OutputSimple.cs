using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Tools.EventBus
{
    [HideMonoScript]
    public class BusOutputSimple : MonoBehaviour
    {
        [SerializeField] private string _key;
        [SerializeField, FoldoutGroup("Event")] private UnityEvent _onInvoke;

        private void OnEnable() => EBus.Subscribe(_key, OnInvoke);
        private void OnDisable() => EBus.UnSubscribe(_key, OnInvoke);
        private void OnInvoke() => _onInvoke?.Invoke();
    }
}