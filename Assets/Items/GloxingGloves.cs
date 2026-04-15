using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Items/GloxingGloves")]
public class GloxingGloves : ItemData
{
    public override void OnPurchase()
    {
        Debug.Log("Gloxing Gloves added to inventory!");
        // add to inventory later
    }

    public override void OnEquip(GlepCombatScript glep)
    {
        glep.bonusAttack += bonusAttack;
    }

    public override void OnUnequip(GlepCombatScript glep)
    {
        glep.bonusAttack -= bonusAttack;
    }
}