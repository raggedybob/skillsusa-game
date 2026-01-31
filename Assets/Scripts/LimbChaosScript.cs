using UnityEngine;

public class LimbChaosScript : MonoBehaviour
{
    public Rigidbody2D rb;

    void FixedUpdate()
    {
        rb.AddTorque(Random.Range(-2f, 2f));
    }
}
