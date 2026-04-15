using System.Collections.Generic;
using UnityEngine;
using static MaterialsUIDisplayScript;

public class MaterialManager : MonoBehaviour
{
    public static MaterialManager Instance;

    [SerializeField] private Dictionary<MaterialType, int> resources =
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
        foreach (MaterialType type in System.Enum.GetValues(typeof(MaterialType)))
        {
            resources[type] = 0;
        }
    }


    public void Add(MaterialType type, int amount)
    {
        if (type != MaterialType.Money)
        {
            
            int newValue = Mathf.Max(0, resources[type] + amount);
            resources[type] = newValue;
            OnMaterialsChanged?.Invoke();
        }
        else
        {
            // money CAN go negative
            resources[type] += amount;
            OnMaterialsChanged?.Invoke();
        }
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

    public bool Spend(List<MaterialCost> costs)
    {
        if (!CanAfford(costs))
            return false;

        foreach (var cost in costs)
        {
            Add(cost.type, -cost.amount);
            OnMaterialsChanged?.Invoke();
        }

        return true;
    }

    public int GetAmount(MaterialType type)
    {
        return resources[type];
    }
    public bool Remove(MaterialType type, int amount)
    {
        if (!resources.ContainsKey(type)) return false;
        if (resources[type] < amount) return false;

        resources[type] -= amount;
        OnMaterialsChanged?.Invoke();
        return true;
    }
}
