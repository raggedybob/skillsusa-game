using System.Collections.Generic;
using UnityEngine;

public enum ItemRarity { Common, Rare, Legendary }
public abstract class ItemData : ScriptableObject
{
    public string itemName;
    [TextArea] public string description;
    public Sprite icon;
    public List<MaterialCost> baseCosts;
    private List<MaterialCost> runtimeCosts;

    [Header("Glep Stat Bonuses")]
    public float bonusHealth;
    public float bonusAttack;
    public float bonusRange;
    public float bonusAttackSpeed;

    public List<MaterialCost> GetCosts()
    {
        if (runtimeCosts == null)
        {
            runtimeCosts = new List<MaterialCost>();
            foreach (var cost in baseCosts)
            {
                runtimeCosts.Add(new MaterialCost
                {
                    type = cost.type,
                    amount = cost.amount
                });
            }
        }
        return runtimeCosts;
    }

    public void ResetRuntimeCosts()
    {
        runtimeCosts = null;
    }

    public abstract void OnPurchase();
    public virtual void OnEquip(GlepCombatScript glep) { } // virtual so simple items dont need to override it
    public virtual void OnUnequip(GlepCombatScript glep) { }

    public ItemRarity rarity;
    public int quantity = 1; // max stack size
}


