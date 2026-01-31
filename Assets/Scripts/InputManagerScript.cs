using UnityEngine;
using UnityEngine.InputSystem;
public class InputHandler : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector2 worldPos = mainCamera.ScreenToWorldPoint(mousePos);

        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

        if (hit.collider == null) return;

        Debug.Log($"Clicked on: {hit.collider.name}");  

        // Try to send the message
        IClickable clickable = hit.collider.GetComponent<IClickable>();
        if (clickable != null)
        {
            clickable.OnClicked();
        }
    }
}

