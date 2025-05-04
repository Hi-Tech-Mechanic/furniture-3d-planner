using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class InputDataListener : MonoBehaviour
{
    [SerializeField] private BuildingGrid buildingGrid;
    [SerializeField] private Button exitFromSelectedFurnitureType;
    [SerializeField] private HelpButton helpButton;
    [SerializeField] private GameObject gun;
    [SerializeField] private TextMeshProUGUI mouseLockMode;
    [SerializeField] private MusicPlayer musicPlayer;
    [SerializeField] private FirstPersonController firstPersonController;

    private void OnEnable()
    {
        HelpButton.OnPressed += SwitchMouseLockMode;
    }

    private void OnDisable()
    {
        HelpButton.OnPressed -= SwitchMouseLockMode;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            buildingGrid.SpawnObjectPerKeyPad(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            buildingGrid.SpawnObjectPerKeyPad(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            buildingGrid.SpawnObjectPerKeyPad(2);
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            buildingGrid.SpawnObjectPerKeyPad(3);
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            buildingGrid.SpawnObjectPerKeyPad(4);
        else if (Input.GetKeyDown(KeyCode.Alpha6))
            buildingGrid.SpawnObjectPerKeyPad(5);
        else if (Input.GetKeyDown(KeyCode.Alpha7))
            buildingGrid.SpawnObjectPerKeyPad(6);
        else if (Input.GetKeyDown(KeyCode.Alpha8))
            buildingGrid.SpawnObjectPerKeyPad(7);
        else if (Input.GetKeyDown(KeyCode.Alpha9))
            buildingGrid.SpawnObjectPerKeyPad(8);
        else if (Input.GetKeyDown(KeyCode.Backspace))
            exitFromSelectedFurnitureType.onClick.Invoke();
        else if (Input.GetKeyDown(KeyCode.Backspace))
            exitFromSelectedFurnitureType.onClick.Invoke();
        else if (Input.GetKeyDown(KeyCode.H))
            helpButton.SwitchState();
        else if (Input.GetKeyDown(KeyCode.Tab))
            this.SwitchMouseLockMode(null);
        else if (Input.GetKeyDown(KeyCode.M))
            musicPlayer.SwitchMusic();
        else if (Input.GetKeyDown(KeyCode.N))
            musicPlayer.NextMusic();
        else if (Input.GetKeyDown(KeyCode.G))
            gun.SetActive(!gun.activeInHierarchy);
        //////////////////////
        else if (Input.GetKeyDown(KeyCode.Keypad1))
            Cursor.lockState = CursorLockMode.None;
    }

    private void SwitchMouseLockMode(bool? cursorIsVisible)
    {
        if (cursorIsVisible == null)
            cursorIsVisible = !Cursor.visible;

        this.firstPersonController.m_MouseLook.lockCursor = (bool)cursorIsVisible;
        this.firstPersonController.enabled = !(bool)cursorIsVisible;

        if ((bool)cursorIsVisible)
        {
            Cursor.visible = (bool)cursorIsVisible;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Cursor.visible = (bool)cursorIsVisible;
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (this.mouseLockMode == null)
            return;

        if (this.firstPersonController.m_MouseLook.lockCursor == true)
            this.mouseLockMode.text = "Блокировка мышки <color=green>вкл</color>";
        else
            this.mouseLockMode.text = "Блокировка мышки <color=red>выкл</color>";
    }
}
