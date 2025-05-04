namespace FurnitureShop
{
    using UnityEngine;
    using UnityEngine.UI;

    public class BackButton : MonoBehaviour
    {
        private Button _thisButton;

        private void Awake()
        {
            _thisButton = gameObject.GetComponent<Button>();
        }

        private void OnEnable()
        {
            SelectFurnitureType.OnSelected += GetActionDataForExit;
        }

        private void OnDisable()
        {
            SelectFurnitureType.OnSelected += GetActionDataForExit;
        }

        private void GetActionDataForExit(SelectFurnitureType selectFurnitureType)
        {
            StaticData.DropObjectsInPool();
            _thisButton.onClick.RemoveAllListeners();
            _thisButton.onClick.AddListener(selectFurnitureType.ExitAdditiveElementsMode);
        }
    }
}

