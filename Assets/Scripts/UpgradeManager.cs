using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance;

    public float globalHealthBonus = 0f;
    public float globalAttackBonus = 0f;
    public float passiveIncomePerSecond = 0f;
    public int unlockedHotbarSlots = 5;

    private Dictionary<UpgradeData, int> upgradeTiers = new Dictionary<UpgradeData, int>();

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Update()
    {

    }

    public int GetTier(UpgradeData upgrade)
    {
        return upgradeTiers.ContainsKey(upgrade) ? upgradeTiers[upgrade] : 0;
    }

    public bool IsMaxTier(UpgradeData upgrade)
    {
        return GetTier(upgrade) >= upgrade.maxTier;
    }

    public void ApplyUpgrade(UpgradeData upgrade)
    {
        int currentTier = GetTier(upgrade);
        if (IsMaxTier(upgrade)) return;

        float value = upgrade.GetCurrentValue(currentTier);

        switch (upgrade.upgradeType)
        {
            case UpgradeType.UnlockHotbarSlot:
                unlockedHotbarSlots++;
                CombatInventoryManager.Instance.UnlockSlot(unlockedHotbarSlots - 1);
                break;
            case UpgradeType.GlobalHealthBonus:
                globalHealthBonus += value;
                break;
            case UpgradeType.GlobalAttackBonus:
                globalAttackBonus += value;
                break;
            case UpgradeType.UnlockGambling:
                GLEPMachineScript.Instance.UnlockGambling();
                break;
        }

        upgradeTiers[upgrade] = currentTier + 1;
    }
}