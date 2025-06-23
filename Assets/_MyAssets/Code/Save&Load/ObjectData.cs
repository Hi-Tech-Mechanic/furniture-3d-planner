using UnityEngine;

/// <summary>
/// Класс данных с координатами и ротацией мировых объектов
/// </summary>
[System.Serializable]
public class ObjectData
{
#if UNITY_WEBGL || PLATFORM_WEBGL

    public string Id;
    public float posX, posY, posZ;
    public float rotX, rotY, rotZ, rotW;
    public float scaleX, scaleY, scaleZ;

    public ObjectData(string id, Transform transform, Quaternion quaternion, Vector3 scale)
    {
        Id = id;

        posX = transform.position.x;
        posY = transform.position.y;
        posZ = transform.position.z;

        rotX = quaternion.x;
        rotY = quaternion.y;
        rotZ = quaternion.z;
        rotW = quaternion.w;

        scaleX = scale.x;
        scaleY = scale.y;
        scaleZ = scale.z;
    }

    public Vector3 Position => new Vector3(posX, posY, posZ);
    public Quaternion Rotation => new Quaternion(rotX, rotY, rotZ, rotW);
    public Vector3 Scale => new Vector3(scaleX, scaleY, scaleZ);

#elif UNITY_STANDALONE

    public string Id { get; set; }
    public Vector3 Position { get; set; }
    public Quaternion Rotation { get; set; }
    public Vector3 Scale { get; set; }

    /// <summary>
    /// Пустой конструктор для LiteDB
    /// </summary>
    public ObjectData() { }

    public ObjectData(string id, Vector3 position, Quaternion rotation, Vector3 scale)
    {
        Id = id;
        Position = position;
        Rotation = rotation;
        Scale = scale;
    }

#endif
}