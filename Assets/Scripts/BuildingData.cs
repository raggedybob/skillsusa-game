using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Buildings/Building Data")]
public class BuildingData : ScriptableObject
{
    public string buildingName;
    [TextArea] public string description;
    public Sprite icon;
    public GameObject prefab;
    public Vector2Int size = Vector2Int.one;
    public List<MaterialCost> costs;
}

