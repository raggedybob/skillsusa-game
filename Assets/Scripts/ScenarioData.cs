using UnityEngine;

[CreateAssetMenu(fileName = "NewScenario", menuName = "Scenarios/Scenario")]
public class ScenarioData : ScriptableObject
{
    public string title;
    [TextArea] public string description;

    public ScenarioChoice[] choices;
}