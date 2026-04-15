using UnityEngine;
using UnityEngine.UI;

public class BasicGLEPScript : MonoBehaviour
{
    [SerializeField] private CombatData basicGlepData;
    [SerializeField] private float spawnInterval = 10f;
    [SerializeField] private float launchForce = 10f;
    [SerializeField] private Slider glepProgressSlider;
    [SerializeField] private AudioClip spawnSoundClip;

    private float timer = 0f;

    private void Awake()
    {
        ShootGlep(); // Spawn one immediately on start
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (glepProgressSlider != null)
        {
            glepProgressSlider.value = Mathf.Clamp01(timer / spawnInterval);
        }

        if (timer >= spawnInterval)
        {
            timer = 0f;
            ShootGlep();
        }
    }

    private void ShootGlep()
    {
        SoundFXManager.Instance.PlaySFX(spawnSoundClip, transform, .5f);
        Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y + 4f, transform.position.z);
        GameObject newGlep = Instantiate(basicGlepData.glepPrefab);
        newGlep.GetComponent<GlepUnitScript>().EnterFactory(spawnPos);

        Rigidbody2D rb = newGlep.GetComponent<Rigidbody2D>();
        float spread = Random.Range(-2f, 2f);
        rb.AddForce(new Vector2(spread, launchForce), ForceMode2D.Impulse);
    }
}