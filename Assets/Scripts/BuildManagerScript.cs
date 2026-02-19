using System.Collections.Generic;
using System.Linq;
using System.Resources;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildManagerScript : MonoBehaviour
{
    public static BuildManagerScript Instance;
    public GameObject selectedBuilding;
    private Dictionary<Vector2Int, BuildingData> occupiedTiles =
    new Dictionary<Vector2Int, BuildingData>();

    private Dictionary<Vector2Int, GameObject> placedObjects =
        new Dictionary<Vector2Int, GameObject>();

    private GameObject currentPreview;
    private SpriteRenderer previewRenderer;
    public event System.Action<BuildingData> OnSelectedBuildingChanged;
    [SerializeField] private int minX = -20;
    [SerializeField] private int maxX = 20;
    [SerializeField] private int minY = 0;
    [SerializeField] private int maxY = 10;
    public bool IsSellMode { get; private set; }

    private void Awake()
    {
        selectedBuilding = null;
        Instance = this;
        IsSellMode = false;
    }

    public bool IsBuildMode { get; private set; }

    public void EnterBuildMode()
    {
        IsBuildMode = true;
        IsSellMode = false;
    }

    public void ExitBuildMode()
    {
        IsBuildMode = false;
    }
    private void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2Int gridPos = WorldToGrid(mousePos);

        if (IsSellMode && Mouse.current.leftButton.wasPressedThisFrame)
        {
            TrySellBuilding(gridPos);
        }

        if (!IsBuildMode || SelectedBuilding == null || currentPreview == null)
            return;

        gridPos.x = Mathf.Clamp(gridPos.x, minX, maxX - SelectedBuilding.size.x + 1);
        gridPos.y = Mathf.Clamp(gridPos.y, minY, maxY - SelectedBuilding.size.y + 1);


        Vector3 snappedPosition = new Vector3(gridPos.x, gridPos.y, 0f);
        currentPreview.transform.position = snappedPosition;

        bool canPlace = CanPlace(gridPos);

        previewRenderer.color = canPlace
            ? new Color(0, 1, 0, 0.5f)
            : new Color(1, 0, 0, 0.5f);
    }

    private Vector2Int WorldToGrid(Vector2 worldPos)
    {
        return new Vector2Int(
            Mathf.RoundToInt(worldPos.x),
            Mathf.RoundToInt(worldPos.y)
        );
    }
    private bool CanPlace(Vector2Int origin)
    {
        if (origin.x < minX || origin.y < minY)
            return false;

        if (origin.x + SelectedBuilding.size.x - 1 > maxX)
            return false;

        if (origin.y + SelectedBuilding.size.y - 1 > maxY)
            return false;

        // Check if area is free
        for (int x = 0; x < SelectedBuilding.size.x; x++)
        {
            for (int y = 0; y < SelectedBuilding.size.y; y++)
            {
                Vector2Int checkPos = new Vector2Int(origin.x + x, origin.y + y);

                if (occupiedTiles.ContainsKey(checkPos))
                    return false;
            }
        }

        // Support rule switch
        if (SelectedBuilding.isBlock)
            return HasAdjacentSupport(origin);
        else
            return HasFullBottomSupport(origin);
    }


    public BuildingData SelectedBuilding { get; private set; }

    public void SetSelectedBuilding(BuildingData data)
    {
        SelectedBuilding = data;

        if (currentPreview != null)
            Destroy(currentPreview);

        currentPreview = Instantiate(data.prefab);
        previewRenderer = currentPreview.GetComponent<SpriteRenderer>();

        Color c = previewRenderer.color;
        c.a = 0.5f;
        previewRenderer.color = c;

        OnSelectedBuildingChanged?.Invoke(data);   
    }

    public void TryPlaceBuilding(Vector2 worldPos)
    {
        if (SelectedBuilding == null) return;

        Vector2Int gridPos = WorldToGrid(worldPos);

        if (!CanPlace(gridPos))
            return;

        if (!MaterialManager.Instance.CanAfford(SelectedBuilding.costs))
        {
            Debug.Log("Not enough resources!");
            return;
        }
        MaterialManager.Instance.Spend(SelectedBuilding.costs);
        Vector3 spawnPos = new Vector3(gridPos.x, gridPos.y, 0f);

        GameObject newBuilding = Instantiate(SelectedBuilding.prefab, spawnPos, Quaternion.identity);

        for (int x = 0; x < SelectedBuilding.size.x; x++)
        {
            for (int y = 0; y < SelectedBuilding.size.y; y++)
            {
                Vector2Int tile = new Vector2Int(gridPos.x + x, gridPos.y + y);

                occupiedTiles[tile] = SelectedBuilding;   
                placedObjects[tile] = newBuilding;        
            }
        }
        InsertGlepScript buildingScript = newBuilding.GetComponent<InsertGlepScript>();
        buildingScript.Initialize(SelectedBuilding, gridPos);
    }
    public void ClearSelectedBuilding()
    {
        SelectedBuilding = null;

        if (currentPreview != null)
        {
            Destroy(currentPreview);
            currentPreview = null;
        }

        OnSelectedBuildingChanged?.Invoke(null);
    }
    private bool HasAdjacentSupport(Vector2Int origin)
    {
        // Floor
        if (origin.y == minY)
            return true;

        // Roof
        if (origin.y + SelectedBuilding.size.y - 1 == maxY)
            return true;

        Vector2Int[] directions =
        {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.right
    };

        for (int x = 0; x < SelectedBuilding.size.x; x++)
        {
            for (int y = 0; y < SelectedBuilding.size.y; y++)
            {
                Vector2Int tile = new Vector2Int(origin.x + x, origin.y + y);

                foreach (var dir in directions)
                {
                    if (occupiedTiles.ContainsKey(tile + dir))
                        return true;
                }
            }
        }

        return false;
    }

    private bool HasFullBottomSupport(Vector2Int origin)
    {
        if (origin.y == minY)
            return true;

        for (int x = 0; x < SelectedBuilding.size.x; x++)
        {
            Vector2Int belowTile = new Vector2Int(origin.x + x, origin.y - 1);

            if (!occupiedTiles.TryGetValue(belowTile, out BuildingData data))
                return false;

            if (!data.isBlock)
                return false;
        }

        return true;
    }

    public void EnterSellMode()
    {
        IsSellMode = true;
        IsBuildMode = false;
        ClearSelectedBuilding();
    }

    public void ExitSellMode()
    {
        IsSellMode = false;
    }

    private void TrySellBuilding(Vector2Int gridPos)
    {
        if (!occupiedTiles.ContainsKey(gridPos))
            return;

        if (!placedObjects.TryGetValue(gridPos, out GameObject building))
            return;


        InsertGlepScript script = building.GetComponent<InsertGlepScript>();
        BuildingData data = script.GetBuildingData();

        foreach (var cost in data.costs)
        {
            MaterialManager.Instance.Add(
                cost.type,
                cost.amount / 2
            );
        }

        for (int x = 0; x < data.size.x; x++)
        {
            for (int y = 0; y < data.size.y; y++)
            {
                Vector2Int tile = new Vector2Int(
                    script.GridPosition.x + x,
                    script.GridPosition.y + y
                );

                occupiedTiles.Remove(tile);
                placedObjects.Remove(tile);
                GameStateManager.Instance.occupiedTiles.Remove(tile);
            }
        }
        Destroy(building);
    }
}