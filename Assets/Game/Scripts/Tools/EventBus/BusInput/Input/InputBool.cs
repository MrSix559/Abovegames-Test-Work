using Sirenix.OdinInspector;
using UnityEngine;

namespace Tools.EventBus
{
    [HideMonoScript]
    public class BusInputBool : MonoBehaviour
    {
        [SerializeField] private string _key;
        public void InvokeBus(bool value) => EBus.Invoke<bool>(_key, value);
    }
}