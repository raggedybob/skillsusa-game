using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    void Awake()
    {
        
    }

    void Update()
    {
        transform.position = new Vector3(_mainCamera.transform.position.x, _mainCamera.transform.position.y, -2f);
    }
}
