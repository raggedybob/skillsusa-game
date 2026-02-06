using UnityEngine;
using UnityEngine.UI; // Required for the Button component
using TMPro; // Required for TextMeshProUGUI or other TMP components

public class MyButtonController : MonoBehaviour
{
    [SerializeField] private Button myButton; // Drag the TMP button here in the Inspector
    [SerializeField] private TextMeshProUGUI myText; // Optional: reference other TMP elements

    void Awake()
    {
        myButton.onClick.AddListener(OnButtonClicked);
    }
    private void OnButtonClicked()
    {
        Debug.Log("Dropping down buildings menu.");
        
    }
}

