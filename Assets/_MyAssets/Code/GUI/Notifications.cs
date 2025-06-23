using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ќтвечает за уведомлени€ о событи€х дл€ пользовател€
/// </summary>
public class Notifications : MonoBehaviour
{
    private static Transform notifyParent;
    private static GameObject notifyPrefab;

    private void Awake()
    {
        notifyParent = GameObject.Find("Canvas_HUD").transform;
        notifyPrefab = Resources.Load<GameObject>("Prefabs/PushNotification");
    }

    public static void InvokeNotify(string message)
    {
        var notify = Instantiate(notifyPrefab, notifyParent);
        var text = notify.GetComponentInChildren<TextMeshProUGUI>();
        var rectTransform = notify.GetComponent<RectTransform>();
        var image = notify.GetComponent<Image>();

        text.text = message;
        var notifyHeight = rectTransform.sizeDelta.y;
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, notifyHeight);
        rectTransform.localScale = new Vector3(1.2F, 1.2F, 1.2F);

        var notifyAnimation = HelperAnimation.ShowPopupElementSmoothly(image, text, rectTransform);
        notifyAnimation.OnComplete(()=> Destroy(notify));
    }
}
