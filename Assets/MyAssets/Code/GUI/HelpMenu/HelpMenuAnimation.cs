using DG.Tweening;
using FurnitureShop;
using UnityEngine;

public class HelpMenuAnimation : MonoBehaviour
{
    private const float startPositionY = 1000;
    RectTransform rectTransform;

    private void Awake()
    {
        this.rectTransform = this.gameObject.GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        this.rectTransform.localPosition = new Vector3(0, startPositionY);
    }

    public void Open()
    {
        this.rectTransform.DOAnchorPosY(0, Constants.Animations.Millisecond_200);
    }

    public void Close()
    {
        this.rectTransform.DOAnchorPosY(startPositionY, Constants.Animations.Millisecond_200)
            .OnComplete(Disable);
    }

    private void Disable()
    {
        this.gameObject.SetActive(false);
    }
}
