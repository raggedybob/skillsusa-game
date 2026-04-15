using UnityEngine;
using System;
using UnityEngine.Rendering.Universal;
using TMPro;

public class DayNightManager : MonoBehaviour
{
    public float currentTime = 0f;
    public int currentDay = 1;
    public static event Action OnDawn;
    public static event Action OnNight;
    [SerializeField] private GameObject outsideBackground;
    [SerializeField] private Sprite daySprite;
    [SerializeField] private Sprite nightSprite;
    public static DayNightManager Instance;
    public bool isNight = false;
    [SerializeField] private Transform clockHand;
    [SerializeField] private TMP_Text dayText;

    private void Awake()
    {
        Instance = this;
        outsideBackground.GetComponent<SpriteRenderer>().sprite = daySprite;
        currentDay = 1;
        dayText = dayText.GetComponent<TMP_Text>();
        dayText.text = $"Day {currentDay}";
    }
    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= 180f) 
        {
            currentTime = 0f; // reset every 3 minutes
            currentDay++; // increment day count
        }
        clockHand.localRotation = Quaternion.Euler(0, 0, -currentTime * 2f); // Rotate clock hand
        if (currentTime >= 120f && !isNight) 
        {
            isNight = true;
            OnNight?.Invoke();
            outsideBackground.GetComponent<SpriteRenderer>().sprite = nightSprite;
            dayText.text = $"Night {currentDay}";
        }
        if (currentTime < 120f && isNight)
        {
            isNight = false;
            OnDawn?.Invoke();
            outsideBackground.GetComponent<SpriteRenderer>().sprite = daySprite;
            dayText.text = $"Day {currentDay}";
        }
    }
    
}