using UnityEngine;
using System.Collections.Generic;   
public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;
    public List<BuildingState> factoryBuildings = new List<BuildingState>();
    public HashSet<Vector2Int> occupiedTiles = new HashSet<Vector2Int>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
}
