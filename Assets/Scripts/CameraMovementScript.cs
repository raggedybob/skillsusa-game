using UnityEngine;
using UnityEngine.InputSystem;
public class CameraMovementScript : MonoBehaviour
{
    public int edgeBoundary = 20;
    public float scrollSpeed = 10f;
    public bool canMove = false;

    // Update is called once per frame
    void Update()
    {
        if (!canMove) return;
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        if (mousePosition.x >= Screen.width - edgeBoundary)
        {
            transform.Translate(Vector3.right * Time.deltaTime * scrollSpeed);  
        }
        else if (mousePosition.x <= edgeBoundary)
        {
            transform.Translate(Vector3.left * Time.deltaTime * scrollSpeed);
        }
    }

}
