using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class CameraMovementScript : MonoBehaviour
{
    [SerializeField] private float dragSpeed = 1f;

    private Vector3 lastMousePosition;
    public bool canMove = false;
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
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            Vector3 move = new Vector3(-delta.x, -delta.y, 0f) * dragSpeed * Time.deltaTime;

            transform.Translate(move);
            transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minX, maxX),
            Mathf.Clamp(transform.position.y, minY, maxY),
            transform.position.z
);

            lastMousePosition = Input.mousePosition;
        }
        float scroll = Input.mouseScrollDelta.y;

        if (scroll != 0)
        {
            cam.orthographicSize -= scroll * zoomSpeed * Time.deltaTime;
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);
        }

    }
}

