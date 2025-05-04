using System;
using UnityEngine;

public class SelectFurnitureType : MonoBehaviour
{
    [Header("ћебель")]
    [SerializeField] private GameObject[] _furniturePrefabs;
    [Space (10)]

    [Header ("¬кладки в меню")]
    [SerializeField] private GameObject[] _furnitureLinks;
    [SerializeField] private GameObject _content;

    public static Action<SelectFurnitureType> OnSelected;

    public void SelectAdditiveElementsMode()
    {
        OnSelected?.Invoke(this);
        _content.gameObject.SetActive(true);

        foreach (GameObject link in _furnitureLinks)
        {
            link.SetActive(false);
        }

        StaticData.DropObjectsInPool();

        foreach (GameObject obj in _furniturePrefabs)
        {
            StaticData.CurrentSpawnObjectsPool.Add(obj);
        }
    }

    public void ExitAdditiveElementsMode()
    {
        _content.gameObject.SetActive(false);

        foreach (GameObject link in _furnitureLinks)
        {
            link.gameObject.SetActive(true);
        }
    }
}
