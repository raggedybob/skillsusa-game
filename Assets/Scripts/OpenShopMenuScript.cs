using UnityEngine;
using UnityEngine.UI;
public class OpenShopMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject createMenu;
    [SerializeField] private GameObject combatBar;
    [SerializeField] private Vector3 openPosition;
    [SerializeField] private Vector3 closedPosition;
    [SerializeField] private Vector3 combatOpenPosition;
    [SerializeField] private Vector3 combatClosedPosition;
    [SerializeField] private GameObject shopMenu;
    [SerializeField] private GameObject buildMenu;
    [SerializeField] private GameObject upgradeMenu;
    [SerializeField] private GameObject itemsMenu;
    [SerializeField] private GameObject inventoryMenu;
    [SerializeField] private GameObject BuildingMenuBar;
    [SerializeField] private GameObject UpgradeMenuBar;
    [SerializeField] private AudioClip openShopSoundClip;

    public void OnClick()
    {
        if (EscapePauseMenuScript.isPaused) return;
        SoundFXManager.Instance.PlaySFX(openShopSoundClip, transform, 1f);
        BuildManagerScript.Instance.EnterBuildMode();
        if (createMenu.transform.localPosition == closedPosition)
        {
            createMenu.transform.localPosition = openPosition;
            combatBar.transform.localPosition = combatOpenPosition;
        }
        else if (inventoryMenu.activeSelf)
        {
            createMenu.transform.localPosition = openPosition;
            combatBar.transform.localPosition = combatOpenPosition;
        }
        else
        {
            createMenu.transform.localPosition = closedPosition;
            combatBar.transform.localPosition = combatClosedPosition;
            BuildManagerScript.Instance.ExitBuildMode();
        }
        BuildingMenuBar.SetActive(true);
        UpgradeMenuBar.SetActive(false);
        shopMenu.SetActive(true);
        buildMenu.SetActive(true);
        upgradeMenu.SetActive(false);
        itemsMenu.SetActive(false);
        inventoryMenu.SetActive(false);
    }     
}
