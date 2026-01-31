using UnityEngine;

public class MaterialCircle : MonoBehaviour, IClickable
{
    public void OnClicked()
    {
        MaterialManager.Instance.AddMaterial1(1);
    }
}
