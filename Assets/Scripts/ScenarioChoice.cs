using UnityEngine;

[System.Serializable]
public class ScenarioChoice
{
    public string choiceText;

    public MaterialCost[] requiredCosts;   // must exist

    public MaterialCost[] materialChanges;  // optional future use

    public ItemData itemReward;

    public int moneyChange;

    public ScenarioData nextScenario;
}