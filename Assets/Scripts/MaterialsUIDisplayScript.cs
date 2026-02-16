using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class MaterialsUIDisplayScript : MonoBehaviour
{
    [System.Serializable]
    public struct MaterialUI
    {
        public MaterialType type;
        public TMP_Text text;
    }

    [SerializeField] private List<MaterialUI> materialTexts;
    private void Start()
    {
        MaterialManager.Instance.OnMaterialsChanged += UpdateCounts;
        UpdateCounts();
    }

    public void UpdateCounts()
    {
        foreach (var entry in materialTexts)
        {
            entry.text.text =
                MaterialManager.Instance.GetAmount(entry.type).ToString();
        }
    }
}

