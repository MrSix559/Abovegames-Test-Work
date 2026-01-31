using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tools
{
    public class PoolObjects<T> where T : MonoBehaviour
    {
        private T _prefab;
        private Transform _parent;
        private Queue<T> _objects;

        public PoolObjects(T prefab, Transform parent = null, int prewarmObjects = 5)
        {
            _prefab = prefab;
            _parent = parent;
            _objects = new Queue<T>();

            for (int i = 0; i < prewarmObjects; i++)
            {
                var obj = GameObject.Instantiate(_prefab, parent);
                obj.gameObject.SetActive(false);
                _objects.Enqueue(obj);
            }
        }

        public T Get()
        {
            T obj;

            for (int i = 0; i < _objects.Count; i++)
            {
                obj = _objects.Dequeue();
                if (obj == null) continue;
                _objects.Enqueue(obj);
                if (!obj.gameObject.activeSelf)
                {
                    obj.gameObject.SetActive(true);
                    return obj;
                }
            }

            obj = Create();
            _objects.Enqueue(obj);
            obj.gameObject.SetActive(true);
            return obj;
        }

        public void Release(T obj)
        {
            obj.gameObject.SetActive(false);
        }

        private T Create()
        {
            return GameObject.Instantiate(_prefab, _parent);
        }
    }
}
