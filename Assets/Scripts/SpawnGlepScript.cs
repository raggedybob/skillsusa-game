using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Spawn Glep")]
public class SpawnGlepItem : ItemData
{
    [SerializeField] private GameObject glep;
    public override void OnPurchase()
    {
        var costs = GetCosts();

        if (!MaterialManager.Instance.CanAfford(costs))
        {
            Debug.Log("Not enough money!");
            return;
        }

        MaterialManager.Instance.Spend(costs);

        Instantiate(glep, new Vector3(0f,0f, 0f), Quaternion.identity);

        foreach (var cost in costs)
        {
            cost.amount *= 2;
        }
    }
}