using UnityEngine;
using System.Collections;
public class StabilityManager : MonoBehaviour
{
    public static StabilityManager Instance;
    [SerializeField] private RectTransform stabilityBar;
    public event System.Action OnGameOver;
    public int Stability { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Stability = 700;
        StartCoroutine(UpdateStability());
    }

    private IEnumerator UpdateStability()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            int activeGleps = GameObject.FindGameObjectsWithTag("Glep").Length;
            Stability -= activeGleps;
            if (Stability <= 0)
            {
                stabilityBar.sizeDelta = new Vector2(0, stabilityBar.GetComponent<RectTransform>().sizeDelta.y);
                Debug.Log("Game Over: Stability reached zero!");
                OnGameOver?.Invoke();
            }
            else
            {
                stabilityBar.sizeDelta = new Vector2(Stability, stabilityBar.GetComponent<RectTransform>().sizeDelta.y);
            }

        }
    }
    public void IncreaseStability(int amount)
    {
        Stability = Mathf.Min(Stability + amount, 700);
        stabilityBar.sizeDelta = new Vector2(Stability, stabilityBar.GetComponent<RectTransform>().sizeDelta.y);
    }
}
