using System.Globalization;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class PlayerData
{
    public float posX;
    public float posY;
    public float posZ;

    public PlayerData(Vector3 position)
    {
        posX = position.x;
        posY = position.y;
        posZ = position.z;
    }

    public Vector3 GetPosition()
    {
        return new Vector3(posX, posY, posZ);
    }
}