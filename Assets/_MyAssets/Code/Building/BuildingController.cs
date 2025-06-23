using DG.Tweening;
using FurnitureShop;
using TMPro;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    private const float maxRaycastDistance = 100F;

    public static bool IsBuildMode = false;
    public static bool IsDestroyMode = false;

    /// <summary>
    /// Слой, на котором находится пол
    /// </summary>
    [SerializeField] private LayerMask floorLayer;
    [SerializeField] private TextMeshProUGUI bindingModeText;

    private PlaceObject flyingBuilding;
    private Camera mainCamera;

    private bool bindingModeIsEnable = false;

    private void Awake()
    {
        this.mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        InputDataListener.OnClickedKeyCode_LeftControl += SwitchBindingMode;
    }

    private void OnDisable()
    {
        InputDataListener.OnClickedKeyCode_LeftControl -= SwitchBindingMode;
    }

    private void Update()
    {
        this.CheckBuildMode();
    }

    /// <summary>
    /// Переключить режим привязки объектов к сетке
    /// </summary>
    public void SwitchBindingMode()
    {
        this.bindingModeIsEnable = !this.bindingModeIsEnable;

        if (this.bindingModeIsEnable)
        {
            this.bindingModeText.text = "Привязка объектов \n<color=green>вкл</color>";
            Notifications.InvokeNotify("Привязка объектов включена");
        }
        else
        {
            this.bindingModeText.text = "Привязка объектов \n<color=red>выкл</color>";
            Notifications.InvokeNotify("Привязка объектов отключена");
        }
    }

    /// <summary>
    /// Навешиваемый метод
    /// </summary>
    /// <param name="buildingPrefab"></param>
    public void StartPlacingBuilding(PlaceObject buildingPrefab)
    {
        if (this.flyingBuilding != null)
        {
            Destroy(this.flyingBuilding.gameObject);
        }

        this.flyingBuilding = Instantiate(buildingPrefab, gameObject.transform);
    }

    private void CheckBuildMode()
    {
        if (this.flyingBuilding is not null)
        {
            IsBuildMode = true;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            var isHitted = Physics.Raycast(ray, out var hit, maxRaycastDistance, floorLayer);

            if (isHitted == false)
                return;

            Vector3 worldPosition = hit.point;

            if (bindingModeIsEnable)
            {
                int x = Mathf.RoundToInt(worldPosition.x);
                int z = Mathf.RoundToInt(worldPosition.z);
                worldPosition = new Vector3(x, worldPosition.y, z);
            }
     
            this.flyingBuilding.transform.position = worldPosition;

            this.CheckUserKeyboardInput();
            if (Input.GetMouseButtonDown(0))
            {
                IsBuildMode = false;
                flyingBuilding.DestroyBottomAreaQuad();
                this.flyingBuilding = null;
            }
        }
    }

    private void CheckUserKeyboardInput()
    {
        float scrollWheelInput = Input.GetAxis("Mouse ScrollWheel");

        if (Input.GetKeyDown(KeyCode.E) || scrollWheelInput < 0)
            RotateObject("left");
        else if (Input.GetKeyDown(KeyCode.R) || scrollWheelInput > 0)
            RotateObject("right");
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
                rotate.y -= UserOptions.CurrentRotationAngleChange;
                break;
            case "right":
                rotate.y += UserOptions.CurrentRotationAngleChange;
                break;
        }

        this.flyingBuilding.gameObject.transform.DORotateQuaternion(Quaternion.Euler(rotate),
            Constants.Timings.Millisecond_300);
    }

    private void ScaleObject(string type)
    {
        Vector3 scale = this.flyingBuilding.gameObject.transform.localScale;

        if (type == "up")
            scale *= UserOptions.CurrentSizeChange;
        else if (type == "down")
            scale /= UserOptions.CurrentSizeChange;

        this.flyingBuilding.gameObject.transform.DOScale(scale, Constants.Timings.Millisecond_300);
    }
}
