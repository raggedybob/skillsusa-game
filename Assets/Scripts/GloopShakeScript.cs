using Cysharp.Threading.Tasks;
using UnityEngine;

public class GloopShakeScript : MonoBehaviour
{
    private Vector2 lastPosition;
    [SerializeField] private float distanceMovedLastFrame;
    [SerializeField] private float MovementPerGloop = 10f;
    [SerializeField] private float MovementTillNextGloop = 10f;
    [SerializeField] private ParticleSystem GloopingParticles;
    [SerializeField] private AudioClip gloopSoundClip;
    void Awake()
    {
        lastPosition = transform.position;
    }

    void FixedUpdate()
    {
        distanceMovedLastFrame = Vector2.Distance(transform.position, lastPosition);
        lastPosition = transform.position;
        MovementTillNextGloop -= distanceMovedLastFrame;

        if (MovementTillNextGloop <= 0)
        {
            MaterialManager.Instance.Add(MaterialType.Gloop, 1);
            MovementTillNextGloop = MovementPerGloop;
            GloopingParticles.Play();
        }
    }
}
