using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Items/GlepRoids")]
public class GlepRoids : ItemData
{
    public override void OnPurchase()
    {
        Debug.Log("GlepRoids added to inventory!");
        // add to inventory later
    }

    public override void OnEquip(GlepCombatScript glep)
    {
        glep.bonusAttack += bonusAttack;
        glep.bonusHealth += bonusHealth;    
        glep.bonusRange += bonusRange;
        glep.bonusAttackSpeed += bonusAttackSpeed;  
    }

    public override void OnUnequip(GlepCombatScript glep)
    {
        glep.bonusAttack -= bonusAttack;
        glep.bonusHealth -= bonusHealth;  
        glep.bonusRange -= bonusRange;
        glep.bonusAttackSpeed -= bonusAttackSpeed;  
    }
}