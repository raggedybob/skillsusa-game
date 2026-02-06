using UnityEngine;

public class backgroundScript : MonoBehaviour
{
    private Camera _mainCamera;
    [SerializeField] private float _parallaxEffectMultiplier = 0.5f;    
    private void Awake()
    {
        _mainCamera = Camera.main;
    }
    void Update()
    {
        transform.position = new Vector3((_mainCamera.transform.position.x * .95f), (_mainCamera.transform.position.y * _parallaxEffectMultiplier), -2f);
    }
}
