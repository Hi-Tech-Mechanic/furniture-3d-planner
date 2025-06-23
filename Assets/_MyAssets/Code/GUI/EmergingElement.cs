using DG.Tweening;
using FurnitureShop;
using UnityEngine;

public class EmergingElement : MonoBehaviour
{
    private const int targetOffset = -200;
    private float startOffset;
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = this.transform.GetComponent<RectTransform>();
        startOffset = this.rectTransform.anchoredPosition.x;
    }

    private void OnEnable()
    {
        this.rectTransform.position = new Vector3(targetOffset, this.transform.position.y);
    }

    public void StartBias()
    {
        this.rectTransform.DOAnchorPosX(this.startOffset, Constants.Timings.Millisecond_100);
    }
}
