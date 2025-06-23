namespace FurnitureShop
{
    using DG.Tweening;
    using UnityEngine;
    using UnityEngine.UI;

    public class HoverTarget : BaseCustomButton
    {
        private const float elementBias = 100;
        private const float targetAlpha = 0.6F;

        private RectTransform RectTransform;
        private Image elementBackground;
        private Vector2 startSizeDelta;
        private Vector2 targetSizeDelta;
        private float startAlpha;

        protected override void Awake()
        {
            base.Awake();
            this.Init();
        }

        protected override void Init()
        {
            base.Init();
            RectTransform = this.gameObject.GetComponent<RectTransform>();
            elementBackground = this.transform.GetChild(1).GetComponentInChildren<Image>(true);
            startAlpha = elementBackground.color.a;

            startSizeDelta.x = this.RectTransform.rect.width;
            startSizeDelta.y = this.RectTransform.rect.height;
            targetSizeDelta = startSizeDelta;
            targetSizeDelta.x -= elementBias;
        }

        public override void ActionAfterPointerEnter()
        {
            if (Cursor.visible == false)
                return;

            base.ActionAfterPointerEnter();
            this.elementBackground.DOFade(targetAlpha, Constants.Timings.Millisecond_500);
            this.RectTransform.DOSizeDelta(targetSizeDelta, Constants.Timings.Millisecond_300);
        }

        public override void ActionAfterPointerExit()
        {
            base.ActionAfterPointerExit();
            
            this.elementBackground.DOFade(startAlpha, Constants.Timings.Millisecond_500);
            this.RectTransform.DOSizeDelta(startSizeDelta, Constants.Timings.Millisecond_300);
        }
    }
}
