using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScenariosManager : MonoBehaviour
{
    public static ScenariosManager Instance;
    private int currentScenarioIndex = 0;

    [Header("Scenario Pool")]
    [SerializeField] private ScenarioData[] scenarios;

    [Header("UI")]
    [SerializeField] private GameObject scenarioPanel;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private Transform choicesParent;
    [SerializeField] private GameObject choiceButtonPrefab;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ScheduleNextEvent();
    }

    void ScheduleNextEvent()
    {
        float delay = Random.Range(60f, 90f);
        Invoke(nameof(TriggerRandomScenario), delay);
    }

    void TriggerRandomScenario()
    {
        if (scenarios.Length == 0) return;

        // Clamp so we don’t crash
        if (currentScenarioIndex >= scenarios.Length)
            return;

        ScenarioData scenario = scenarios[currentScenarioIndex];
        currentScenarioIndex++;

        ShowScenario(scenario);
    }

    public void ShowScenario(ScenarioData scenario)
    {
        scenarioPanel.SetActive(true);

        titleText.text = scenario.title;
        descriptionText.text = scenario.description;

        // clear old buttons
        foreach (Transform child in choicesParent)
            Destroy(child.gameObject);

        // create new buttons
        foreach (var choice in scenario.choices)
        {
            GameObject btn = Instantiate(choiceButtonPrefab, choicesParent);

            btn.GetComponentInChildren<TMP_Text>().text = choice.choiceText;

            UnityEngine.UI.Button button = btn.GetComponent<UnityEngine.UI.Button>();

                bool canAfford = MaterialManager.Instance.CanAfford(
                new List<MaterialCost>(choice.requiredCosts)
            );

            button.interactable = canAfford;

            button.onClick.AddListener(() =>
            {
                ApplyChoice(choice);
                scenarioPanel.SetActive(false);

                if (choice.nextScenario != null)
                {
                    StartCoroutine(TriggerFollowUp(choice.nextScenario));
                }
                else
                {
                    ScheduleNextEvent();
                }
            });
        }
    }

    void ApplyChoice(ScenarioChoice choice)
    {
        if (choice.requiredCosts != null && choice.requiredCosts.Length > 0)
        {
            MaterialManager.Instance.Spend(
                new List<MaterialCost>(choice.requiredCosts)
            );
        }

        if (choice.moneyChange != 0)
        {
            MaterialManager.Instance.Add(MaterialType.Money, choice.moneyChange);
        }

        if (choice.itemReward != null)
        {
            InventoryManager.Instance.AddItem(choice.itemReward);
            choice.itemReward.OnPurchase();
        }
    }

    IEnumerator TriggerFollowUp(ScenarioData next)
    {
        yield return new WaitForSeconds(60f);
        ShowScenario(next);
    }
}
