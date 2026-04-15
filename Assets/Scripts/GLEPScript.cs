using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GLEPScript : MonoBehaviour, IClickable
{
    [SerializeField] private GameObject glepGamba;
    [SerializeField] private RectTransform rollContent;
    private int cost = 0;
    public List<CombatData> GambaList;
    [SerializeField] private GameObject iconPrefab;
    [SerializeField] private float iconWidth = 100f;
    [SerializeField] private float launchForce = 10f;
    [SerializeField] private float baseCost = 10f;
    [SerializeField] private float costMultiplier = 1.5f;
    [SerializeField] private TMP_Text costText;
    private int totalRolls = 0;
    [SerializeField] private GameObject costDisplay; // a UI panel showing the cost text
    [SerializeField] private AudioClip rollSoundClip;
    [SerializeField] private AudioClip spawnSoundClip;

    public int stripLength = 20;
    public int landingIndex = 15;   // winner always lands here
    private CombatData winner;
    private bool isRolling = false;

    private void Awake()
    {
        glepGamba.SetActive(false);
        costDisplay.SetActive(false);
        UpdateCost();
    }

    void OnMouseEnter()
    {
        costDisplay.SetActive(true);
    }

    void OnMouseExit()
    {
        costDisplay.SetActive(false);
    }

    public void OnClicked()
    {
        if (!isRolling)
        {
            Debug.Log("G.L.E.P. Clicked!");
            if (MaterialManager.Instance.GetAmount(MaterialType.Money) >= cost)
            {
                MaterialManager.Instance.Spend(new List<MaterialCost> { new MaterialCost { type = MaterialType.Money, amount = cost } });
                Debug.Log("GLEP Cost Paid!");
                glepGamba.SetActive(true);
                StartRoll();
            }
            else
            {
                costText.text = "Not enough Money!";
                Invoke("UpdateCost", 1f); // reset cost text after 1 second
                Debug.Log("Not enough Money to pay the cost!");
            }
        }
    }

    public CombatData RollGlep()
    {
        isRolling = true;
        float total = 0f;
        foreach (CombatData glep in GambaList)
            total += glep.dropRate;

        float roll = Random.Range(0f, total);
        float cumulative = 0f;

        foreach (CombatData glep in GambaList)
        {
            cumulative += glep.dropRate;
            if (roll <= cumulative)
                return glep;
        }
        
        return GambaList[0]; 
    }

    public void StartRoll()
    {
        SoundFXManager.Instance.PlaySFX(rollSoundClip, transform, .8f);
        rollContent.anchoredPosition = Vector2.zero; 

        foreach (Transform child in rollContent)
            Destroy(child.gameObject);

        winner = RollGlep();
        for (int i = 0; i < stripLength; i++)
        {
            CombatData glep = i == landingIndex
                ? winner
                : RollGlep(); // use weighted roll
            GameObject icon = Instantiate(iconPrefab, rollContent);
            icon.GetComponent<Image>().sprite = glep.sprite;
        }
        StartCoroutine(ScrollToWinner());
    }

    IEnumerator ScrollToWinner()
    {
        float targetX = -(landingIndex * iconWidth);
        float elapsed = 0f;
        float duration = 1f;
        Vector2 startPos = rollContent.anchoredPosition;
        Vector2 endPos = new Vector2(targetX, 0);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            t = 1f - Mathf.Pow(1f - t, 3f);
            rollContent.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
            yield return null;
        }

        rollContent.anchoredPosition = endPos;
        Debug.Log("Winner: " + winner.name);

        yield return new WaitForSeconds(3f); 
        glepGamba.SetActive(false);
        isRolling = false;
        totalRolls++;
        UpdateCost();
        ShootGlep();
    }

    private void ShootGlep()
    {
        SoundFXManager.Instance.PlaySFX(spawnSoundClip, transform, .5f);
        Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y + 4f, transform.position.z);
        GameObject newGlep = Instantiate(winner.glepPrefab);
        newGlep.GetComponent<GlepUnitScript>().EnterFactory(spawnPos);

        Rigidbody2D rb = newGlep.GetComponent<Rigidbody2D>();
        float spread = Random.Range(-2f, 2f); 
        rb.AddForce(new Vector2(spread, launchForce), ForceMode2D.Impulse);
    }

    private void UpdateCost()
    {
        cost = Mathf.RoundToInt(baseCost * Mathf.Pow(costMultiplier, totalRolls));
        costText.text = $"Cost: {cost} Money";    
    }
}
