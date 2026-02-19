using UnityEngine;

public class ShopScript : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void OnClick()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}