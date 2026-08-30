using UnityEngine;
using UnityEngine.UI;

public class SusMeter : MonoBehaviour
{
    // The static instance makes it globally accessible
    public static SusMeter Instance { get; private set; }

    [SerializeField] private Slider suspicionSlider;
    [SerializeField] private float maxSuspicion = 100f;
    [SerializeField] private float suspicionIncreaseAmount = 10f;

    private float currentSuspicion = 0f;

    private void Awake()
    {
        if (Instance != null && Instance != this) 
        {
            Destroy(gameObject);
        }
        else 
        {
            Instance = this;
        }
    }

    private void Start()
    {
        if (suspicionSlider != null)
        {
            suspicionSlider.minValue = 0f;
            suspicionSlider.maxValue = maxSuspicion;
            suspicionSlider.value = currentSuspicion;
        }
        else
        {
            Debug.LogWarning("SusMeter: Suspicion Slider is not assigned!");
        }
    }

    public void RegisterItemPickup()
    {
        Debug.Log("SusMeter received an item pickup!");

        if (!AISuspicionZone.IsPlayerInside)
        {
            Debug.Log("Pickup happened OUTSIDE the suspicion zone.");
            return;
        }

        Debug.Log("Pickup happened INSIDE the suspicion zone!");
        AddSuspicion(suspicionIncreaseAmount);
    }

    public void AddSuspicion(float amount)
    {
        currentSuspicion = Mathf.Min(currentSuspicion + amount, maxSuspicion);

        if (suspicionSlider != null)
        {
            suspicionSlider.value = currentSuspicion; 
        }

        MemoryBoard.SetVariable("SuspicionLevel", currentSuspicion.ToString());
        Debug.Log($"Suspicion increased to {currentSuspicion}");
    }
}