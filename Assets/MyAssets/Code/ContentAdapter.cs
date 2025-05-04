using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContentAdapter : MonoBehaviour
{
    [Header ("Отображение мебели в меню")]
    [SerializeField] private GameObject _contentPrefab;
    private Transform _contentParent;
    [Space(10)]

    [Header ("Модели мебели")]
    [SerializeField] private GameObject[] _furnitureElements;
    [SerializeField] private Transform _spawnParentTransform;
    public List<GameObject> CreatedObjects = new();
    [Space(10)]

    [SerializeField] private Sprite[] _elementsSprite;
    private TextMeshProUGUI _elementName;
    private Image _elementImage;

    void Start()
    {
        _contentParent = gameObject.transform;

        int iter = 0;
        foreach (Sprite sprite in _elementsSprite)
        {
            GameObject spawnedObject = Instantiate(_contentPrefab, _contentParent);

            _elementName = spawnedObject.GetComponentInChildren<TextMeshProUGUI>();
            _elementName.text = sprite.name;

            Image[] tmpImages = spawnedObject.GetComponentsInChildren<Image>();

            foreach (Image image in tmpImages)
            {
                if (image.gameObject.name == "Image")
                {
                    _elementImage = image;
                }
            }
            _elementImage.sprite = sprite;

            iter++;
        }
    }

    #region Методы взаимодействия
    //public void AddElement(int index)
    //{
    //    float rnd_x = Random.Range(0.4f, -0.4f);
    //    float rnd_z = Random.Range(0.4f, -0.4f);
    //    CreatedObjects.Add(Instantiate(_furnitureElements[index], _spawnParentTransform));
    //    Transform transform = CreatedObjects[CreatedObjects.Count - 1].gameObject.transform;
    //    CreatedObjects[CreatedObjects.Count - 1].gameObject.transform.localPosition = new Vector3(rnd_x, 1.8f, rnd_z);
    //}

    public void RemoveElement()
    {
        if (CreatedObjects.Count > 0)
        {
            Destroy(CreatedObjects[CreatedObjects.Count - 1]);
            CreatedObjects.Remove(CreatedObjects[CreatedObjects.Count - 1]);
        }
    }

    //public void ExpendElement()
    //{
    //    CreatedObjects[CreatedObjects.Count - 1].gameObject.transform.localScale *= 1.5f;
    //}
    #endregion
}
