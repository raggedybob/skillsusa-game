using UnityEngine;

[CreateAssetMenu(fileName = "CombatData", menuName = "Scriptable Objects/CombatData")]
public class CombatData : ScriptableObject
{
    [Header("Base Stats")]
    public string glepName;
    public float baseHealth;
    public float baseAttack;
    public float baseRange;
    public float baseAttackSpeed;
    public float dropRate;
    public GameObject glepPrefab;

    [Header("Visuals")]
    public Sprite sprite;

    [Header("Items")]
    public ItemData item1;
    public ItemData item2;
    public ItemData item3;
}