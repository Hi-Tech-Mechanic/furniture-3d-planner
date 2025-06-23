using System.Runtime.InteropServices;
using UnityEngine;

/// <summary>
/// Сохранения через JS для web платформы
/// </summary>
public class LocalStorageManager
{
    // Подключаем JS функции
    [DllImport("__Internal")]
    private static extern void SaveToLocalStorage(string key, string value);

    [DllImport("__Internal")]
    private static extern void LoadFromLocalStorage(string key, System.IntPtr valuePtr, int maxLength);

    public static void SaveData(string key, string data)
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        SaveToLocalStorage(key, data);
#else
        PlayerPrefs.SetString(key, data);
#endif
    }

    public static string LoadData(string key)
    {
#if !UNITY_EDITOR && UNITY_WEBGL

        System.IntPtr ptr = Marshal.AllocHGlobal(1024); // Буфер на 1024 символа
        LoadFromLocalStorage(key, ptr, 1024);
        string result = Marshal.PtrToStringAuto(ptr);
        Marshal.FreeHGlobal(ptr);
        return result;

#else

        return PlayerPrefs.GetString(key, "");

#endif
    }
}