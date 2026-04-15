using UnityEngine;
using UnityEngine.SceneManagement;
public class FactoryHPManager : MonoBehaviour
{
    public static FactoryHPManager Instance;
    public float maxHealth = 500f;
    private float currentHealth;
    [SerializeField] private float damageReduction = 0.5f;
    [SerializeField] private float retaliationDamage = 10f;
    [SerializeField] private UnityEngine.UI.Slider hpSlider;
    [SerializeField] private TMPro.TMP_Text hpText;

    void Awake()
    {
        Instance = this;
        currentHealth = maxHealth;
        UpdateHPBar();
    }

    public void TakeDamage(float amount, EnemyScript attacker)
    {
        float reduced = amount * damageReduction;
        currentHealth -= reduced;
        UpdateHPBar();

        if (attacker != null)
            attacker.TakeDamage(retaliationDamage);

        if (currentHealth <= 0) FactoryDestroyed();
    }

    void FactoryDestroyed()
    {
        Debug.Log("Factory destroyed!");
        SceneManager.LoadScene("GameOver");
    }

    void UpdateHPBar()
    {
        if (hpSlider != null)
            hpSlider.value = currentHealth / maxHealth;
        if (hpText != null)
            hpText.text = $"{Mathf.CeilToInt(currentHealth)}/{Mathf.CeilToInt(maxHealth)}";
    }
}
