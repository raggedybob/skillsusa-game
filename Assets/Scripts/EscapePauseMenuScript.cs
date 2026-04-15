using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
public class EscapePauseMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject audioPanel;
    [SerializeField] private GameObject areYouSurePanel;
    [SerializeField] private RectTransform pauseMenuTransform;
    public static bool isPaused = false;
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
    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        StartCoroutine(SlideIn());
    }

    public void ResumeGame()
    {
        StartCoroutine(SlideOut());
        pausePanel.SetActive(true);
        audioPanel.SetActive(false);
        areYouSurePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    private IEnumerator SlideIn()
    {
        Vector2 startPos = new Vector2(-Screen.width, 0);
        Vector2 endPos = new Vector2(0, 0);
        float duration = 0.3f;
        float elapsed = 0f;

        pauseMenuTransform.anchoredPosition = startPos;
        pauseMenuUI.SetActive(true);

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = elapsed / duration;
            t = t * t * (3f - 2f * t); 
            pauseMenuTransform.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
            yield return null;
        }

        pauseMenuTransform.anchoredPosition = endPos;
    }

    private IEnumerator SlideOut()
    {
        Vector2 startPos = new Vector2(0, 0);
        Vector2 endPos = new Vector2(-Screen.width, 0);
        float duration = 0.3f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = elapsed / duration;
            t = t * t * (3f - 2f * t);
            pauseMenuTransform.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
            yield return null;
        }

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }
}
