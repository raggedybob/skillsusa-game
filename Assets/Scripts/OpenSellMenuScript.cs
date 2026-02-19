using UnityEngine;

public class OpenSellMenuScript : MonoBehaviour
{
    public void OnClick()
    {
        if(!BuildManagerScript.Instance.IsSellMode)
        {
            BuildManagerScript.Instance.EnterSellMode();
        }
        else
        {
            BuildManagerScript.Instance.ExitSellMode();
        }
    }
}
