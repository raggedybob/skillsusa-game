using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyUpgradeScript : MonoBehaviour
{
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        UpgradeData upgrade = UpgradeDisplayScript.Instance.CurrentUpgrade;
        if (upgrade == null) return;

        if (UpgradeManager.Instance.IsMaxTier(upgrade))
        {
            Debug.Log("Already maxed!");
            return;
        }

        int tier = UpgradeManager.Instance.GetTier(upgrade);
        List<MaterialCost> costs = upgrade.GetCurrentCosts(tier);

        // check all costs first
        foreach (MaterialCost cost in costs)
        {
            if (MaterialManager.Instance.GetAmount(cost.type) < cost.amount)
            {
                Debug.Log("Not enough materials!");
                return;
            }
        }

        // then spend
        MaterialManager.Instance.Spend(costs);
        UpgradeManager.Instance.ApplyUpgrade(upgrade);
        UpgradeDisplayScript.Instance.UpdateDisplay(upgrade);
    }
}