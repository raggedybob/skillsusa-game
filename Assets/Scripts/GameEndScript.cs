using UnityEngine;

public class GameEndScript : MonoBehaviour, IClickable
{   
    public void OnClicked()
    {
        Debug.Log("Game Ended");
        Application.Quit();
    }
}
