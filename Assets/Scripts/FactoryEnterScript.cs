using UnityEngine;
using UnityEngine.SceneManagement;
public class FactoryEnterScript : MonoBehaviour, IClickable
{
    public void OnClicked()
    {
        Debug.Log("Factory Entered");
        // Add logic for entering the factory here
        SceneManager.LoadScene("FactoryScene"); 
    }   
}
