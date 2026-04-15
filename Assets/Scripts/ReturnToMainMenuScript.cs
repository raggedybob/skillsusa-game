using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMainMenuScript : MonoBehaviour
{   
    public void OnClick()
    {
        SceneManager.LoadScene("Main Menu");
    }    
}

