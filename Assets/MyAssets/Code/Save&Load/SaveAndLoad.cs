using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;

public class SaveAndLoad
{
    private const float _timeToSave = 30;

    public static bool IsSavingData = false;

    [SerializeField] private Button _saveButton;

    private float _timeToSaveCounter = _timeToSave;

    private Action OnSaved;

    private void OnApplicationQuit() => SavePlayerData();

    public void AutoSave()
    {
        if (_timeToSaveCounter > 0)
        {
            _timeToSaveCounter -= Time.deltaTime;
        }
        else
        {
            _timeToSaveCounter = _timeToSave;
            SavePlayerData();
        }
    }

    public void SavePlayerData()
    {
        SaveBinaryPlayerData();
    }

    private void SaveBinaryPlayerData()
    {
        SaveSystem.SavePlayerData(); //MoneyMenu 

        if (IsSavingData == false)
        {
            Debug.Log("SaveBinaryPlayerData Complete");
        }
    }

    public void CheckBinarySave()
    {
        if (File.Exists(SaveSystem.Path) != false)
        {
            Debug.Log("LoadIsCorrect, path = " + SaveSystem.Path);
            LoadPlayerData();
        }
        else Debug.Log("Save file not found in" + SaveSystem.Path);
    }

    private void LoadPlayerData()
    {
        Debug.Log("LOAD_1");

        //if (CheckNull(data.FirstLaunchScene))
        //    Game.FirstLaunchScene = data_1.FirstLaunchScene;

    }

    private bool CheckNull(object obj)
    {
        if (obj != null)
            return true;
        return false;
    }
}