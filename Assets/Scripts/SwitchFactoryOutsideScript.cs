using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SwitchFactoryOutsideScript : MonoBehaviour
{
    [SerializeField] private GameObject CreateMenu;
    [SerializeField] private Vector3 outsidePosition;
    [SerializeField] private Vector3 factoryPosition;
    [SerializeField] private Transform cameraPos;
    [SerializeField] private Sprite buttonOutsideSprite;
    [SerializeField] private Sprite buttonFactorySprite;
    [SerializeField] private AudioClip switchSoundClip;
    public static bool isOutside;

    private void Awake()
    {
        isOutside = false;
        gameObject.GetComponent<Image>().sprite = buttonFactorySprite;
    }
    public void OnClick()
    {
        SoundFXManager.Instance.PlaySFX(switchSoundClip, transform, 1f);
        isOutside = !isOutside;
        if (isOutside)
        {
            cameraPos.position = outsidePosition;
            CreateMenu.SetActive(false);
            gameObject.GetComponent<Image>().sprite = buttonOutsideSprite;
        }
        else
        {
            cameraPos.position = factoryPosition;
            CreateMenu.SetActive(true);
            gameObject.GetComponent<Image>().sprite = buttonFactorySprite;
        }
    }
}
