using System.Collections;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public EnemyData data;
    private float currentHealth;
    private bool isAttacking = false;
    private Transform currentTarget;
    [SerializeField] private AudioClip damagedSoundClip;

    [Header("HP Bar")]
    public GameObject hpBarPrefab;
    private GameObject hpBarInstance;
    private UnityEngine.UI.Slider hpSlider;

    void Start()
    {
        currentHealth = data.baseHealth;
        GetComponent<SpriteRenderer>().sprite = data.sprite;
        SpawnHPBar();
    }

    void Update()
    {
        if (isAttacking) return; // add this

        currentTarget = FindNearestGlep();
        if (currentTarget != null)
        {
            float dist = Vector2.Distance(transform.position, currentTarget.position);
            if (dist <= data.baseRange)
                StartCoroutine(BounceAttack());
            else
                MoveForward();
        }
        else
        {
            MoveForward();
        }
        UpdateHPBar();
    }

    void MoveForward()
    {
        transform.position -= Vector3.right * data.moveSpeed * Time.deltaTime;
    }

    Transform FindNearestGlep()
    {
        GameObject[] gleps = GameObject.FindGameObjectsWithTag("Glep");
        Transform nearest = null;
        float minDist = Mathf.Infinity;

        foreach (GameObject glep in gleps)
        {
            float dist = Vector2.Distance(transform.position, glep.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = glep.transform;
            }
        }

        // fall back to factory if no gleps
        if (nearest == null)
        {
            GameObject factory = GameObject.FindGameObjectWithTag("Factory");
            if (factory != null) return factory.transform;
        }

        return nearest;
    }

    IEnumerator BounceAttack()
    {
        isAttacking = true;

        Vector3 startPos = transform.position;
        Vector3 lungePos = currentTarget != null
            ? Vector3.MoveTowards(startPos, currentTarget.position, 0.4f)
            : startPos - Vector3.right * 0.4f;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * 8f;
            transform.position = Vector3.Lerp(startPos, lungePos, t);
            yield return null;
        }

        if (currentTarget != null)
        {
            EnemyScript enemy = currentTarget.GetComponent<EnemyScript>();
            GlepCombatScript glep = currentTarget.GetComponent<GlepCombatScript>();
            FactoryHPManager factory = currentTarget.GetComponent<FactoryHPManager>();

            if (glep != null) glep.TakeDamage(data.baseAttack);
            if (factory != null) factory.TakeDamage(data.baseAttack, this);
        }

        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * 8f;
            transform.position = Vector3.Lerp(lungePos, startPos, t);
            yield return null;
        }

        transform.position = startPos;
        yield return new WaitForSeconds(1f / data.baseAttackSpeed);
        isAttacking = false;
    }

    public void TakeDamage(float amount)
    {
        SoundFXManager.Instance.PlaySFX(damagedSoundClip, transform, 1f);
        currentHealth -= amount;
        UpdateHPBar();
        if (currentHealth <= 0) Die();
    }

    [SerializeField] private bool isFinalBoss = false;

    void Die()
    {
        if (hpBarInstance != null) Destroy(hpBarInstance);

        if (isFinalBoss)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("EndScene");
            return;
        }

        Destroy(gameObject);
    }

    void SpawnHPBar()
    {
        if (hpBarPrefab == null) return;
        hpBarInstance = Instantiate(hpBarPrefab);
        hpSlider = hpBarInstance.GetComponentInChildren<UnityEngine.UI.Slider>();
    }

    void UpdateHPBar()
    {
        if (hpSlider == null) return;
        hpSlider.value = currentHealth / data.baseHealth;
        if (hpBarInstance != null)
            hpBarInstance.transform.position = transform.position + Vector3.up * 2f;
    }
}

