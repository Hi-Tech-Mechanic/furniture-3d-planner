using UnityEngine;

/// <summary>
/// Состояние установки объекта
/// </summary>
public class PlaceObject : MonoBehaviour
{
    /// <summary>
    /// Высота обрисовываемого периметра
    /// </summary>
    private const float cubeHeight = 0.1F;

    [SerializeField] private Vector2Int Size = Vector2Int.one;
    
    /// <summary>
    /// Выделение площади под создаваемым объектом
    /// </summary>
    private GameObject bottomAreaQuad;
    private GameObject createdBottomAreaQuad;

    private void Awake()
    {
        this.bottomAreaQuad = Resources.Load<GameObject>("Prefabs/ObjectAreaQuad");
    }

    private void Start()
    {
        this.CreateBottomAreaQuad();
    }

    private void OnDrawGizmos()
    {
        for (int x = 0; x < Size.x; x++)
        {
            for (int y = 0; y < Size.y; y++)
            {
                Gizmos.color = new Color(0f, 1f, 0f, 0.5f);
                Gizmos.DrawCube(transform.position + new Vector3(x, 0, y), new Vector3(1, cubeHeight, 1));
            }
        }
    }

    public void DestroyBottomAreaQuad()
    {
       Destroy(this.createdBottomAreaQuad);
    }

    /// <summary>
    /// При старте будет создавать область, обозначающий периметр объекта
    /// </summary>
    private void CreateBottomAreaQuad()
    {
        if (BuildingController.IsBuildMode == false)
            return;

        this.createdBottomAreaQuad = Instantiate(this.bottomAreaQuad, this.transform);
        Destroy(this.createdBottomAreaQuad.GetComponent<Collider>());
        this.createdBottomAreaQuad.transform.localScale = new Vector3(Size.x, cubeHeight, Size.y);
        this.createdBottomAreaQuad.name = "BottomAreaQuad";
    }
}
