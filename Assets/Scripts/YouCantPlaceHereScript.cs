using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class YouCantPlaceHereScript : MonoBehaviour
{
    //get set isWarning bool

    public bool isWarning { get; set; }
    public static YouCantPlaceHereScript Instance;

    private void Start()
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 0f;
        Instance = this;
    }

    public IEnumerator HideYouCantPlaceThere()
    {
        isWarning = true;
        for (float i = 1f; i >= 0f; i -= .05f)
        {
            gameObject.GetComponent<CanvasGroup>().alpha = i;
            yield return new WaitForSeconds(0.05f);
        }
        gameObject.GetComponent<CanvasGroup>().alpha = 1f;
        gameObject.SetActive(false);
        isWarning = false;
        yield return null;
    }
}
