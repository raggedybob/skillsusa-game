using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class EscapePauseMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    private void Awake()
    {
        pauseMenuUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && (SceneManager.GetActiveScene().name == "FactoryScene" || SceneManager.GetActiveScene().name == "OutsideScene")) 
        {
            if (!pauseMenuUI.activeSelf)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false); 
        Time.timeScale = 1f; 
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true); 
        Time.timeScale = 0f; 
    }
}
