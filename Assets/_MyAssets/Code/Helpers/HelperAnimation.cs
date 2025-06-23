using DG.Tweening;
using FurnitureShop;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class HelperAnimation
{
   public static Sequence ShowPopupElementSmoothly(Image image, TextMeshProUGUI text, RectTransform rectTransform)
    {
        var sequenceFade = DOTween.Sequence();
        sequenceFade.Join(image.DOFade(0, Constants.Timings.Millisecond_1000))
                    .Join(text.DOFade(0, Constants.Timings.Millisecond_1000))
                    .Join(rectTransform.DOScale(0.5F, Constants.Timings.Millisecond_1000));

        var sequence = DOTween.Sequence();
        sequence.Append(rectTransform.DOAnchorPosY(0, Constants.Timings.Millisecond_300))
                .Join(rectTransform.DOScale(1F, Constants.Timings.Millisecond_300))
                .AppendInterval(Constants.Timings.Millisecond_1000)
                .Append(sequenceFade)
                .OnComplete(() =>
                {
                    //Destroy(notify);
                    sequence = null;
                });

        return sequence;
    }
}
