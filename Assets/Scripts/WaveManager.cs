using System.Collections;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public EnemyData enemyType;
    public int count;
    public float spawnInterval;
    public float delayBeforeWave;
}

[System.Serializable]
public class NightWaves
{
    public Wave[] waves;
}

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;
    public NightWaves[] nights = new NightWaves[3]; // 3 nights
    public Transform spawnPoint;
    private int currentNight = 0;

    void Awake() => Instance = this;

    void OnEnable() => DayNightManager.OnNight += StartNight;
    void OnDisable() => DayNightManager.OnNight -= StartNight;

    void StartNight()
    {
        if (currentNight >= nights.Length) return;
        StartCoroutine(RunNight(nights[currentNight]));
        currentNight++;
    }

    IEnumerator RunNight(NightWaves night)
    {
        foreach (Wave wave in night.waves)
        {
            yield return new WaitForSeconds(wave.delayBeforeWave);
            for (int i = 0; i < wave.count; i++)
            {
                Instantiate(wave.enemyType.enemyPrefab, spawnPoint.position, Quaternion.identity);
                yield return new WaitForSeconds(wave.spawnInterval);
            }
        }
    }
}
