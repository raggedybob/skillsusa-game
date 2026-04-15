using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProductionToggleButton : MonoBehaviour
{
    [SerializeField] private TMP_Text buttonText;
    [SerializeField] private Image buttonImage;
    [SerializeField] private AudioClip toggleSoundClip;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Toggle);
        UpdateText();
    }

    private void Toggle()
    {
        SoundFXManager.Instance.PlaySFX(toggleSoundClip, transform, 1f);
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
