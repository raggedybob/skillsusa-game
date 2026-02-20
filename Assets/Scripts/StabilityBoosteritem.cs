using UnityEngine;
using TMPro;

[CreateAssetMenu(menuName = "Items/Stability Booster")]
public class StabilityBoosterItem : ItemData
{
    public override void OnPurchase()
    {
        var costs = GetCosts();

        if (!MaterialManager.Instance.CanAfford(costs))
        {
            Debug.Log("Not enough money!");
            return;
        }

        MaterialManager.Instance.Spend(costs);

        StabilityManager.Instance.IncreaseStability(700);

        foreach (var cost in costs)
        {
            int currentCost = cost.amount *= 2;
        }
    }
}

