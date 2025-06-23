using System;
using TMPro;
using UnityEngine;

/// <summary>
/// Пользовательские настройки
/// </summary>
public class UserOptions : MonoBehaviour
{
    private const float standardRotationAngleChange = 90;
    private const float standardSizeChange = 1.1F;

    private const int maxRotationAngle = 3600;
    private const int maxSize = 10;

    [Header("Поля ввода")]
    [SerializeField] private TMP_InputField inputAngleField;
    [SerializeField] private TextMeshProUGUI angleValue;
    [Space(5)]
    [SerializeField] private TMP_InputField inputSizeField;
    [SerializeField] private TextMeshProUGUI sizeValue;

    public static float CurrentRotationAngleChange = standardRotationAngleChange;
    public static float CurrentSizeChange = standardSizeChange;

    private void Start()
    {
        DisplayRotationAngleValue();
        DisplaySizeValue();
    }

    public void UpdateRotationAngleValue()
    {
        if (string.IsNullOrEmpty(inputAngleField.text))
        {
            CurrentRotationAngleChange = 0;
        }
        else
        {
            var _ = float.TryParse(inputAngleField.text, out var value);
            CurrentRotationAngleChange = value;
            CurrentRotationAngleChange = CheckCorrectInput(CurrentRotationAngleChange, maxRotationAngle);
        }

        DisplayRotationAngleValue();
    }

    public void UpdateSizeValue()
    {
        if (string.IsNullOrEmpty(inputSizeField.text))
        {
            CurrentSizeChange = 0;
        }
        else
        {
            var _ = float.TryParse(inputSizeField.text, out var value);
            CurrentSizeChange = value / 100;
            CurrentSizeChange = CheckCorrectInput(CurrentSizeChange, maxSize, false);
        }

        DisplaySizeValue();
    }

    private void DisplayRotationAngleValue()
    {
        angleValue.text = $"{CurrentRotationAngleChange}°";
    }

    private void DisplaySizeValue()
    {
        sizeValue.text = $"{CurrentSizeChange * 100}%";
    }

    private float CheckCorrectInput(float value, float maxValue, bool negativeValueIsExist = true)
    {
        if (value > maxValue)
        {
            value = maxValue;
        }
        else if (negativeValueIsExist && value < -maxValue)
        {
            value = -maxValue;
        }

        return value;
    }
}
