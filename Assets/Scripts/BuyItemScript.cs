using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyItemScript : MonoBehaviour
{
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        ItemData item = selectedItemUIScript.Instance.CurrentItem;
        if (item == null) return;
        if (!MaterialManager.Instance.CanAfford(item.baseCosts)) 
        {
            Debug.Log("Cannot afford item: " + item.itemName);
            return;
        }   
        MaterialManager.Instance.Spend(item.baseCosts);
        InventoryManager.Instance.AddItem(item);
        item.OnPurchase();
    }
}

