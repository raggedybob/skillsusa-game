using UnityEngine;

public class OpenAudioMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject audioUI;
    [SerializeField] private GameObject pauseMenuUI;

    private void Awake()
    {
        audioUI.SetActive(false);
    }

    public void OnClick()
    {
        Debug.Log("audio menu opened");
        audioUI.SetActive(true);
        pauseMenuUI.SetActive(false);
    }
}
