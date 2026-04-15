using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] GameObject Outline;
    [SerializeField] GameObject PausePanel;
    [SerializeField] GameObject GlepianAutonomy;
    [SerializeField] GameObject AreYouSureYouWantToGoBackToTheMainMenu;

    private void Awake()
    {
        AreYouSureYouWantToGoBackToTheMainMenu.SetActive(false);
    }
    public void OnClick()
    {
        Outline.SetActive(false);
        PausePanel.SetActive(false);
        GlepianAutonomy.SetActive(false);
        AreYouSureYouWantToGoBackToTheMainMenu.SetActive(true);
    }
}
