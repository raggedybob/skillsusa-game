using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButtonScript : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenu;
    public void OnClicked()
    {
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            Debug.Log("Game Started");
            SceneManager.LoadScene("OutsideScene");
        }
        else if (SceneManager.GetActiveScene().name == "EndScene" || SceneManager.GetActiveScene().name == "GameOver")
        {
            SceneManager.LoadScene("Main Menu");
        }
        else
        {
            PauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
