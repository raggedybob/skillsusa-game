using System.Collections.Generic;
using UnityEngine;

public abstract class ItemData : ScriptableObject
{
    public string itemName;
    [TextArea] public string description;
    public Sprite icon;
    public List<MaterialCost> baseCosts;

    private List<MaterialCost> runtimeCosts;

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
}


