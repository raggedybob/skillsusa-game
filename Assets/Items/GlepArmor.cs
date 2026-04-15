using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Items/GlepArmor")]
public class GlepArmor : ItemData
{
    public override void OnPurchase()
    {
        Debug.Log("GlepArmor added to inventory!");
    }

    public override void OnEquip(GlepCombatScript glep)
    {
        glep.bonusHealth += bonusHealth; 
    }

    public override void OnUnequip(GlepCombatScript glep)
    {
        glep.bonusHealth -= bonusHealth;
    }
}