using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButtonScript : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenu;
    public void OnClick()
    {
        Debug.Log("Play Button Clicked");
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            SceneManager.LoadScene("OutsideScene");
        }
        else if (SceneManager.GetActiveScene().name == "EndScene" || SceneManager.GetActiveScene().name == "GameOver")
        {
            SceneManager.LoadScene("Main Menu");
        }
        else
        {
            EscapePauseMenuScript.isPaused = false;
            PauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
