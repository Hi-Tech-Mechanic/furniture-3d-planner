using UnityEngine;

public class BuildingGrid : MonoBehaviour
{
    public Vector2Int GridSize = new Vector2Int(10, 10);

    private Building[,] grid;
    private Building flyingBuilding;
    private Camera mainCamera;

    private void Awake()
    {
        grid = new Building[GridSize.x, GridSize.y];

        mainCamera = Camera.main;
    }

    public void StartPlacingBuilding(Building buildingPrefab)
    {
        if (flyingBuilding != null)
        {
            Destroy(flyingBuilding.gameObject);
        }

        flyingBuilding = Instantiate(buildingPrefab, gameObject.transform);

    }


    void Update()
    {
        if (flyingBuilding is not null)
        {
            var groundPlane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (groundPlane.Raycast(ray, out float position))
            {
                Vector3 worldPosition = ray.GetPoint(position);

                int x = Mathf.RoundToInt(worldPosition.x);
                int y = Mathf.RoundToInt(worldPosition.z);

                flyingBuilding.transform.position = new Vector3(x, 0, y);

                CheckUserKeyboardInput();

                if (Input.GetMouseButtonDown(0))
                {
                    flyingBuilding = null;
                }
            }
        }
    }

    private void CheckUserKeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
            RotateObject("left");
        else if (Input.GetKeyDown(KeyCode.R))
            RotateObject("right");
        else if (Input.GetKeyDown(KeyCode.W))
            RotateObject("up");
        else if (Input.GetKeyDown(KeyCode.S))
            RotateObject("down");
        else if (Input.GetKeyDown(KeyCode.Q))
            ScaleObject("up");
        else if (Input.GetKeyDown(KeyCode.Z))
            ScaleObject("down");
    }

    private void RotateObject(string forward)
    {
        Vector3 rotate = flyingBuilding.gameObject.transform.eulerAngles;

        switch (forward)
        {
            case "left":
                rotate.y -= UserOptions.CurrentRotationAngle;
                break;
            case "right":
                rotate.y += UserOptions.CurrentRotationAngle;
                break;
            case "up":
                rotate.x += UserOptions.CurrentRotationAngle;
                break;
            case "down":
                rotate.x -= UserOptions.CurrentRotationAngle;
                break;
        }

        flyingBuilding.gameObject.transform.rotation = Quaternion.Euler(rotate);
    }

    private void ScaleObject(string type)
    {
        Vector3 scale = flyingBuilding.gameObject.transform.localScale;

        if (type == "up")
            scale *= UserOptions.CurrentSizeChange;
        else if (type == "down")
            scale /= UserOptions.CurrentSizeChange;

        flyingBuilding.gameObject.transform.localScale = scale;
    }

    public void SpawnObjectPerKeyPad(int index)
    {
        if (StaticData.CurrentSpawnObjectsPool.Count > 0)
        {
            StartPlacingBuilding(StaticData.CurrentSpawnObjectsPool[index].GetComponent<Building>());
        }
    }
}
