using UnityEngine;
using UnityEngine.UI;
public class OpenBuildMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject createMenu;
    [SerializeField] private GameObject shopMenu;
    [SerializeField] private Vector3 openPosition;
    [SerializeField] private Vector3 closedPosition;
    [SerializeField] private AudioClip openBuildSoundClip;
    [SerializeField] private AudioClip closeBuildSoundClip;

    public void Awake()
    {
        createMenu.transform.localPosition = closedPosition;
    }

    public void OnClick()
    {
        if (!BuildManagerScript.Instance.IsBuildMode)
        {
            OpenBuildMenu();
            SoundFXManager.Instance.PlaySFX(openBuildSoundClip, transform, 1f);
        }
        else
        {
            CloseBuildMenu();
            SoundFXManager.Instance.PlaySFX(closeBuildSoundClip, transform, 1f);
        }
    }

    public void OpenBuildMenu()
    {
        Debug.Log("Opening Build Menu");
        shopMenu.SetActive(false);
        BuildManagerScript.Instance.EnterBuildMode();
        createMenu.transform.localPosition = openPosition;
    }

    public void CloseBuildMenu()
    {
        Debug.Log("Closing Build Menu");
        BuildManagerScript.Instance.ExitBuildMode();
        BuildManagerScript.Instance.ClearSelectedBuilding();
        createMenu.transform.localPosition = closedPosition;
    }
}
