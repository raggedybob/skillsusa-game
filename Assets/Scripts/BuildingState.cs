using UnityEngine;

[System.Serializable]
public class BuildingState
{
    public BuildingData buildingType;
    public Vector2Int gridPosition;

    public bool isActive;
    public bool hasGlepInside;
}

