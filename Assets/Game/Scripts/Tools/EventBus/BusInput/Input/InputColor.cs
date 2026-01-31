using Sirenix.OdinInspector;
using UnityEngine;

namespace Tools.EventBus
{
    [HideMonoScript]
    public class BusInputColor : MonoBehaviour
    {
        [SerializeField] private string _key;
        public void InvokeBus(Color value) => EBus.Invoke<Color>(_key, value);
    }
}