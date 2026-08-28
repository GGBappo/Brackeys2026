using UnityEngine;
using UnityEngine.UI;

public class SusMeter : MonoBehaviour
{
    [SerializeField] private Slider suspicionSlider;
    [SerializeField] private float maxSuspicion = 100f;
    [SerializeField] private float suspicionIncreaseAmount = 10f;

    private float currentSuspicion = 0f;

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

        currentSuspicion = Mathf.Min(
            currentSuspicion + suspicionIncreaseAmount,
            maxSuspicion
        );

        if (suspicionSlider != null)
        {
            suspicionSlider.value = currentSuspicion;
        }

        Debug.Log($"Suspicion increased to {currentSuspicion}");
    }
}