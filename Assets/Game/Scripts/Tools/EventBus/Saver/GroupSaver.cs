using UnityEngine;
using System.Collections.Generic;

namespace Tools.EventBus
{
    public static class GroupSaver
    {
        private static readonly Dictionary<string, object> _groups = new();
        public static void Register<T>(string key, T defaultValue = default)
        {
            if (_groups.ContainsKey(key))
                return;

            T data;

            if (PlayerPrefs.HasKey(key))
                data = JsonUtility.FromJson<T>(PlayerPrefs.GetString(key));
            else
            {
                data = defaultValue;
                Save(key, data);
            }

            _groups[key] = data;

            EBus.Subscribe<T>(key, value =>
            {
                _groups[key] = value;
                Save(key, value);
            });
        }
        public static T Get<T>(string key)
        {
            return _groups.TryGetValue(key, out var data)
                ? (T)data
                : default;
        }
        public static void Save<T>(string key, T value)
        {
            PlayerPrefs.SetString(key, JsonUtility.ToJson(value, false));
            PlayerPrefs.Save();
        }
    }
}
