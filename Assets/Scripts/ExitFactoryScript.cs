using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitFactoryScript : MonoBehaviour
{
    public void OnClick()
    {
        Debug.Log("Exiting Factory");

        SceneManager.LoadScene("OutsideScene");
    }
}
