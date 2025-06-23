using DG.Tweening;
using FurnitureShop;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

/// <summary>
/// ѕровер€ет позицию пользовател€, чтобы он вернулс€ в изначальную окрестность 
/// </summary>
public class CheckUserPosition : MonoBehaviour
{
    private const int minimalValue_Y = -5;
    private Vector3 startPosition;

    private void Start()
    {
        this.startPosition = this.transform.localPosition;
    }

    private void Update()
    {
        this.CheckPosition();
    }


    private void CheckPosition()
    {
        if (this.transform.localPosition.y > minimalValue_Y)
            return;

        var sequence = DOTween.Sequence();
        var personController = this.GetComponent<FirstPersonController>();
        personController.enabled = false; // ¬ключаем выключаем контроллер, чтобы можно было двигатьс€ после перемещени€

        sequence.Append(this.transform.DOMove(this.startPosition, Constants.Timings.Millisecond_500))
                .OnComplete(() =>
                {
                    personController.enabled = true;  
                });
    }
}
