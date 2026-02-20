using UnityEngine;

public enum ProductionMode
{
    Materials,
    Money
}

public class ProductionManager : MonoBehaviour
{
    public static ProductionManager Instance;

    public ProductionMode CurrentMode = ProductionMode.Materials;

    private void Awake()
    {
        Instance = this;
    }

    public void ToggleMode()
    {
        if (CurrentMode == ProductionMode.Materials)
            CurrentMode = ProductionMode.Money;
        else
            CurrentMode = ProductionMode.Materials;

        Debug.Log("Production Mode: " + CurrentMode);
    }
}

