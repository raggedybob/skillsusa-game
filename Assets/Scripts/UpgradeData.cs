using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TierCost
{
    public List<MaterialCost> costs;
}

[CreateAssetMenu(fileName = "UpgradeData", menuName = "Scriptable Objects/UpgradeData")]
public class UpgradeData : ScriptableObject
{
    public string upgradeName;
    [TextArea] public string description;
    public UpgradeType upgradeType;
    public int maxTier = 3;
    public float[] valuesPerTier;
    public TierCost[] costsPerTier; // each tier has a list of material costs

    public float GetCurrentValue(int tier)
    {
        if (tier >= valuesPerTier.Length) return 0f;
        return valuesPerTier[tier];
    }

    public List<MaterialCost> GetCurrentCosts(int tier)
    {
        if (tier >= costsPerTier.Length) return new List<MaterialCost>();
        return costsPerTier[tier].costs;
    }
}

public enum UpgradeType
{
    UnlockHotbarSlot,
    GlobalHealthBonus,
    GlobalAttackBonus,
    UnlockGambling
}