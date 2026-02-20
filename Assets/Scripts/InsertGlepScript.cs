using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (gameObject.CompareTag("Glortal"))
        {
            SceneManager.LoadScene("EndScene");
        }
        else
        {
            currentGlep = glep;

            currentGlep.Consume();

            spriteRenderer.sprite = activeSprite;
            isActive = true;
            StartCoroutine(ProductionLoop());
        }
    }

    private IEnumerator ProductionLoop()
    {
        while (isActive)
        {
            yield return new WaitForSeconds(data.productionInterval);

            if (ProductionManager.Instance.CurrentMode == ProductionMode.Materials)
            {
                // Normal production
                MaterialManager.Instance.Add(
                    data.producesMaterial,
                    data.amountPerTick
                );
            }
            else
            {
                // Money production instead
                int sellValue = GetSellValue(data.producesMaterial);

                MaterialManager.Instance.Add(
                    MaterialType.Money,
                    data.amountPerTick * sellValue
                );
            }
        }
    }
    private int GetSellValue(MaterialType type)
    {
        switch (type)
        {
            case MaterialType.Gloop: return 2;
            case MaterialType.Glumber: return 5;
            case MaterialType.Gletal: return 10;
            case MaterialType.GlepJuice: return 20;
            default: return 1;
        }
    }
}
