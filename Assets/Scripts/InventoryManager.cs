using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    // tracks how many of each item the player owns
    private Dictionary<ItemData, int> inventory = new Dictionary<ItemData, int>();

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void AddItem(ItemData item)
    {
        if (inventory.ContainsKey(item))
            inventory[item]++;
        else
            inventory[item] = 1;

        InventoryUIScript.Instance.RefreshUI();
    }

    public void RemoveItem(ItemData item)
    {
        if (!inventory.ContainsKey(item)) return;
        inventory[item]--;
        if (inventory[item] <= 0)
            inventory.Remove(item);

        InventoryUIScript.Instance.RefreshUI();
    }

    public int GetQuantity(ItemData item)
    {
        return inventory.ContainsKey(item) ? inventory[item] : 0;
    }

    public Dictionary<ItemData, int> GetAll() => inventory;
}