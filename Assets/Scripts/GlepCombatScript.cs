using System.Collections;
using UnityEngine;
using System;

public class GlepCombatScript : MonoBehaviour
{
    private GlepUnitScript glepUnit;
    private float currentHealth;
    private float maxHealth;
    private float attack;
    private float range;
    private float attackSpeed;
    private float moveSpeed = 2f;
    private bool isAttacking = false;
    private Transform currentTarget;
    public float bonusHealth = 0f;
    public float bonusAttack = 0f;
    public float bonusAttackSpeed = 0f;
    public float bonusRange = 0f;
    public event Action OnAttack;
    [SerializeField] private AudioClip damagedSoundClip;

    // and update OnEnable to use them

    [Header("HP Bar")]
    public GameObject hpBarPrefab;
    private GameObject hpBarInstance;
    private UnityEngine.UI.Slider hpSlider;

    void OnEnable()
    {
        glepUnit = GetComponent<GlepUnitScript>();
        CombatData d = glepUnit.data;
        maxHealth = d.baseHealth + UpgradeManager.Instance.globalHealthBonus + bonusHealth;
        attack = d.baseAttack + UpgradeManager.Instance.globalAttackBonus + bonusAttack;

        // base stats + item bonuses
        maxHealth = d.baseHealth + GetItemBonus(d, "health");
        currentHealth = maxHealth;
        attack = d.baseAttack + GetItemBonus(d, "attack");
        range = d.baseRange + GetItemBonus(d, "range");
        attackSpeed = d.baseAttackSpeed + GetItemBonus(d, "attackSpeed");
        SpawnHPBar();
    }

    float GetItemBonus(CombatData d, string stat)
    {
        float bonus = 0f;
        if (d.item1 != null) bonus += GetStatFromItem(d.item1, stat);
        if (d.item2 != null) bonus += GetStatFromItem(d.item2, stat);
        if (d.item3 != null) bonus += GetStatFromItem(d.item3, stat);
        return bonus;
    }

    float GetStatFromItem(ItemData item, string stat)
    {
        return stat switch
        {
            "health" => item.bonusHealth,
            "attack" => item.bonusAttack,
            "range" => item.bonusRange,
            "attackSpeed" => item.bonusAttackSpeed,
            _ => 0f
        };
    }

    void Update()
    {
        if (isAttacking) return; // dont do anything else while attacking

        currentTarget = FindNearestEnemy();

        if (currentTarget != null)
        {
            float dist = Vector2.Distance(transform.position, currentTarget.position);
            if (dist <= range)
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
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
    }

    Transform FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform nearest = null;
        float minDist = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector2.Distance(transform.position, enemy.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = enemy.transform;
            }
        }
        return nearest;
    }

    IEnumerator BounceAttack()
    {
        isAttacking = true;

        Vector3 startPos = transform.position;
        Vector3 lungePos = currentTarget != null
            ? Vector3.MoveTowards(startPos, currentTarget.position, 0.4f)
            : startPos + Vector3.right * 0.4f;

        // lunge forward
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * 8f;
            transform.position = Vector3.Lerp(startPos, lungePos, t);
            yield return null;
        }

        // deal damage on contact
        if (currentTarget != null)
        {
            EnemyScript enemy = currentTarget.GetComponent<EnemyScript>();
            if (enemy != null)
            {
                enemy.TakeDamage(attack);
                OnAttack?.Invoke(); // fire the event
            }
        }

        // bounce back
        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * 8f;
            transform.position = Vector3.Lerp(lungePos, startPos, t);
            yield return null;
        }

        transform.position = startPos;

        yield return new WaitForSeconds(1f / attackSpeed);
        isAttacking = false;
    }

    public void TakeDamage(float amount)
    {
        SoundFXManager.Instance.PlaySFX(damagedSoundClip, transform, 1f);
        currentHealth -= amount;
        UpdateHPBar();
        if (currentHealth <= 0) Die();
    }

    void Die()
    {
        if (hpBarInstance != null) Destroy(hpBarInstance);
        // notify hotbar slot here later
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
        hpSlider.value = currentHealth / maxHealth;

        // follow the glep in world space
        if (hpBarInstance != null)
        {
            Vector3 above = transform.position + Vector3.up * 0.8f;
            hpBarInstance.transform.position = above;
        }
    }
}
