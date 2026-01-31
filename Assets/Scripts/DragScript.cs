using UnityEngine;
using UnityEngine.InputSystem;

public class PhysicsDrag2D : MonoBehaviour
{
    TargetJoint2D joint;

    void Update()
    {
        Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(
            Mouse.current.position.ReadValue()
        );

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            RaycastHit2D hit = Physics2D.Raycast(mouseWorld, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                joint = gameObject.AddComponent<TargetJoint2D>();
                joint.autoConfigureTarget = false;
                joint.frequency = 2.5f;
                joint.dampingRatio = 0.6f;
                joint.maxForce = 1000f;
                joint.target = mouseWorld;
            }
        }

        if (joint != null)
            joint.target = mouseWorld;

        if (Mouse.current.leftButton.wasReleasedThisFrame && joint != null)
            Destroy(joint);
    }
}
