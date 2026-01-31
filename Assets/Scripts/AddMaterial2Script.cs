using UnityEngine;
using System.Collections;
public class AddMaterial2Script : MonoBehaviour, IClickable
{
    public bool IsProducing { get; private set; }

    private Coroutine productionRoutine;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public void OnClicked()
    {
        if (!IsProducing)
        {
            IsProducing = true;
            spriteRenderer.color = Color.blue;
            productionRoutine = StartCoroutine(Material2Production());
        }
        else
        {
            IsProducing = false;
            spriteRenderer.color = Color.white;
            if (productionRoutine != null)
            {
                StopCoroutine(productionRoutine);
                productionRoutine = null;
            }
        }
    }

    IEnumerator Material2Production()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            MaterialManager.Instance.AddMaterial2(1);
        }
    }
}

