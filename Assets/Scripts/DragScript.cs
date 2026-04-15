using UnityEngine;
using UnityEngine.InputSystem;

public class PhysicsDrag2D : MonoBehaviour
{
    TargetJoint2D joint;
    public static PhysicsDrag2D currentlyDragged;
    private float timeRemaining;
    [SerializeField] private float jumpInterval = 10f;
    [SerializeField] private float minForce = 5f;
    [SerializeField] private float maxForce = 5f;
    [SerializeField] private AudioClip jumpSoundClip;

    public bool IsBeingDragged { get; private set; }

    private void Awake()
    {
        timeRemaining = jumpInterval;
        IsBeingDragged = false;
    }
    void Update()
    {
        timeRemaining -= Time.deltaTime;
        if(timeRemaining <= 0f)
        {
            Jump();
        }
        if (BuildManagerScript.Instance.selectedBuilding == null)
        {
            Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(
            Mouse.current.position.ReadValue());

            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                RaycastHit2D hit = Physics2D.Raycast(mouseWorld, Vector2.zero);

                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    if (currentlyDragged != null)
                        return;

                    currentlyDragged = this;
                    IsBeingDragged = true;

                    joint = gameObject.AddComponent<TargetJoint2D>();
                    joint.autoConfigureTarget = false;
                    joint.frequency = 2.5f;
                    joint.dampingRatio = 0.6f;
                    joint.maxForce = 1000f;
                    joint.target = mouseWorld;
                }
            }

            if (joint != null)
            {
                joint.target = mouseWorld;
                timeRemaining = jumpInterval;
            }
            if (Mouse.current.leftButton.wasReleasedThisFrame && joint != null)
            {
                Destroy(joint);
                IsBeingDragged = false;
                currentlyDragged = null;
            }

        }
    }
    public void ForceRelease()
    {
        if (joint != null)
        {
            Destroy(joint);
        }

        IsBeingDragged = false;

        if (currentlyDragged == this)
            currentlyDragged = null;
    }
    public void Consume()
    {
        ForceRelease();
        gameObject.SetActive(false);
    }
    private void Jump()
    {
        SoundFXManager.Instance.PlaySFX(jumpSoundClip, transform, .5f);
        Vector2 randomDirection = Random.insideUnitCircle;
        Vector2 forceVector = randomDirection.normalized * Random.Range(minForce, maxForce);
        gameObject.GetComponent<Rigidbody2D>().AddForce(forceVector, ForceMode2D.Impulse);
        timeRemaining = jumpInterval;
    }
}
