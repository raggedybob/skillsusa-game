using UnityEngine;
using UnityEngine.SceneManagement;
public class FactoryEnterScript : MonoBehaviour, IClickable
{
    public void OnClicked()
    {
        Debug.Log("Factory Entered");

        SceneManager.LoadScene("FactoryScene"); 
    }
}
