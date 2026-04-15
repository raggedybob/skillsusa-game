using UnityEngine;

public class GLEPMachineScript : MonoBehaviour
{
    public static GLEPMachineScript Instance;
    [SerializeField] private GameObject basicGLEP;
    [SerializeField] private GameObject gamblingGLEP;

    void Awake()
    {
        Instance = this;
        basicGLEP.SetActive(true);
        gamblingGLEP.SetActive(false);
    }

    public void UnlockGambling()
    {
        basicGLEP.SetActive(false);
        gamblingGLEP.SetActive(true);
    }
}