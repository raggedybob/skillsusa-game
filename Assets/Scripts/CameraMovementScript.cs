using UnityEngine;
public class CameraMovementScript : MonoBehaviour
{

    private Vector3 lastMousePosition;
    public bool canMove = false;
    private Vector3 dragOrigin;
    [SerializeField] private Camera cam;
    [SerializeField] private float zoomSpeed = 5f;
    [SerializeField] private float minZoom = 3f;
    [SerializeField] private float maxZoom = 10f;
    [SerializeField] private float factoryMinX = -10f; 
    [SerializeField] private float factoryMaxX = 10f;
    [SerializeField] private float factoryMinY = -10f;
    [SerializeField] private float factoryMaxY = 1000f;
    [SerializeField] private float outsideMinX = -1000f;
    [SerializeField] private float outsideMaxX = 1000f;
    [SerializeField] private float outsideMinY = -1000f;
    [SerializeField] private float outsideMaxY = 1000f;
    void Awake()
    {
        if (this.gameObject.scene.name == "FactoryScene")
        {
            canMove = true;
        }
        else
        {
            canMove = false;
        }
    }
    void Update()
    {
        if (EscapePauseMenuScript.isPaused) return;
        if (Input.GetMouseButtonDown(1))
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 currentPos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 difference = dragOrigin - currentPos;

            transform.position += difference;

            if (!SwitchFactoryOutsideScript.isOutside)
            {
                transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, factoryMinX, factoryMaxX),
                Mathf.Clamp(transform.position.y, factoryMinY, factoryMaxY),
                transform.position.z);
            }
            else
            {
                transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, outsideMinX, outsideMaxX),
                Mathf.Clamp(transform.position.y, outsideMinY, outsideMaxY),
                transform.position.z);
            }
        }
        
        float scroll = Input.mouseScrollDelta.y;
        
        if (scroll != 0)
        {
            cam.orthographicSize -= scroll * zoomSpeed * Time.deltaTime;
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);
        }
    }
}

