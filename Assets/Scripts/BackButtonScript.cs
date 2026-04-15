using Unity.VisualScripting;
using UnityEngine;

public class BackButtonScript : MonoBehaviour
{
    [SerializeField] private GameObject audioUI;
    [SerializeField] private GameObject pauseMenuUI;

    public void OnClick()
    {
        audioUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }
}
