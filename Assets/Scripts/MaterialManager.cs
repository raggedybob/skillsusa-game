using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    public static MaterialManager Instance;

    private Dictionary<MaterialType, int> resources =
        new Dictionary<MaterialType, int>();

    public event System.Action OnMaterialsChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        foreach (MaterialType type in System.Enum.GetValues(typeof(MaterialType)))
        {
            resources[type] = 0;
        }
    }


    public void Add(MaterialType type, int amount)
    {
        resources[type] += amount;
        OnMaterialsChanged?.Invoke();
    }

    public bool CanAfford(List<MaterialCost> costs)
    {
        foreach (var cost in costs)
        {
            if (resources[cost.type] < cost.amount)
                return false;
        }

        return true;
    }

    public void Spend(List<MaterialCost> costs)
    {
        foreach (var cost in costs)
        {
            resources[cost.type] -= cost.amount;
            OnMaterialsChanged?.Invoke();
        }
    }

    public int GetAmount(MaterialType type)
    {
        return resources[type];
    }
}
