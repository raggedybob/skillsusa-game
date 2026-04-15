using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Image slotBackground;
    [SerializeField] private Sprite commonColor;
    [SerializeField] private Sprite rareColor;
    [SerializeField] private Sprite legendaryColor;

    private ItemData currentItem;

    public void OnClicked()
    {
        InventoryUIScript.Instance.SelectItem(currentItem);
    }

    public void Setup(ItemData item, int quantity)
    {
        currentItem = item;
        itemIcon.sprite = item.icon;
        quantityText.text = quantity.ToString();

        slotBackground.sprite = item.rarity switch
        {
            ItemRarity.Common => commonColor,
            ItemRarity.Rare => rareColor,
            ItemRarity.Legendary => legendaryColor,
            _ => commonColor
        };
        GetComponent<Button>().onClick.AddListener(OnClicked);
    }
}