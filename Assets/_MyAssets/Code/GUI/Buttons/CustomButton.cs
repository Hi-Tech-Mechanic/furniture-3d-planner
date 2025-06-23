namespace FurnitureShop
{
    using DG.Tweening;
    using UnityEngine;
    using UnityEngine.UI;

    public class CustomButton : BaseCustomButton
    {
        [SerializeField] private bool animationIsEnabled = true;
        
        /// <summary>
        /// Подсвечиваемая рамка кнопки
        /// </summary>
        private Image buttonEdge;

        protected override void Awake()
        {
            base.Awake();
            var imgChildrens = this.transform.GetComponentsInChildren<Image>(includeInactive: true);
            foreach(var targetImg in imgChildrens)
            {
                if (targetImg.CompareTag(Constants.Tags.UIComponentEdge))
                {
                    buttonEdge = targetImg;
                }
            }
        }

        public void InvokeClickAnimation()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.AppendCallback(this.ActionAfterPointerEnter)
                    .AppendInterval(Constants.Timings.Millisecond_200)
                    .AppendCallback(this.ActionAfterPointerExit);
        }

        public override void ActionAfterPointerEnter()
        {
            if (Cursor.visible == false)
                return;

            base.ActionAfterPointerEnter();

            if (animationIsEnabled)
                this.transform.DOScale(Constants.Scales.ScaleMultiplier_110Percent, Constants.Timings.Millisecond_200);
            if (this.buttonEdge != null)
                this.buttonEdge.DOFade(1, Constants.Timings.Millisecond_200);
        }

        public override void ActionAfterPointerExit()
        {
            base.ActionAfterPointerExit();
            
            if (animationIsEnabled)
                this.transform.DOScale(1, Constants.Timings.Millisecond_200);
            if (this.buttonEdge != null)
                this.buttonEdge.DOFade(0, Constants.Timings.Millisecond_200);
        }
    }
}
