using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static string Path => Application.persistentDataPath + "/data_1.fun";

    public static Action OnSaved;

    public static void SavePlayerData() //MoneyMenu moneyMenu
    {
        BinaryFormatter formatter = new();
        FileStream stream = new(Path, FileMode.Create);

        //PlayerData data = new();

        //formatter.Serialize(stream, data);
        SaveAndLoad.IsSavingData = true;
        stream.Close();

        if (stream.CanWrite == false)
        {
            Debug.Log("SavePlayerData_1 Complete");
            SaveAndLoad.IsSavingData = false;
            OnSaved?.Invoke();
        }
    }

    public static PlayerData LoadPlayer()
    {
        BinaryFormatter formatter = new();
        FileStream stream = new(Path, FileMode.Open);

        PlayerData data = formatter.Deserialize(stream) as PlayerData;

        stream.Close();
        return data;
    }
}