using System.Collections;
using UnityEngine;

public class DeployGlepScript : MonoBehaviour
{
    private GameObject previewInstance;
    private bool isPlacing = false;
    [SerializeField] private GameObject pressSpaceToCancelDrop;
    [SerializeField] private CanvasGroup glepStatsGroup;
    [SerializeField] private GameObject youCantPlaceThere;
    [SerializeField] private float factoryLeftBoundaryX = -10f;
    [SerializeField] private float factoryRightBoundaryX = 10f;
    [SerializeField] private float factoryTopBoundaryY = 5f;
    [SerializeField] private float factoryBottomBoundaryY = -5f;
    public static DeployGlepScript Instance;

    void Awake() => Instance = this;

    public void OnClicked()
    {
        if (SwitchFactoryOutsideScript.isOutside)
        {
            if (!YouCantPlaceHereScript.Instance.isWarning)
            {
                StartCoroutine(YouCantPlaceHereScript.Instance.HideYouCantPlaceThere());
            }
            return;
        }

        GlepUnitScript glep = CombatInventoryManager.Instance.GetSelectedGlep();
        if (glep == null) return;

        StartPreview(glep);
    }

    public void StartPreview(GlepUnitScript glep)
    {
        if (previewInstance != null) Destroy(previewInstance);

        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0;
        previewInstance = Instantiate(glep.data.glepPrefab, mouseWorld, Quaternion.identity);

        SpriteRenderer sr = previewInstance.GetComponent<SpriteRenderer>();
        sr.color = new Color(1f, 1f, 1f, 0.5f);

        previewInstance.GetComponent<Collider2D>().enabled = false;
        previewInstance.GetComponent<GloopShakeScript>().enabled = false;
        previewInstance.GetComponent<PhysicsDrag2D>().enabled = false;
        Rigidbody2D rb = previewInstance.GetComponent<Rigidbody2D>();
        if (rb != null) rb.simulated = false;

        glepStatsGroup.alpha = 0f;
        glepStatsGroup.interactable = false;
        glepStatsGroup.blocksRaycasts = false;

        isPlacing = true;
        pressSpaceToCancelDrop.SetActive(true);
    }

    void Update()
    {
        if (!isPlacing || previewInstance == null) return;

        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0;
        previewInstance.transform.position = mouseWorld;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = previewInstance.transform.position;
            bool inFactory = pos.x > factoryLeftBoundaryX && pos.x < factoryRightBoundaryX
                          && pos.y < factoryTopBoundaryY && pos.y > factoryBottomBoundaryY;

            if (inFactory)
            {
                PlaceGlep();
            }
            else
            {
                if (!YouCantPlaceHereScript.Instance.isWarning)
                {
                    StartCoroutine(YouCantPlaceHereScript.Instance.HideYouCantPlaceThere());
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CancelPreview();
        }
    }

    private void PlaceGlep()
    {
        // destroy the outside instance first
        GlepUnitScript outsideGlep = CombatInventoryManager.Instance.GetSelectedGlep();
        if (outsideGlep != null) Destroy(outsideGlep.gameObject);

        previewInstance.GetComponent<SpriteRenderer>().color = Color.white;
        previewInstance.GetComponent<Collider2D>().enabled = true;
        previewInstance.GetComponent<GloopShakeScript>().enabled = true;
        previewInstance.GetComponent<PhysicsDrag2D>().enabled = true;
        previewInstance.GetComponent<Rigidbody2D>().simulated = true;
        pressSpaceToCancelDrop.SetActive(false);
        previewInstance = null;
        isPlacing = false;

        int index = CombatInventoryManager.Instance.selectedSlotIndex;
        CombatInventoryManager.Instance.ClearSlot(index);
    }

    private void CancelPreview()
    {
        Destroy(previewInstance);
        isPlacing = false;
        pressSpaceToCancelDrop.SetActive(false);
        glepStatsGroup.alpha = 1f;
        glepStatsGroup.interactable = true;
        glepStatsGroup.blocksRaycasts = true;
    }
}
