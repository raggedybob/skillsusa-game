using UnityEngine;

public class ExitHelpButtonScript : MonoBehaviour
{
    [SerializeField] private GameObject helpPanel;

    public void Onclick()
    {
        helpPanel.SetActive(false);
    }
}
