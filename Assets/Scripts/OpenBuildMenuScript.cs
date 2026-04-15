using UnityEngine;
using UnityEngine.UI;
public class OpenBuildMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject createMenu;
    [SerializeField] private GameObject combatBar;
    [SerializeField] private GameObject buildingMenuBar;
    [SerializeField] private GameObject UpgradeMenuBar;
    [SerializeField] private GameObject buildMenu;
    [SerializeField] private GameObject upgradeMenu;
    [SerializeField] private GameObject itemsMenu;
    [SerializeField] private AudioClip openBuildSoundClip;
    [SerializeField] private Vector3 openPosition;
    [SerializeField] private Vector3 closedPosition;

    public void OnClick()
    {
        if (EscapePauseMenuScript.isPaused) return;
        OpenBuildMenu();
        buildingMenuBar.SetActive(true);
        UpgradeMenuBar.SetActive(false);
        SoundFXManager.Instance.PlaySFX(openBuildSoundClip, transform, 1f);     
    }

    public void OpenBuildMenu()
    {
        Debug.Log("Opening Build Menu");
        upgradeMenu.SetActive(false);
        itemsMenu.SetActive(false);
        buildMenu.SetActive(true);
        BuildManagerScript.Instance.EnterBuildMode();
        if(createMenu.transform.localPosition == closedPosition)
        {
            createMenu.transform.localPosition = openPosition;
            combatBar.transform.localPosition = new Vector3(combatBar.transform.localPosition.x, combatBar.transform.localPosition.y + 150, combatBar.transform.localPosition.z);
        }
    }
}
