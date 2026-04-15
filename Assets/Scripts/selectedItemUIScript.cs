using NUnit.Framework.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class selectedItemUIScript : MonoBehaviour
{
    [SerializeField] private Sprite noItemSelected;
    [SerializeField] private Image selectedIcon;
    [SerializeField] private TMP_Text selectedName;
    [SerializeField] private TMP_Text selectedDescription;
    public static selectedItemUIScript Instance;
    public ItemData CurrentItem { get; private set; }


    private void Awake()
    {
        Instance = this;
        UpdateItemDisplay(null);
    }

    public void UpdateItemDisplay(ItemData data)
    {
        CurrentItem = data; 

        if (data == null)
        {
            selectedIcon.sprite = noItemSelected;
            selectedName.text = "None";
            selectedDescription.text = "No item selected";
            return;
        }

        selectedIcon.sprite = data.icon;
        selectedName.text = data.itemName;
        selectedDescription.text = data.description;
    } 
}

