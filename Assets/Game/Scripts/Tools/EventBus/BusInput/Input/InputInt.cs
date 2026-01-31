using Sirenix.OdinInspector;
using UnityEngine;

namespace Tools.EventBus
{
    [HideMonoScript]
    public class BusInputInt : MonoBehaviour
    {
        [SerializeField] private string _key;
        public void InvokeBus(int value) => EBus.Invoke<int>(_key, value);
    }
}