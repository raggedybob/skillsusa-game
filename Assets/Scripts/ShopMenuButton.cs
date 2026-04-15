using UnityEngine;
using UnityEngine.UI;

public class ShopMenuButton : MonoBehaviour
{
    [SerializeField] private ItemData itemData;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SelectItem);
    }
    private void SelectItem()
    {
        selectedItemUIScript.Instance.UpdateItemDisplay(itemData);
    }   
}
