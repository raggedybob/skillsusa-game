using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class InputHandler : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    private string FACTORY_SCENE_NAME = "FactoryScene";

    private void Update()
    {
        if (!Mouse.current.leftButton.wasPressedThisFrame)
            return;

        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector2 worldPos = mainCamera.ScreenToWorldPoint(mousePos);

        if (SceneManager.GetActiveScene().name == FACTORY_SCENE_NAME)
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                return;

            if (BuildManagerScript.Instance != null && BuildManagerScript.Instance.IsBuildMode)
            {
                BuildManagerScript.Instance.TryPlaceBuilding(worldPos);
                return;
            }
        }

        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

        if (hit.collider == null) return;

        IClickable clickable = hit.collider.GetComponent<IClickable>();
        clickable?.OnClicked();
    }
}

