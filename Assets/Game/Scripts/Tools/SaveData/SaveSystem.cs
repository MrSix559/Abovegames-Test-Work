using Tools.SaveData;
using UnityEngine;

public static class SaveSystem
{
    public static T Load<T>(string key, T defaultValue = default) where T : class
    {
        if (!typeof(T).IsSerializable)
        {
            Debug.LogWarning($"This type must be Serializable <{typeof(T).FullName}>");
            return defaultValue;
        }
        if (!EncryptedPlayerPrefs.HasEncryptedKey(key) && defaultValue != default)
            Save(key, defaultValue);
        return JsonUtility.FromJson<T>(EncryptedPlayerPrefs.GetEncryptedString(key, default));
    }

    public static void Save<T>(string key, T obj) where T : class
    {
        if(!typeof(T).IsSerializable)
        {
            Debug.LogWarning($"This type must be Serializable <{typeof(T).FullName}>");
            return;
        }
        EncryptedPlayerPrefs.SetEncryptedString(key, JsonUtility.ToJson(obj));
    }
}
