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
/// �������� ������� �������� ��� ������ � ���������,
/// ��� ������������� ��� � � ������� ������
/// </summary>
public class SaveLoadSystem : MonoBehaviour
{
#if UNITY_STANDALONE

    private const string collectionName = "objects";
    private string databasePath;
    private ILiteCollection<ObjectData> collection;

#endif

    private const float timeToSave = 10;

    [Header("��������� ����������")]
    [SerializeField] private Button saveButton;
    [SerializeField] private Image saveButtonProgressBar;
    [Space(5)]
    [SerializeField] private Button loadButton;
    [SerializeField] private Image loadButtonProgressBar;
    private float startProgressBarAlpha;
    /// <summary>
    /// ������������������ �������� DoTween
    /// </summary>
    private Sequence sequence;

    private LiteDatabase database; // ������������ ��������� ���� ������


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
        Notifications.InvokeNotify("���������");
        this.ProgressBarCompleteAnimation(this.saveButtonProgressBar);
    }

    public void InvokeLoad()
    {
        OnLoad?.Invoke();
        Notifications.InvokeNotify("���������");
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
    /// ����������� ���������������� ���������� ������ ������ �������
    /// </summary>
    /// <param name="data">��������� ������������ �������</param>
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
    /// ����������� ���������������� �������� ������ ������ �������
    /// </summary>
    /// <param name="objectId"></param>
    /// <param name="callback"></param>
    /// <returns></returns>
    public async Task LoadObjectDataAsync(string objectId, Action<ObjectData> callback)
    {
        ObjectData loadedData = null;

        await Task.Run(() =>
        {
            lock (collection) // ���������������� ������
            {
                loadedData = collection.FindOne(x => x.Id == objectId);
            }
        });

        callback?.Invoke(loadedData);
    }

    private void InitializeDatabase()
    {
        database = new LiteDatabase(databasePath);

        // ��������� BsonMapper ��� Vector3
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

        // ��������� BsonMapper ��� Quaternion
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
                // �������� �� �������� ��������
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
