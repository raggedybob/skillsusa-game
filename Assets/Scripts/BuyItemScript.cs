using UnityEngine;
using UnityEngine.UI;

public class BuyItemScript : MonoBehaviour
{
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        ItemData item = selectedItemUIScript.Instance.CurrentItem;

        if (item == null)
            return;

        item.OnPurchase();
    }
}

