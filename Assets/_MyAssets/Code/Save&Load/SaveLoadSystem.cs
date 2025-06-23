using UnityEngine;
using UnityEngine.UI;
using System;
using System.Threading.Tasks;
using LiteDB;
using System.IO;
using System.Collections;
using DG.Tweening;
using FurnitureShop;

/// <summary>
/// Вызывает событие загрузки при старте и сохраняет,
/// как автоматически так и с помощью кнопки
/// </summary>
public class SaveLoadSystem : MonoBehaviour
{
#if UNITY_STANDALONE

    private const string collectionName = "objects";
    private string databasePath;
    private ILiteCollection<ObjectData> collection;

#endif

    private const float timeToSave = 10;

    [Header("Настройка сохранений")]
    [SerializeField] private Button saveButton;
    [SerializeField] private Image saveButtonProgressBar;
    [Space(5)]
    [SerializeField] private Button loadButton;
    [SerializeField] private Image loadButtonProgressBar;
    private float startProgressBarAlpha;
    /// <summary>
    /// Последовательность анимаций DoTween
    /// </summary>
    private Sequence sequence;

    private LiteDatabase database; // Единственный экземпляр базы данных


    public static Action OnSave;
    public static Action OnLoad;

    public static SaveLoadSystem Instance { get; private set; }

    private void Awake()
    {
        this.Init();
    }

    private void Start()
    {
        this.InvokeLoad();
        this.StartCoroutine(AutoSave());
    }

    private void OnApplicationQuit()
    {
        this.database?.Dispose();
    }

    public void InvokeSave()
    {
        OnSave?.Invoke();
        Notifications.InvokeNotify("Сохранено");
        this.ProgressBarCompleteAnimation(this.saveButtonProgressBar);
    }

    public void InvokeLoad()
    {
        OnLoad?.Invoke();
        Notifications.InvokeNotify("Загружено");
        this.ProgressBarCompleteAnimation(this.loadButtonProgressBar);
    }

    private void ProgressBarCompleteAnimation(Image progressBar)
    {
        if (this.sequence != null)
        {
            this.sequence.Kill();
            this.sequence = null;
        }

        this.sequence = DOTween.Sequence();

        this.sequence.Append(progressBar.DOFillAmount(1, Constants.Timings.Millisecond_500))
                     .Append(progressBar.DOFade(0, Constants.Timings.Millisecond_300))
                     .OnComplete(() =>
                     {
                         this.sequence = null;
                     });

        progressBar.fillAmount = 0;
        var tmpColor = progressBar.color;
        progressBar.color = new Color(tmpColor.r, tmpColor.g, tmpColor.b, startProgressBarAlpha);
    }

    private IEnumerator AutoSave()
    {
        yield return new WaitForSeconds(timeToSave);
        OnSave?.Invoke();
        StartCoroutine(AutoSave());
    }

#if UNITY_WEBGL || PLATFORM_WEBGL

    private void Init()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        startProgressBarAlpha = loadButtonProgressBar.color.a;
    }

#endif

#if UNITY_STANDALONE

    /// <summary>
    /// Асинхронное потокобезопасное сохранение данных одного объекта
    /// </summary>
    /// <param name="data">Экземпляр сохраняемого объекта</param>
    /// <returns></returns>
    public async Task SaveObjectDataAsync(ObjectData data)
    {
        await Task.Run(() =>
        {
            lock (collection)
            {
                collection.Upsert(data);
            }
        });
    }

    /// <summary>
    /// Асинхронная потокобезопасная загрузка данных одного объекта
    /// </summary>
    /// <param name="objectId"></param>
    /// <param name="callback"></param>
    /// <returns></returns>
    public async Task LoadObjectDataAsync(string objectId, Action<ObjectData> callback)
    {
        ObjectData loadedData = null;

        await Task.Run(() =>
        {
            lock (collection) // Потокобезопасный доступ
            {
                loadedData = collection.FindOne(x => x.Id == objectId);
            }
        });

        callback?.Invoke(loadedData);
    }

    private void InitializeDatabase()
    {
        database = new LiteDatabase(databasePath);

        // Настройка BsonMapper для Vector3
        var mapper = BsonMapper.Global;
        mapper.RegisterType<Vector3>(
            serialize: vector => new BsonDocument
            {
                ["x"] = vector.x,
                ["y"] = vector.y,
                ["z"] = vector.z
            },
            deserialize: bson =>
            {
                return new Vector3(
                    (float)bson["x"].AsDouble,
                    (float)bson["y"].AsDouble,
                    (float)bson["z"].AsDouble
                );
            }
        );

        // Настройка BsonMapper для Quaternion
        mapper.RegisterType<Quaternion>(
            serialize: quaternion => new BsonDocument
            {
                ["x"] = quaternion.x,
                ["y"] = quaternion.y,
                ["z"] = quaternion.z,
                ["w"] = quaternion.w
            },
            deserialize: bson =>
            {
                // Проверка на диапазон значений
                return new Quaternion(
                    (float)Math.Clamp(bson["x"].AsDouble, -Mathf.Infinity, Mathf.Infinity),
                    (float)Math.Clamp(bson["y"].AsDouble, -Mathf.Infinity, Mathf.Infinity),
                    (float)Math.Clamp(bson["z"].AsDouble, -Mathf.Infinity, Mathf.Infinity),
                    (float)Math.Clamp(bson["w"].AsDouble, -Mathf.Infinity, Mathf.Infinity)
                );
            }
        );

        mapper.Entity<ObjectData>().Id(x => x.Id);

        collection = database.GetCollection<ObjectData>(collectionName);
    }

    private void Init()
    {
        databasePath = Path.Combine(Application.persistentDataPath, "saveData.db");

        if (Instance == null)
        {
            Instance = this;

            InitializeDatabase();
        }
        else
        {
            Destroy(gameObject);
        }

        startProgressBarAlpha = loadButtonProgressBar.color.a;
    }

#endif
}
