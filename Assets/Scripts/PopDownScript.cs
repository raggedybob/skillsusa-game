using UnityEngine;

public class PopDownScript : MonoBehaviour
{
    [SerializeField] private GameObject createMenu;
    [SerializeField] private Vector3 closedPosition;

    public void OnClick()
    {
        if (EscapePauseMenuScript.isPaused) return;
        BuildManagerScript.Instance.ExitBuildMode();
        BuildManagerScript.Instance.ClearSelectedBuilding();
        createMenu.transform.localPosition = closedPosition;
        gameObject.SetActive(false);
    }
}
