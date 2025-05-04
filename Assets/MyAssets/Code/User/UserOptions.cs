using System;
using TMPro;
using UnityEngine;

public class UserOptions : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputAngle;
    [SerializeField] private TMP_InputField _inputSize;

    public const int _standardRotationAngle = 90;
    public const float _standardSizeChange = 1.1f;

    public static float CurrentRotationAngle = _standardRotationAngle;
    public static float CurrentSizeChange = _standardSizeChange;

    public void UpdateAngleRotation()
    {
        CurrentRotationAngle = Int32.Parse(_inputAngle.text);
    } 

    public void UpdateSize()
    {
        CurrentSizeChange = 1 + ((float)(Int32.Parse(_inputSize.text)) / 100);
    }
}
