using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI mouseLockMode;
    [SerializeField] private HelpButton helpButton;

    private void OnEnable()
    {
        InputDataListener.OnMouseLockChanged += this.DisplayMouseLockMode;
        InputDataListener.OnClickedKeyCode_H += this.HelpButtonHandler;
    }

    private void OnDisable()
    {
        InputDataListener.OnMouseLockChanged -= this.DisplayMouseLockMode;
        InputDataListener.OnClickedKeyCode_H -= this.HelpButtonHandler;
    }

    private void DisplayMouseLockMode(bool isLock)
    {
        if (this.mouseLockMode == null)
            return;

        if (isLock == true)
            this.mouseLockMode.text = "���������� ������� \n<color=green>���</color>";
        else
            this.mouseLockMode.text = "���������� ������� \n<color=red>����</color>";
    }

    private void HelpButtonHandler()
    {
        helpButton.SwitchState();
    }
}
