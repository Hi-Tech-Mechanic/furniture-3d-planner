using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���������� ������� ������� ������
/// </summary>
public class ControlPanel : MonoBehaviour
{
    private const int destroyModeIndex = 1;

    [Header("���������� ������� ������ ����������")]
    [Space(5)]
    [Tooltip("������ �����, ������ ���� 1 ������������")]
    [SerializeField] private Button exitButtonInControlPanel;
    [Space(10)]
    [SerializeField] private GameObject layer_1;
    [SerializeField] private Button[] buttonsLayer_1;
    [SerializeField] private GameObject layer_2;
    [SerializeField] private Button[] buttonsLayer_2;
    [SerializeField] private GameObject layer_3;
    [Tooltip("������������ ����� ������")]
    [SerializeField] private Transform[] furnitureTypes;
    [Space(5)]

    [Header("����� ����������")]
    [SerializeField] private GameObject lazerRay;

    private List<Button> buttonsLayer_3 = new();

    public static ControlPanel Instance { get; private set; }

    /// <summary>
    /// ������� ����� � ������� ������
    /// </summary>
    private int DepthOfEntryToControlPanel
    {
        get => _depthOfEntryToControlPanel;
        set
        {
            _depthOfEntryToControlPanel = value;
            this.SelectCurrentState();
        }
    }
    private int _depthOfEntryToControlPanel = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        InputDataListener.OnKeyboardClicked += this.KeyboardInputHandler;
        InputDataListener.OnClickedKeyCode_Delete += this.InvokeDestroyMode;
    }

    private void OnDisable()
    {
        InputDataListener.OnKeyboardClicked -= this.KeyboardInputHandler;
        InputDataListener.OnClickedKeyCode_Delete -= this.InvokeDestroyMode;
    }

    #region Buttons Handlers

    public void IncreaseDepthInControlPanel()
    {
        this.DepthOfEntryToControlPanel++;
    }

    public void DecreaseDepthInControlPanel()
    {
        this.DepthOfEntryToControlPanel--;

        if (DepthOfEntryToControlPanel < 0)
            DepthOfEntryToControlPanel = 0;
    }

    public void SwitchDestroyMode()
    {
        if (BuildingController.IsBuildMode == true)
            return;

        BuildingController.IsDestroyMode = !BuildingController.IsDestroyMode;
        lazerRay.SetActive(BuildingController.IsDestroyMode);
    }

    #endregion

    /// <summary>
    /// ������������ ������� ��� ������� ������
    /// </summary>
    /// <param name="index">����� �������, ������� � 0</param>
    private void KeyboardInputHandler(int index)
    {
        if (BuildingController.IsBuildMode)
            return;

        if (layer_1.activeInHierarchy)
        {
            if (index >= 0 && index < buttonsLayer_1.Length)
            {
                buttonsLayer_1[index].onClick.Invoke();
            }
        }
        else if (layer_2.activeInHierarchy)
        {
            if (index >= 0 && index < buttonsLayer_2.Length)
            {
                buttonsLayer_2[index].onClick.Invoke();
            }
        }
        else if (layer_3.activeInHierarchy && index >= 0 && index < furnitureTypes.Length) 
        {
            foreach (var furnitureType in furnitureTypes)
            {
                if (furnitureType.gameObject.activeInHierarchy == false)
                    continue;

                for (int childIndex = 0; childIndex < furnitureType.childCount; childIndex++)
                {
                    buttonsLayer_3.Add(furnitureType.GetChild(childIndex).GetComponent<Button>());
                }

                break;
            }

            if (index >= 0 && index < buttonsLayer_3.Count)
            {
                buttonsLayer_3[index].onClick.Invoke();
                buttonsLayer_3.Clear(); // ������� ����� ������
            }
        }
    }

    private void SelectCurrentState()
    {
        if (this._depthOfEntryToControlPanel > 0)
            exitButtonInControlPanel.interactable = true;
        else exitButtonInControlPanel.interactable = false;

        switch (this._depthOfEntryToControlPanel)
        {
            case (0):
                layer_1.SetActive(true);
                layer_2.SetActive(false);
                break;
            case (1):
                layer_1.SetActive(false);
                layer_2.SetActive(true);
                layer_3.SetActive(false);

                break;
            case (2):
                layer_2.SetActive(false);
                layer_3.SetActive(true);

                // ����������� ���������� ��������� ��������� ���������
                foreach (var element in furnitureTypes)
                {
                    if (element.gameObject.activeInHierarchy)
                        element.gameObject.SetActive(false);
                }
                break;
        }
    }

    private void InvokeDestroyMode()
    {
        buttonsLayer_1[destroyModeIndex].onClick.Invoke();
    }
}
