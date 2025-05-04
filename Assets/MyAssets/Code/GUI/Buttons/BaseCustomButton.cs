namespace FurnitureShop
{
    using Unity.VisualScripting;
    using UnityEngine;
    using UnityEngine.EventSystems;

    public abstract class BaseCustomButton : MonoBehaviour, ICustomButton, IPointerEnterHandler, IPointerExitHandler
    {
        private AudioClip hoverSound;
        private AudioSource audioSource;

        protected virtual void Awake()
        {
            var clip = Resources.Load<AudioClip>("Sound/Effects/SFX_Press_Button_Joystick");
            if (clip != null)
                this.hoverSound = clip;

            var audioSource = this.GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = this.AddComponent<AudioSource>();
            }

            this.audioSource = audioSource;
            this.audioSource.clip = this.hoverSound;
        }

        public virtual void PlaySoundAfterPointerEnter()
        {
            this.audioSource.Play();
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

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            this.ActionAfterPointerEnter();
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            this.ActionAfterPointerExit();
        }
    }
}
