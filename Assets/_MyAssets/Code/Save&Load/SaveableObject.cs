using UnityEngine;

public class SaveableObject : MonoBehaviour
{
    /// <summary>
    /// Уникальное имя объекта (для идентификации в базе данных)
    /// </summary>
    private string Id => this.gameObject.GetHashCode().ToString();

    private void Awake()
    {
        this.SubscribeToEvents();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SaveData();
        }
    }

    private void OnDestroy()
    {
        this.UnsubscribeFromEvents();
    }

    private void SubscribeToEvents()
    {
        SaveLoadSystem.OnSave += SaveData;
        SaveLoadSystem.OnLoad += LoadData;
    }

    private void UnsubscribeFromEvents()
    {
        SaveLoadSystem.OnSave -= SaveData;
        SaveLoadSystem.OnLoad -= LoadData;
    }

#if UNITY_WEBGL

    public void SaveData()
    {
        var data = new ObjectData(this.Id, this.transform, this.transform.rotation, this.transform.localScale);
        var jsonData = JsonUtility.ToJson(data);
        LocalStorageManager.SaveData(this.Id, jsonData);
    }

    public void LoadData()
    {
        string jsonData = LocalStorageManager.LoadData(this.Id);
        if (string.IsNullOrEmpty(jsonData))
            return;

        var data = JsonUtility.FromJson<ObjectData>(jsonData);
        var position = new Vector3(data.posX, data.posY, data.posZ);
        var quaternion = new Quaternion(data.rotX, data.rotY, data.rotZ, data.rotW);
        var scale = new Vector3(data.scaleX, data.scaleY, data.scaleZ);
        this.transform.position = position;
        this.transform.rotation = quaternion;
        this.transform.localScale = scale;
    }

#elif UNITY_STANDALONE

    private async void SaveData()
    {
        var data = new ObjectData(this.Id, this.transform.position, this.transform.rotation, this.transform.localScale);
        await SaveLoadSystem.Instance.SaveObjectDataAsync(data);
    }
        
    private async void LoadData()
    {
        await SaveLoadSystem.Instance.LoadObjectDataAsync(this.Id, (data) =>
        {
            if (data != null)
            {
                transform.position = data.Position;
                transform.rotation = data.Rotation;
                transform.localScale = data.Scale;
            }
        });
    }

#endif
}