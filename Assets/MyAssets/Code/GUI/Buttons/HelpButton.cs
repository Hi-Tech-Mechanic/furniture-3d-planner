using FurnitureShop;
using System;
using UnityEngine;

public class HelpButton : MonoBehaviour
{
    [SerializeField] private HelpMenuAnimation helpMenu;
    [SerializeField] private BiasController[] biasControllers;
    CustomButton customButton;
    private bool windowIsOpen = false;

    public static Action<bool?> OnPressed;

    private void Awake()
    {
        this.customButton = GetComponent<CustomButton>();
    }

    public void SwitchState()
    {
        if (this.windowIsOpen)
        {
            this.CloseHelpMenuWindow();
        }
        else
        {
            this.OpenHelpMenuWindow();
        }

        this.windowIsOpen = !this.windowIsOpen;
        OnPressed?.Invoke(windowIsOpen);
    }

    private void OpenHelpMenuWindow()
    {
        this.customButton.InvokeClickAnimation();
        this.helpMenu.gameObject.SetActive(true);
        this.helpMenu.Open();

        foreach (var controller in biasControllers)
        {
            controller.ShowAllElements();
        }
    }

    private void CloseHelpMenuWindow()
    {
        this.helpMenu.Close();

        foreach (var controller in biasControllers)
        {
            controller.HideAllElements();
        }
    }
}
