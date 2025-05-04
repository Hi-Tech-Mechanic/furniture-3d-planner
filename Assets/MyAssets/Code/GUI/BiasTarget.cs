namespace FurnitureShop
{
    using DG.Tweening;
    using UnityEngine;

    public class BiasTarget : BaseCustomButton
    {
        private const float targetSize = 0.9F;
        private const float baseSize = 1F;

        public override void ActionAfterPointerEnter()
        {
            if (Cursor.visible == false)
                return;

            base.ActionAfterPointerEnter();
            this.transform.DOScaleX(targetSize, Constants.Animations.Millisecond_200);
        }

        public override void ActionAfterPointerExit()
        {
            base.ActionAfterPointerExit();
            this.transform.DOScaleX(baseSize, Constants.Animations.Millisecond_200);
        }
    }
}
