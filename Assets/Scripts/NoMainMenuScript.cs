using UnityEngine;

public class NoMainMenuScript : MonoBehaviour
{
    [SerializeField] GameObject Outline;
    [SerializeField] GameObject PausePanel;
    [SerializeField] GameObject GlepianAutonomy;
    [SerializeField] GameObject AreYouSureYouWantToGoBackToTheMainMenu;
    public void OnClick()
    {
        Outline.SetActive(true);
        PausePanel.SetActive(true);
        GlepianAutonomy.SetActive(true);
        AreYouSureYouWantToGoBackToTheMainMenu.SetActive(false);
    }
}
