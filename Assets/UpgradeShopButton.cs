using UnityEngine;
using UnityEngine.UI;

public class UpgradeShopButton : MonoBehaviour
{
    [SerializeField] private UpgradeData upgradeData;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SelectUpgrade);
    }

    private void SelectUpgrade()
    {
        UpgradeDisplayScript.Instance.UpdateDisplay(upgradeData);
    }
}
