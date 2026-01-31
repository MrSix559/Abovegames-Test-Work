using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Tools.EventBus
{
    [HideMonoScript]
    public class BusOutputColor : MonoBehaviour
    {
        [SerializeField] private string _key;
        [SerializeField, FoldoutGroup("Event")] private UnityEvent<Color> _onInvoke;

        private void OnEnable() => EBus.Subscribe<Color>(_key, OnInvoke);
        private void OnDisable() => EBus.UnSubscribe<Color>(_key, OnInvoke);
        private void OnInvoke(Color value) => _onInvoke?.Invoke(value);
    }
}