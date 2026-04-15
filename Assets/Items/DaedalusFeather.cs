using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Items/DaedalusFeather")]
public class DaedalusFeather : ItemData
{
    public override void OnPurchase()
    {
        Debug.Log("DaedalusFeather added to inventory!");
    }

    public override void OnEquip(GlepCombatScript glep)
    {
        glep.OnAttack += () => glep.bonusAttackSpeed += 0.1f;
    }

    public override void OnUnequip(GlepCombatScript glep)
    {
        glep.bonusAttackSpeed = 0f; // reset on unequip
    }
}