using UnityEngine;

public class OpenItemMenuScript : MonoBehaviour
{
    [SerializeField] private AudioClip openItemSoundClip;
    [SerializeField] private GameObject buildMenu;
    [SerializeField] private GameObject upgradeMenu;
    [SerializeField] private GameObject itemsMenu;
    [SerializeField] private GameObject BuildingMenuBar;
    [SerializeField] private GameObject UpgradeMenuBar;

    public void OnClick()
    {
        if (EscapePauseMenuScript.isPaused) return;
        BuildManagerScript.Instance.ExitBuildMode();
        BuildingMenuBar.SetActive(true);
        UpgradeMenuBar.SetActive(false);
        itemsMenu.SetActive(true);
        upgradeMenu.SetActive(false);
        buildMenu.SetActive(false);
        SoundFXManager.Instance.PlaySFX(openItemSoundClip, transform, 1f);
    }
}
