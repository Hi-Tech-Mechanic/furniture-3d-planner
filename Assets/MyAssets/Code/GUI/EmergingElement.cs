using DG.Tweening;
using FurnitureShop;
using UnityEngine;

public class EmergingElement : MonoBehaviour
{
    private const int startBias = -200;
    private RectTransform rectTransform;

    private void OnEnable()
    {
        rectTransform = this.transform.GetComponent<RectTransform>();
        rectTransform.position = new Vector3(startBias, this.transform.position.y);
    }

    public void StartBias()
    {
        rectTransform.DOAnchorPosX(0, Constants.Animations.Millisecond_100);
    }
}
