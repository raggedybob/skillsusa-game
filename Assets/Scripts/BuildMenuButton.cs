using UnityEngine;
using UnityEngine.UI;

public class BuildMenuButton : MonoBehaviour
{
    [SerializeField] private BuildingData buildingData;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SelectBuilding);
        //button.onClick.AddListener(() => AudioManagerScript.Instance.Play("Click"));    
    }

    private void SelectBuilding()
    {
        BuildManagerScript.Instance.SetSelectedBuilding(buildingData);
    }
}
