using UnityEngine;

public class DestructiveLaser: MonoBehaviour
{
    [SerializeField] private float laserRange = 100F;
    [SerializeField] private float highlightIntensity = 1.5F;
    [Tooltip("���� ������� ����� ���������")]
    [SerializeField] private Color targetObjectColor = Color.red;
    [SerializeField] private LineRenderer lineRenderer;

    private RaycastHit hit;
    /// <summary>
    /// Render ������� � ������� ������
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
            // ������������ ���������� ������� ������ ���������
            if (previousObjectRenderer != null)
                ResetHighlight();

            return;
        }

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        lineRenderer.SetPosition(0, transform.position);

        if (Physics.Raycast(ray, out hit, laserRange))
        {
            // ���� ��� ����� � ������, ������������� ����� ���� � ����� �����������
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
    /// ���������� �������� ���� �������
    /// </summary>
    private void HighlightObject()
    {
        if (currentObjectRenderer != null)
        {
            currentObjectRenderer.material.color = targetObjectColor * highlightIntensity;
        }
    }

    /// <summary>
    /// ���������� �������� ���� �������
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
