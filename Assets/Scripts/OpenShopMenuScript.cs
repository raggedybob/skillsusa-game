using UnityEngine;
using UnityEngine.UI;
public class OpenShopMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject createMenu;
    [SerializeField] private Vector3 openPosition;
    [SerializeField] private Vector3 closedPosition;
    [SerializeField] private GameObject shopMenu;

    private void Awake()
    {
        shopMenu.SetActive(false);
    }
    public void OnClick()
    {
        BuildManagerScript.Instance.ExitBuildMode();
        //BuildManagerScript.Instance.ExitSellMode();
        if (!shopMenu.activeSelf || createMenu.transform.localPosition == closedPosition)
        {
            OpenShopMenu();
        }
        else
        {
            CloseShopMenu();
        }
    }
    public void OpenShopMenu()
    {
        shopMenu.SetActive(true);
        if(createMenu.transform.localPosition == closedPosition)
        {
            createMenu.transform.localPosition = openPosition;
        }    
    }

    public void CloseShopMenu()
    {
        shopMenu.SetActive(false);
        if(createMenu.transform.localPosition == openPosition)
        {
            createMenu.transform.localPosition = closedPosition;
        }
    }
}
