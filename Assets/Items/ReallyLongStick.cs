using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Items/ReallyLongStick")]
public class ReallyLongStick : ItemData
{
    public override void OnPurchase()
    {
        Debug.Log("Really Long Stick added to inventory!");
    }

    public override void OnEquip(GlepCombatScript glep)
    {
        glep.bonusRange += bonusRange; // just adds the +100 hp
    }

    public override void OnUnequip(GlepCombatScript glep)
    {
        glep.bonusRange -= bonusRange;
    }
}