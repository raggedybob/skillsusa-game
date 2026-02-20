using UnityEngine;
using UnityEngine.SceneManagement;
public class FactoryEnterScript : MonoBehaviour, IClickable
{
    [SerializeField] private NPCDialogueScript NPC;
    public void OnClicked()
    {
        Debug.Log("Factory Entered");
        SceneManager.LoadScene("FactoryScene");
    }
}
