using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildManagerScript : MonoBehaviour
{
    public static BuildManagerScript Instance;
    public GameObject selectedBuilding;
    private Dictionary<Vector2Int, GameObject> occupiedTiles =
    new Dictionary<Vector2Int, GameObject>();
    private GameObject currentPreview;
    private SpriteRenderer previewRenderer;
    public event System.Action<BuildingData> OnSelectedBuildingChanged;

    private void Awake()
    {
        selectedBuilding = null;
        Instance = this;
    }

    public bool IsBuildMode { get; private set; }

    public void EnterBuildMode()
    {
        IsBuildMode = true;
    }

    public void ExitBuildMode()
    {
        IsBuildMode = false;
    }
    private void Update()
    {
        if (!IsBuildMode || SelectedBuilding == null || currentPreview == null)
            return;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2Int gridPos = WorldToGrid(mousePos);

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
        for (int x = 0; x < SelectedBuilding.size.x; x++)
        {
            for (int y = 0; y < SelectedBuilding.size.y; y++)
            {
                Vector2Int checkPos = new Vector2Int(origin.x + x, origin.y + y);

                if (occupiedTiles.ContainsKey(checkPos))
                    return false;
            }
        }

        return true;
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

        Vector3 spawnPos = new Vector3(gridPos.x, gridPos.y, 0f);

        GameObject building = Instantiate(SelectedBuilding.prefab, spawnPos, Quaternion.identity);

        for (int x = 0; x < SelectedBuilding.size.x; x++)
        {
            for (int y = 0; y < SelectedBuilding.size.y; y++)
            {
                Vector2Int tile = new Vector2Int(gridPos.x + x, gridPos.y + y);
                occupiedTiles.Add(tile, building);
            }
        }
        MaterialManager.Instance.Spend(SelectedBuilding.costs);
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
}
