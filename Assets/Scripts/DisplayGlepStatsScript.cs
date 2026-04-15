using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayGlepStatsScript : MonoBehaviour
{
    [SerializeField] private Sprite noGlepSelected;
    [SerializeField] private Image selectedIcon;
    [SerializeField] private TMP_Text selectedName;
    [SerializeField] private TMP_Text selectedHealth;
    [SerializeField] private TMP_Text selectedAttack;
    [SerializeField] private TMP_Text selectedAttackSpeed;
    [SerializeField] private TMP_Text selectedRange;
    [SerializeField] private Image item1;
    [SerializeField] private Image item2;
    [SerializeField] private Image item3;
    public static DisplayGlepStatsScript Instance;
    [SerializeField] private CanvasGroup glepStatsGroup;
    private void Awake()
    {
        Instance = this;

        gameObject.SetActive(false);
    }

    public void UpdateSelectedDisplay(CombatData data)
    {
        gameObject.SetActive(true);
        selectedName.text = data.glepName;
        selectedIcon.sprite = data.sprite;
        selectedHealth.text = data.baseHealth.ToString();
        selectedAttack.text = data.baseAttack.ToString();
        selectedAttackSpeed.text = data.baseAttackSpeed.ToString();
        selectedRange.text = data.baseRange.ToString();
        item1.gameObject.SetActive(data.item1 != null && data.item1.icon != null);
        item2.gameObject.SetActive(data.item2 != null && data.item2.icon != null);
        item3.gameObject.SetActive(data.item3 != null && data.item3.icon != null);

        if (data.item1 != null && data.item1.icon != null) item1.sprite = data.item1.icon;
        if (data.item2 != null && data.item2.icon != null) item2.sprite = data.item2.icon;
        if (data.item3 != null && data.item3.icon != null) item3.sprite = data.item3.icon;
    }

    public void HideStats()
    {
        gameObject.SetActive(false);
    }
}
