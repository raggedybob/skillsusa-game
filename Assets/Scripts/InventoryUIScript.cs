using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUIScript : MonoBehaviour
{
    public static InventoryUIScript Instance;

    [SerializeField] private Transform contentParent; // scroll view content
    [SerializeField] private GameObject inventorySlotPrefab;
    [SerializeField] private Image selectedIcon;
    [SerializeField] private TMP_Text selectedName;
    [SerializeField] private TMP_Text selectedDescription;
    [SerializeField] private GameObject equipButton;
    [SerializeField] private GameObject hotbarHighlight; // optional: highlight hotbar when equipping
    [SerializeField] private GameObject ClickOnGlepText; // optional: show when equipping
    public ItemData selectedItem; 

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void RefreshUI()
    {
        foreach (Transform child in contentParent)
            Destroy(child.gameObject);

        foreach (var pair in InventoryManager.Instance.GetAll())
        {
            GameObject slot = Instantiate(inventorySlotPrefab, contentParent);
            slot.GetComponent<InventorySlotUI>().Setup(pair.Key, pair.Value);
        }
    }

    public void SelectItem(ItemData item)
    {
        selectedItem = item;
        selectedIcon.sprite = item.icon;
        selectedName.text = item.itemName;
        selectedDescription.text = item.description;
        equipButton.SetActive(true);
    }

    public bool isEquipping = false;

    public void OnEquipClicked()
    {
        if (selectedItem == null) return;
        isEquipping = true;
        hotbarHighlight.SetActive(true);
        ClickOnGlepText.SetActive(true);
        Debug.Log("Select a glep to equip to!");
    }
}
