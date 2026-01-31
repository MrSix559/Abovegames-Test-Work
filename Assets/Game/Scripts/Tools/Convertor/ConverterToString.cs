using UnityEngine;
using UnityEngine.Events;

namespace Tools
{
    public class ConverterToString : MonoBehaviour
    {
        [SerializeField] private UnityEvent<string> _onConverted;

        public void ConvertFloatToString(float value)
        {
            _onConverted?.Invoke(value.ToString("0.00"));
        }

        public void ConvertIntToString(int value)
        {
            _onConverted?.Invoke(value.ToString());
        }
    }
}