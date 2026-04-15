using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InsertGlepScript : MonoBehaviour
{
    [SerializeField] private float hoverTimeRequired = 2f;

    private float currentHoverTime = 0f;
    private PhysicsDrag2D currentGlep;
    private bool isActive = false;
    private bool isCompleting = false;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite activeSprite;
    private BuildingData data;

    [SerializeField] private Slider hoverProgressSlider;
    [SerializeField] private CanvasGroup sliderCanvasGroup;
    [SerializeField] private RectTransform sliderTransform;

    [SerializeField] private float fadeSpeed = 5f;
    [SerializeField] private float scaleAmount = 1.1f; // how big it gets at full

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

    private void Start()
    {
        if (sliderCanvasGroup != null)
            sliderCanvasGroup.alpha = 0f;

        if (hoverProgressSlider != null)
            hoverProgressSlider.value = 0f;

        if (sliderTransform != null)
            sliderTransform.localScale = Vector3.one;
    }

    private void Update()
    {
        // Fade logic
        if (sliderCanvasGroup != null)
        {
            float targetAlpha = (currentHoverTime > 0f || isCompleting) && !isActive ? 1f : 0f;
            sliderCanvasGroup.alpha = Mathf.Lerp(sliderCanvasGroup.alpha, targetAlpha, Time.deltaTime * fadeSpeed);
        }

        // Scale based on progress
        if (sliderTransform != null && hoverProgressSlider != null)
        {
            float scale = Mathf.Lerp(1f, scaleAmount, hoverProgressSlider.value);
            sliderTransform.localScale = new Vector3(scale, scale, scale);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        PhysicsDrag2D glep = other.GetComponent<PhysicsDrag2D>();

        if (glep != null && glep.IsBeingDragged)
        {
            if (isActive || isCompleting)
                return;

            currentHoverTime += Time.deltaTime;

            if (hoverProgressSlider != null)
                hoverProgressSlider.value = Mathf.Clamp01(currentHoverTime / hoverTimeRequired);

            if (currentHoverTime >= hoverTimeRequired && gameObject.GetComponent<SpriteRenderer>().color == Color.white)
            {
                StartCoroutine(CompleteAndActivate(glep));
            }
        }

        if (glep != null && !glep.IsBeingDragged)
        {
            ResetHover();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        PhysicsDrag2D glep = other.GetComponent<PhysicsDrag2D>();

        if (glep != null)
        {
            ResetHover();
        }
    }

    private void ResetHover()
    {
        if (isCompleting) return;

        currentHoverTime = 0f;

        if (hoverProgressSlider != null)
            hoverProgressSlider.value = 0f;
    }

    private IEnumerator CompleteAndActivate(PhysicsDrag2D glep)
    {
        isCompleting = true;

        // Lock at full
        if (hoverProgressSlider != null)
            hoverProgressSlider.value = 1f;

        // Small pause so player sees it filled
        yield return new WaitForSeconds(0.2f);

        ActivateBuilding(glep);

        // Let it fade out while full
        yield return new WaitForSeconds(0.3f);

        // Reset after fade
        currentHoverTime = 0f;
        isCompleting = false;

        if (hoverProgressSlider != null)
            hoverProgressSlider.value = 0f;
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
                MaterialManager.Instance.Add(
                    data.producesMaterial,
                    data.amountPerTick
                );
            }
            else
            {
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
