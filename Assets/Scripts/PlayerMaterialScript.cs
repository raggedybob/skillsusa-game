using TMPro;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    public static MaterialManager Instance;
    public int Material1;
    public int Material2;
    public int Material3; 
    private void Awake()
    {
        Instance = this;
    }

    public void AddMaterial1(int amount)
    {
        Material1 += amount;
        Debug.Log("Material1: " + Material1);
    }
    public void AddMaterial2(int amount)
    {
        Material2 += amount;
        Debug.Log("Material2: " + Material2);
    }
    public void AddMaterial3(int amount)
    {
        Material3 += amount;
        Debug.Log("Material3: " + Material3);
    }
}
