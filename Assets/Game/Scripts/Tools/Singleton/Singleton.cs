using UnityEngine;

namespace Tools
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        private static bool _isInitialized = false;

        public static T Instance
        {
            get
            {
                if (_instance == null && !_isInitialized)
                {
                    _instance = FindFirstObjectByType<T>();
                    _isInitialized = true;
                }

                return _instance;
            }
        }

        public virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
            }
            else if (_instance != this)
            {
                Debug.LogWarning($"[Singleton] ������ ��������� {typeof(T)} ������ � ����� ���������.");
                Destroy(gameObject);
            }
        }
    }
}