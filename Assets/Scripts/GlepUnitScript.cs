using System.Collections.Generic;
using UnityEngine;

public enum GlepState { Factory, Combat, Transitioning }

public class GlepUnitScript : MonoBehaviour
{
    public CombatData data;
    public GlepState currentState;
    public List<ItemData> equippedItems = new List<ItemData>();

    public GlepCombatScript combatScript;
    [SerializeField] private PhysicsDrag2D factoryScript1;
    [SerializeField] private GloopShakeScript factoryScript2;

    void Awake()
    {
        combatScript = GetComponent<GlepCombatScript>();
    }

    public void EnterFactory(Vector3 factoryPos)
    {
        currentState = GlepState.Factory;
        transform.position = factoryPos;
        factoryScript1.enabled = true;
        factoryScript2.enabled = true;
        combatScript.enabled = false;
    }

    public void EnterCombat(Vector3 combatPos)
    {
        currentState = GlepState.Combat;
        transform.position = combatPos;
        combatScript.enabled = true;
        factoryScript1.enabled = false;
        factoryScript2.enabled = false;
        combatScript.enabled = false;
    }

    public bool IsInCombat() => currentState == GlepState.Combat;
}