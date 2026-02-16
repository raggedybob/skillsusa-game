using UnityEngine;
using System.Collections;

public class InsertGlepScript : MonoBehaviour
{
    [SerializeField] private float hoverTimeRequired = 2f;

    private float currentHoverTime = 0f;
    private PhysicsDrag2D currentGlep;
    private bool isActive = false;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite activeSprite;

    private void OnTriggerStay2D(Collider2D other)
    {
        PhysicsDrag2D glep = other.GetComponent<PhysicsDrag2D>();

        if (glep != null && glep.IsBeingDragged)
        {
            if (isActive)
                return;
            currentHoverTime += Time.deltaTime;

            if (currentHoverTime >= hoverTimeRequired)
            {
                ActivateBuilding(glep);
            }
        }
        if (!glep.IsBeingDragged)
        {
            currentHoverTime = 0f;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        PhysicsDrag2D glep = other.GetComponent<PhysicsDrag2D>();

        if (glep != null)
        {
            currentHoverTime = 0f;
        }
    }

    private void ActivateBuilding(PhysicsDrag2D glep)
    {
        currentGlep = glep;

        glep.gameObject.SetActive(false); // hide glep

        // change sprite
        spriteRenderer.sprite = activeSprite;
        isActive = true;
        StartCoroutine(ProductionLoop());
    }
    private IEnumerator ProductionLoop()
    {
        while (isActive)
        {
            yield return new WaitForSeconds(1f);
            MaterialManager.Instance.Add(MaterialType.Gloop, 1);
        }
    }
}
