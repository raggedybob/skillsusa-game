using UnityEngine;

public class OpenUpgradeMenuScript : MonoBehaviour
{
    [SerializeField] private AudioClip openUpgradeSoundClip;
    [SerializeField] private GameObject buildMenu;
    [SerializeField] private GameObject upgradeMenu;
    [SerializeField] private GameObject itemsMenu;
    [SerializeField] private GameObject buildingMenuBar;
    [SerializeField] private GameObject upgradeMenuBar;

    public void OnClick()
    {
        if (EscapePauseMenuScript.isPaused) return;
        BuildManagerScript.Instance.ExitBuildMode();
        buildingMenuBar.SetActive(false);
        upgradeMenuBar.SetActive(true);
        upgradeMenu.SetActive(true);
        buildMenu.SetActive(false);
        itemsMenu.SetActive(false);
        SoundFXManager.Instance.PlaySFX(openUpgradeSoundClip, transform, 1f);
    }
}
