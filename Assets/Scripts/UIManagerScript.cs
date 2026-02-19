using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerScript : MonoBehaviour
{
    [SerializeField] private Sprite noObjectSelected;
    [SerializeField] private Image selectedIcon;
    [SerializeField] private TMP_Text selectedName;
    [SerializeField] private TMP_Text selectedDescription;

    private void Awake()
    {
        UpdateSelectedDisplay(new BuildingData
        {
            buildingName = "None",
            description = "No building selected",
            icon = noObjectSelected,
            prefab = null,
            size = Vector2Int.zero
        });
    }
    private void Start()
    {
        BuildManagerScript.Instance.OnSelectedBuildingChanged += UpdateSelectedDisplay;
    }

    public void UpdateSelectedDisplay(BuildingData data)
    {
        if (data == null)
        {
            selectedIcon.sprite = noObjectSelected;
            selectedName.text = "None";
            selectedDescription.text = "No building selected";
            return;
        }

        selectedIcon.sprite = data.icon;
        selectedName.text = data.buildingName;
        selectedDescription.text = data.description;
    }
}
