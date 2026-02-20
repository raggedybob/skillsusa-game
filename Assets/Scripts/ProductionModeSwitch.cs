using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProductionToggleButton : MonoBehaviour
{
    [SerializeField] private TMP_Text buttonText;
    [SerializeField] private Image buttonImage;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Toggle);
        UpdateText();
    }

    private void Toggle()
    {
        ProductionManager.Instance.ToggleMode();
        UpdateText();
    }

    private void UpdateText()
    {
        if (ProductionManager.Instance.CurrentMode == ProductionMode.Materials)
        {
            buttonText.text = "Production Mode: Materials";
            buttonImage.color = Color.lightBlue;
        }
        else
        {
            buttonText.text = "Production Mode: Money";
            buttonImage.color = Color.lightGreen;
        }           
    }
}
