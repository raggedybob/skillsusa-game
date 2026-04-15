using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeDisplayScript : MonoBehaviour
{
    public static UpgradeDisplayScript Instance;

    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private TMP_Text costText;
    [SerializeField] private TMP_Text tierText;

    public UpgradeData CurrentUpgrade { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void UpdateDisplay(UpgradeData upgrade)
    {
        CurrentUpgrade = upgrade;
        nameText.text = upgrade.upgradeName;
        descriptionText.text = upgrade.description;

        int tier = UpgradeManager.Instance.GetTier(upgrade);

        if (UpgradeManager.Instance.IsMaxTier(upgrade))
        {
            tierText.text = "MAXED";
            costText.text = "";
        }
        else
        {
            List<MaterialCost> costs = upgrade.GetCurrentCosts(tier);
            string costString = "";
            foreach (MaterialCost cost in costs)
                costString += $"{cost.amount} {cost.type}  ";
            costText.text = costString.Trim();
        }
    }
}