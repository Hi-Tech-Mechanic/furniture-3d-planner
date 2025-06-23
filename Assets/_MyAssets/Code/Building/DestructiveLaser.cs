using UnityEngine;

public class DestructiveLaser: MonoBehaviour
{
    [SerializeField] private float laserRange = 100F;
    [SerializeField] private float highlightIntensity = 1.5F;
    [Tooltip("Цвет объекта после попадания")]
    [SerializeField] private Color targetObjectColor = Color.red;
    [SerializeField] private LineRenderer lineRenderer;

    private RaycastHit hit;
    /// <summary>
    /// Render объекта в который попали
    /// </summary>
    private Renderer currentObjectRenderer;
    private Renderer previousObjectRenderer;

    private int? previousObjectHashCode;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        UpdateDestructiveLazer();
    }

    private void UpdateDestructiveLazer()
    {
        if (BuildingController.IsDestroyMode == false)
        {
            // Окончательно сбрасываем остатки цветов материала
            if (previousObjectRenderer != null)
                ResetHighlight();

            return;
        }

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        lineRenderer.SetPosition(0, transform.position);

        if (Physics.Raycast(ray, out hit, laserRange))
        {
            // Если луч попал в объект, устанавливаем конец луча в точку пересечения
            lineRenderer.SetPosition(1, hit.point);
            //transform.LookAt(hit.point);

            var hitedGameObject = hit.collider.gameObject;
            if (hitedGameObject.GetComponent<RemovableObject>() == null)
            {
                ResetHighlight();
                return;
            }

            currentObjectRenderer = hitedGameObject.GetComponent<Renderer>();

            if (hitedGameObject.GetHashCode() == previousObjectHashCode
                || previousObjectHashCode == null)
            {
                HighlightObject();
            }
            else
            {
                ResetHighlight();
            }

            previousObjectHashCode = hitedGameObject.GetHashCode();
            previousObjectRenderer = currentObjectRenderer;
            if (Input.GetButtonDown("Fire1"))
            {
                Destroy(hitedGameObject);
            }
        }
        else
        {
            Vector3 endPoint = ray.origin + ray.direction * laserRange;
            lineRenderer.SetPosition(1, endPoint);
            transform.LookAt(endPoint);
        }
    }

    /// <summary>
    /// Возвращаем исходный цвет объекта
    /// </summary>
    private void HighlightObject()
    {
        if (currentObjectRenderer != null)
        {
            currentObjectRenderer.material.color = targetObjectColor * highlightIntensity;
        }
    }

    /// <summary>
    /// Возвращаем исходный цвет объекта
    /// </summary>
    private void ResetHighlight()
    {
        if (previousObjectRenderer != null)
        {
            previousObjectRenderer.material.color = new Color(1, 1, 1);
            previousObjectRenderer = null;
        }
    }
}
