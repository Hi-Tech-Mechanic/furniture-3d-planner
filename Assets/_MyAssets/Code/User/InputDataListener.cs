using System;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class InputDataListener : MonoBehaviour
{
    [SerializeField] private GameObject gun;
    [SerializeField] private MusicPlayer musicPlayer;
    [SerializeField] private FirstPersonController firstPersonController;
    [SerializeField] private SaveLoadSystem saveLoadSystem;

    #region User Input Actions

    public static Action<bool> OnMouseLockChanged;
    public static Action<int> OnKeyboardClicked;
    public static Action OnClickedKeyCode_H;
    public static Action OnClickedKeyCode_Delete;
    public static Action OnClickedKeyCode_LeftControl;

    #endregion

    private bool cursorIsFreeMove = false;
    private bool fullScreen = false;
    private int windowedScreenWidth;
    private int windowedScreenHeight;

    private void Start()
    {
        this.SwitchMouseLockState(false);
    }

    private void OnEnable()
    {
        HelpButton.OnPressed += SwitchMouseLockState;
    }

    private void OnDisable()
    {
        HelpButton.OnPressed -= SwitchMouseLockState;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            OnKeyboardClicked?.Invoke(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            OnKeyboardClicked?.Invoke(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            OnKeyboardClicked?.Invoke(2);
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            OnKeyboardClicked?.Invoke(3);
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            OnKeyboardClicked?.Invoke(4);
        else if (Input.GetKeyDown(KeyCode.Alpha6))
            OnKeyboardClicked?.Invoke(5);
        else if (Input.GetKeyDown(KeyCode.Alpha7))
            OnKeyboardClicked?.Invoke(6);
        else if (Input.GetKeyDown(KeyCode.Alpha8))
            OnKeyboardClicked?.Invoke(7);
        else if (Input.GetKeyDown(KeyCode.Alpha9))
            OnKeyboardClicked?.Invoke(8);
        else if (Input.GetKeyDown(KeyCode.Backspace))
            ControlPanel.Instance.DecreaseDepthInControlPanel();
        else if (Input.GetKeyDown(KeyCode.H))
            OnClickedKeyCode_H?.Invoke();
        else if (Input.GetKeyDown(KeyCode.Tab))
            this.SwitchMouseLockState(null);
        else if (Input.GetKeyDown(KeyCode.LeftControl))
            OnClickedKeyCode_LeftControl?.Invoke();
        else if (Input.GetKeyDown(KeyCode.M))
            this.musicPlayer.SwitchMusic();
        else if (Input.GetKeyDown(KeyCode.N))
            this.musicPlayer.NextMusic();
        else if (Input.GetKeyDown(KeyCode.G))
            this.gun.SetActive(this.gun.activeInHierarchy == false);
        else if (Input.GetKeyDown(KeyCode.F6))
            saveLoadSystem.InvokeSave();
        else if (Input.GetKeyDown(KeyCode.F10))
            saveLoadSystem.InvokeLoad();
        else if (Input.GetKeyDown(KeyCode.F11))
            this.SwitchFullScreenState();
        else if (Input.GetKeyDown(KeyCode.Delete))
            OnClickedKeyCode_Delete?.Invoke();
        else if (Input.GetKeyDown(KeyCode.Escape))
            this.SwitchFreeMouseMoveState();
    }

    private void SwitchMouseLockState(bool? cursorIsVisible)
    {
        if (cursorIsVisible == null)
            cursorIsVisible = !Cursor.visible;

        this.firstPersonController.m_MouseLook.lockCursor = (bool)cursorIsVisible;
        this.firstPersonController.enabled = !(bool)cursorIsVisible;

        Cursor.visible = (bool)cursorIsVisible;

        if ((bool)cursorIsVisible)
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        OnMouseLockChanged?.Invoke(this.firstPersonController.m_MouseLook.lockCursor);
    }

    private void SwitchFreeMouseMoveState()
    {
        cursorIsFreeMove = !cursorIsFreeMove;

        Cursor.visible = cursorIsFreeMove;

        if (cursorIsFreeMove)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;

        this.firstPersonController.enabled = !cursorIsFreeMove;
    }


    private void SwitchFullScreenState()
    {
        if (this.fullScreen == false)
        {
            windowedScreenWidth = Screen.width;
            windowedScreenHeight = Screen.height;
        }

        this.fullScreen = !this.fullScreen;

        if (this.fullScreen)
        {
            int width = Screen.currentResolution.width;
            int height = Screen.currentResolution.height;
            Screen.SetResolution(width, height, true);
            Screen.fullScreen = true;
            Notifications.InvokeNotify("Полноэкранный режим включен");
        }
        else
        {
            Screen.SetResolution(windowedScreenWidth, windowedScreenHeight, false);
            Screen.fullScreen = false;
            Notifications.InvokeNotify("Полноэкранный режим отключен");
        }
    }
}
