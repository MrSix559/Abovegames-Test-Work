using Sirenix.OdinInspector;
using UnityEngine;

namespace Tools.EventBus
{
    [HideMonoScript]
    public class BusInputString : MonoBehaviour
    {
        [SerializeField] private string _key;
        public void InvokeBus(string value) => EBus.Invoke<string>(_key, value);
    }
}
