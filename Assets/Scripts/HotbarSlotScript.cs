using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HotbarSlot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public int slotIndex;
    private bool isDragging;
    private bool isLocked = true;
    [SerializeField] private RectTransform lineImage;
    [SerializeField] private RectTransform outlineImage;
    [SerializeField] private GameObject SelectHand;
    [SerializeField] private Image slotPortrait;
    [SerializeField] private GameObject lockImage;
    [SerializeField] private CanvasGroup glepStatsGroup;
    [SerializeField] private GameObject hotbarHighlight;
    [SerializeField] private GameObject ClickOnGlepText;
    private GlepUnitScript assignedGlep;
    private bool hasGlep = false;

    void Start() 
    {
        lineImage.gameObject.SetActive(false);
        outlineImage.gameObject.SetActive(false);
        SelectHand.SetActive(false);

        if (slotIndex >= UpgradeManager.Instance.unlockedHotbarSlots)
        {
            isLocked = true;
            lockImage.SetActive(true);
        }
        else
        {
            isLocked = false;
            lockImage.SetActive(false);
        }
    }

    public void Unlock()
    {
        isLocked = false;
        lockImage.SetActive(false);
    }

    public void OnPointerDown(PointerEventData e)
    {
        if (isLocked) return;

        if (InventoryUIScript.Instance.isEquipping)
        {
            GlepUnitScript glep = assignedGlep;
            if (!hasGlep || assignedGlep == null) return;

            ItemData item = InventoryUIScript.Instance.selectedItem;
            glep.equippedItems.Add(item);
            item.OnEquip(glep.combatScript);

            InventoryUIScript.Instance.isEquipping = false;
            InventoryManager.Instance.RemoveItem(item);

            Debug.Log($"Equipped {item.itemName} to glep!");

            hotbarHighlight.SetActive(false);
            ClickOnGlepText.SetActive(false);
            return;
        }

        if (hasGlep)
        {
            UpdateGlepStats(CombatInventoryManager.Instance.slots[slotIndex].GetComponent<GlepUnitScript>());
            CombatInventoryManager.Instance.SelectSlot(slotIndex);
            glepStatsGroup.alpha = 1f;
            glepStatsGroup.interactable = true;
            glepStatsGroup.blocksRaycasts = true;
            return; // stop here, dont start dragging
        }

        isDragging = true;
        lineImage.gameObject.SetActive(true);
        outlineImage.gameObject.SetActive(true);
        SelectHand.SetActive(true);
    }

    public void OnPointerUp(PointerEventData e)
    {
        if (isLocked || hasGlep) return;
        isDragging = false;
        lineImage.gameObject.SetActive(false);
        outlineImage.gameObject.SetActive(false);
        SelectHand.SetActive(false);

        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = -1;
        RaycastHit2D hit = Physics2D.Raycast(mouseWorld, Vector2.zero);
        if (hit.collider != null)
        {
            GlepUnitScript glep = hit.collider.GetComponent<GlepUnitScript>();
            assignedGlep = glep;

            if (glep != null && glep.combatScript != null)
            {
                assignedGlep = glep;
                glep.combatScript.OnDeath += OnGlepDied;

                CombatInventoryManager.Instance.AssignToSlot(glep, slotIndex);
                slotPortrait.sprite = glep.data.sprite;
                slotPortrait.color = Color.white;
                hasGlep = true;
            }
        }
    }

    void Update()
    {
        if (!isDragging) return;
        Vector2 mousePos = Input.mousePosition;
        Vector2 slotPos = transform.position;
        lineImage.position = (slotPos + mousePos) / 2f;
        outlineImage.position = (slotPos + mousePos) / 2f;
        SelectHand.transform.position = mousePos;
        float distance = Vector2.Distance(slotPos, mousePos);
        lineImage.sizeDelta = new Vector2(distance, 4f);
        outlineImage.sizeDelta = new Vector2(distance + 4f, 8f);
        Vector2 direction = mousePos - slotPos;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        lineImage.rotation = Quaternion.Euler(0, 0, angle);
        outlineImage.rotation = Quaternion.Euler(0, 0, angle);
        SelectHand.transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }

    private void UpdateGlepStats(GlepUnitScript glep)
    {
        DisplayGlepStatsScript.Instance.UpdateSelectedDisplay(glep.data);
    }
    private void OnGlepDied()
    {
        ClearPortrait();
    }

    public void ClearPortrait()
    {
        if (hasGlep)
        {
            var glep = CombatInventoryManager.Instance.slots[slotIndex]
                ?.GetComponent<GlepUnitScript>();

            if (assignedGlep != null)
            {
                assignedGlep.combatScript.OnDeath -= OnGlepDied;
                assignedGlep = null;
            }
        }

        slotPortrait.sprite = null;
        slotPortrait.color = new Color(1, 1, 1, 0);
        hasGlep = false;
    }
}