using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StaticData : MonoBehaviour
{
    [Tooltip("Текущий буффер мебели для спавна")]
    public static List<GameObject> CurrentSpawnObjectsPool = new();

    public enum FurnitureType
    {
        Tables = 0,
        Chairs = 1,
        CabinetFurniture = 2,
        Chests = 3,
        Other = 4
    }

    public static void DropObjectsInPool()
    {
        if (CurrentSpawnObjectsPool.Count > 0)
        {
            //int iter = CurrentSpawnObjectsPool.Count - 1;
            foreach (var obj in CurrentSpawnObjectsPool.ToList())
            {
                CurrentSpawnObjectsPool.Remove(obj);
                //iter--;
            }
        }
    }
}
