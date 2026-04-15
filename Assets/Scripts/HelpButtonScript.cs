using UnityEngine;

public class HelpButtonScript : MonoBehaviour
{
    [SerializeField] private GameObject helpPanel;

    public void Start()
    {
        helpPanel.SetActive(false);
    }
    public void Onclick()
    {
        helpPanel.SetActive(true);
    }
}
