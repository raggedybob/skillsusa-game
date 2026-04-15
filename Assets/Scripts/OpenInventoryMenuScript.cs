using UnityEngine;

public class OpenInventoryMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject createMenu;
    [SerializeField] private GameObject combatBar;
    [SerializeField] private GameObject inventoryMenu;
    [SerializeField] private GameObject shopMenu;
    [SerializeField] private Vector3 openPosition;
    [SerializeField] private Vector3 closedPosition;
    [SerializeField] private Vector3 combatOpenPosition;
    [SerializeField] private Vector3 combatClosedPosition;
    [SerializeField] private GameObject BuildingMenuBar;
    [SerializeField] private GameObject UpgradeMenuBar;
    [SerializeField] private GameObject InventoryMenuBar;
    [SerializeField] private AudioClip openInventorySound;

    public void OnClick()
    {
        if (EscapePauseMenuScript.isPaused) return;
        BuildManagerScript.Instance.ExitBuildMode();
        if (createMenu.transform.localPosition == closedPosition)
        {
            createMenu.transform.localPosition = openPosition;
            combatBar.transform.localPosition = combatOpenPosition;
            InventoryMenuBar.SetActive(true);
            BuildingMenuBar.SetActive(false);
            UpgradeMenuBar.SetActive(false);
            inventoryMenu.SetActive(true);
            shopMenu.SetActive(false);
        }
        else if (createMenu.transform.localPosition == openPosition && inventoryMenu.activeSelf)
        {
            createMenu.transform.localPosition = closedPosition;
            combatBar.transform.localPosition = combatClosedPosition;
            InventoryMenuBar.SetActive(false);
            inventoryMenu.SetActive(false);
        }
        else if (createMenu.transform.localPosition == openPosition && !inventoryMenu.activeSelf)
        {
            inventoryMenu.SetActive(true);
            InventoryMenuBar.SetActive(true);
            BuildingMenuBar.SetActive(false);
            UpgradeMenuBar.SetActive(false);
            shopMenu.SetActive(false);
        }
    }
}
