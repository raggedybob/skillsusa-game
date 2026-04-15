using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public float baseHealth;
    public float baseAttack;
    public float baseRange;
    public float baseAttackSpeed;
    public float moveSpeed;
    public Sprite sprite;
    public GameObject enemyPrefab;
}