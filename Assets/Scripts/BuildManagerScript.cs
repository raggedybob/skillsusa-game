using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildManagerScript : MonoBehaviour
{
    public static BuildManagerScript Instance;
    public GameObject selectedBuilding;
    private Dictionary<Vector2Int, GameObject> occupiedTiles =
    new Dictionary<Vector2Int, GameObject>();

    private void Awake()
    {
        selectedBuilding = null;
        Instance = this;
    }

    public bool IsBuildMode { get; private set; }

    public void EnterBuildMode()
    {
        IsBuildMode = true;
        Debug.Log("Entered Build Mode");
    }

    public void ExitBuildMode()
    {
        IsBuildMode = false;
        Debug.Log("Exited Build Mode");
    }
    private Vector2Int WorldToGrid(Vector2 worldPos)
    {
        return new Vector2Int(
            Mathf.RoundToInt(worldPos.x),
            Mathf.RoundToInt(worldPos.y)
        );
    }

    public void TryPlaceBuilding(Vector2 worldPos)
    {
        Vector2Int gridPos = WorldToGrid(worldPos);

        if (occupiedTiles.ContainsKey(gridPos))
        {
            Debug.Log("Tile already occupied!");
            return;
        }

        Vector3 spawnPos = new Vector3(gridPos.x, gridPos.y, 0f);

        GameObject building = Instantiate(selectedBuilding, spawnPos, Quaternion.identity);

        occupiedTiles.Add(gridPos, building);
    }

}
