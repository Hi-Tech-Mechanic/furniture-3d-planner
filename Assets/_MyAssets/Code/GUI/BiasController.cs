using FurnitureShop;
using System.Collections;
using UnityEngine;

public class BiasController : MonoBehaviour
{
    [SerializeField] private EmergingElement[] elements;

    public void ShowAllElements()
    {
        if (this.gameObject.activeInHierarchy == false) {
            this.gameObject.SetActive(true);
        }
        StartCoroutine(SoftBias());
    }

    private IEnumerator SoftBias()
    {
        foreach (var element in elements)
        {
            element.gameObject.SetActive(true);
            element.StartBias();
            yield return new WaitForSeconds(Constants.Timings.Millisecond_100);
        }
    }

    public void HideAllElements()
    {
        foreach (var element in elements)
        {
            element.gameObject.SetActive(false);
        }
    }
}
