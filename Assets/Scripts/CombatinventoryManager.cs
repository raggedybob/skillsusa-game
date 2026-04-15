using UnityEngine;

public class CombatInventoryManager : MonoBehaviour
{
    public static CombatInventoryManager Instance;
    public GameObject[] slots = new GameObject[8];
    public int selectedSlotIndex = -1;
    public HotbarSlot[] hotbarSlots;
    public Vector3[] combatPositions = new Vector3[8];

    void OnEnable() => DayNightManager.OnNight += ActivateCombat;
    void OnDisable() => DayNightManager.OnNight -= ActivateCombat;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void AssignToSlot(GlepUnitScript glep, int index)
    {
        slots[index] = glep.gameObject;
        glep.EnterCombat(combatPositions[index]);
        Debug.Log($"Slot {index} assigned {glep.data.name}");
    }

    public void ClearSlot(int index)
    {
        slots[index] = null;
        hotbarSlots[index].ClearPortrait();
        if (selectedSlotIndex == index)
        {
            selectedSlotIndex = -1;
            DisplayGlepStatsScript.Instance.HideStats();
        }
    }

    public void UnlockSlot(int index)
    {
        if (index < hotbarSlots.Length)
            hotbarSlots[index].Unlock();
    }

    public void SelectSlot(int index) => selectedSlotIndex = index;

    public GlepUnitScript GetSelectedGlep()
    {
        if (selectedSlotIndex == -1) return null;
        if (slots[selectedSlotIndex] == null) return null;
        return slots[selectedSlotIndex].GetComponent<GlepUnitScript>();
    }

    void ActivateCombat()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == null) continue;
            slots[i].GetComponent<GlepUnitScript>().combatScript.enabled = true;
        }
    }
}
