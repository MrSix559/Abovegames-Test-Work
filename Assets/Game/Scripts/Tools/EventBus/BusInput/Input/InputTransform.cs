using Sirenix.OdinInspector;
using UnityEngine;

namespace Tools.EventBus
{
    [HideMonoScript]
    public class BusInputTransform : MonoBehaviour
    {
        [SerializeField] private string _key;
        public void InvokeBus(Transform value) => EBus.Invoke<Transform>(_key, value);
    }
}
