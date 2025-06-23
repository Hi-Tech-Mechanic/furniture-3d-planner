namespace FurnitureShop
{
    using Unity.VisualScripting;
    using UnityEngine;
    using UnityEngine.EventSystems;

    public abstract class BaseCustomButton : MonoBehaviour, ICustomButton, IPointerEnterHandler,
        IPointerExitHandler, IPointerClickHandler
    {
        private AudioClip hoverSound;
        private AudioClip clickSound;
        private AudioSource audioSource;

        protected virtual void Awake()
        {
            this.Init();
        }

        protected virtual void Init()
        {
            var clip = Resources.Load<AudioClip>("Sound/Effects/SFX_Press_Button_Joystick");
            if (clip != null)
                this.hoverSound = clip;

            clip = Resources.Load<AudioClip>("Sound/Effects/SFX_Press_Button_Keyboard");
            if (clip != null)
                this.clickSound = clip;

            var audioSource = this.GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = this.AddComponent<AudioSource>();
            }

            this.audioSource = gameObject.GetComponent<AudioSource>();
        }

        public void ActionAfterClick()
        {
            this.audioSource.PlayOneShot(clickSound);
        }

        public virtual void ActionAfterPointerEnter()
        {
            if (Cursor.visible == false)
                return;
    
            this.PlaySoundAfterPointerEnter();
        }

        public virtual void ActionAfterPointerExit()
        {

        }

        public virtual void PlaySoundAfterPointerEnter()
        {
            this.audioSource.PlayOneShot(hoverSound);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            this.ActionAfterClick();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            this.ActionAfterPointerEnter();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            this.ActionAfterPointerExit();
        }
    }
}
