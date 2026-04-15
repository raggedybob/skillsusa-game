using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Items/SmallShield")]
public class SmallShield : ItemData
{
    public override void OnPurchase()
    {
        Debug.Log("Small Shield added to inventory!");
    }

    public override void OnEquip(GlepCombatScript glep)
    {
        glep.bonusHealth += bonusHealth; // just adds the +100 hp
    }

    public override void OnUnequip(GlepCombatScript glep)
    {
        glep.bonusHealth -= bonusHealth;
    }
}