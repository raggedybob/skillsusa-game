using UnityEngine;

public class ExitGlepStatsScript : MonoBehaviour
{
    [SerializeField] private GameObject statsPanel;

    public void OnClick()
    {
        if (statsPanel != null)
        {
            statsPanel.SetActive(false);
        }
    }
}
