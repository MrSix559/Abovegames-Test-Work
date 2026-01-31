using Sirenix.OdinInspector;
using UnityEngine;

namespace Tools.EventBus
{
    [HideMonoScript]
    public class BusInputSimple : MonoBehaviour
    {
        [SerializeField] private string _key;
        public void InvokeBus() => EBus.Invoke(_key);
    }
}
