using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class CameraMovementScript : MonoBehaviour
{

    private Vector3 lastMousePosition;
    public bool canMove = false;
    private Vector3 dragOrigin;
    [SerializeField] private Camera cam;
    [SerializeField] private float zoomSpeed = 5f;
    [SerializeField] private float minZoom = 3f;
    [SerializeField] private float maxZoom = 10f;
    [SerializeField] private float minX = -10f; 
    [SerializeField] private float maxX = 10f;
    [SerializeField] private float minY = -10f;
    [SerializeField] private float maxY = 10f;
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
        if (Input.GetMouseButtonDown(1))
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 currentPos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 difference = dragOrigin - currentPos;

            transform.position += difference;

            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, minX, maxX),
                Mathf.Clamp(transform.position.y, minY, maxY),
                transform.position.z
            );
        }
        float scroll = Input.mouseScrollDelta.y;

        if (scroll != 0)
        {
            cam.orthographicSize -= scroll * zoomSpeed * Time.deltaTime;
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);
        }

    }
}

