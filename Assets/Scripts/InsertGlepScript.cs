using UnityEngine;
using System.Collections;

public class InsertGlepScript : MonoBehaviour
{
    [SerializeField] private float hoverTimeRequired = 2f;

    private float currentHoverTime = 0f;
    private PhysicsDrag2D currentGlep;
    private bool isActive = false;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite activeSprite;
    private BuildingData data;

    public Vector2Int GridPosition { get; private set; }

    public void Initialize(BuildingData data, Vector2Int gridPos)
    {
        this.data = data;
        this.GridPosition = gridPos;
    }

    public BuildingData GetBuildingData()
    {
        return data;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        PhysicsDrag2D glep = other.GetComponent<PhysicsDrag2D>();

        if (glep != null && glep.IsBeingDragged)
        {
            if (isActive)
                return;
            currentHoverTime += Time.deltaTime;

            if (currentHoverTime >= hoverTimeRequired && gameObject.GetComponent<SpriteRenderer>().color == Color.white)
            {
                ActivateBuilding(glep);
            }
        }
        if (!glep.IsBeingDragged)
        {
            currentHoverTime = 0f;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        PhysicsDrag2D glep = other.GetComponent<PhysicsDrag2D>();

        if (glep != null)
        {
            currentHoverTime = 0f;
        }
    }

    private void ActivateBuilding(PhysicsDrag2D glep)
    {
        currentGlep = glep;

        currentGlep.Consume();

        spriteRenderer.sprite = activeSprite;
        isActive = true;
        StartCoroutine(ProductionLoop());
    }

    private IEnumerator ProductionLoop()
    {
        while (isActive)
        {
            yield return new WaitForSeconds(data.productionInterval);

            MaterialManager.Instance.Add(
                data.producesMaterial,
                data.amountPerTick
            );
        }
    }
}
