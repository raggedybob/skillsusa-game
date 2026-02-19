using UnityEngine;
using System.Collections;
public class GlepProductionScript : MonoBehaviour
{
    public float productionInterval = 30f; // time between each glep
    [SerializeField] private GameObject fountainParticles;
    [SerializeField] private GameObject glepPrefab;
    [SerializeField] private ParticleSystem fountainParticleSystem; 
    private void Awake()
    {
        fountainParticles.SetActive(false);
        StartCoroutine(StartGlep()); 
        StartCoroutine(GlepProduction());
    }

    private IEnumerator GlepProduction()
    {
        while (true)
        {
            yield return new WaitForSeconds(productionInterval);
            yield return StartCoroutine(StartGlep());
        }
    }   

    private IEnumerator StartGlep()
    {
        if (fountainParticles != null)
        {
            fountainParticles.SetActive(true);
            fountainParticleSystem.Play();
            for (float y = -4f; y < 10f; y += .25f)
            {
                fountainParticles.transform.position = transform.position + new Vector3(0, y, 0);
                yield return new WaitForSeconds(0.1f);
            }
        }
        fountainParticles.SetActive(false);
        ProduceGlep();
    }
    private void ProduceGlep()
    {
        Instantiate(glepPrefab, transform.position + new Vector3(0, 10, 0), Quaternion.identity);
    }
}
