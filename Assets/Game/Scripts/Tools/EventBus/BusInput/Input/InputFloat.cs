using Sirenix.OdinInspector;
using UnityEngine;

namespace Tools.EventBus
{
    [HideMonoScript]
    public class BusInputFloat : MonoBehaviour
    {
        [SerializeField] private string _key;
        public void InvokeBus(float value) => EBus.Invoke<float>(_key, value);
    }
}
