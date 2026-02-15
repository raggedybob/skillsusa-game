using UnityEngine;
using UnityEngine.UI;
public class OpenBuildMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject createMenu;
    [SerializeField] private Vector3 openPosition;
    [SerializeField] private Vector3 closedPosition;

    public void Awake()
    {
        createMenu.transform.localPosition = closedPosition;
    }

    public void OnClick()
    {
        Debug.Log("Build Menu Button Clicked");
        if (createMenu.transform.localPosition == closedPosition)
        {
            OpenBuildMenu();
        }
        else
        {
            CloseBuildMenu();
        }
    }

    public void OpenBuildMenu()
    {
        Debug.Log("Opening Build Menu");
        BuildManagerScript.Instance.EnterBuildMode();
        createMenu.transform.localPosition = openPosition;
    }

    public void CloseBuildMenu()
    {
        Debug.Log("Closing Build Menu");
        BuildManagerScript.Instance.ExitBuildMode();
        createMenu.transform.localPosition = closedPosition;
    }
}
