using UnityEngine;

[RequireComponent(typeof(Collider))]
public class AISuspicionZone : MonoBehaviour
{
    public static bool IsPlayerInside { get; private set; }

    private Collider suspicionCollider;

    private void Awake()
    {
        suspicionCollider = GetComponent<Collider>();

        // Make sure this collider is a trigger
        suspicionCollider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        Debug.Log("PLAYER ENTERED SUSPICION ZONE");

        IsPlayerInside = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        Debug.Log("PLAYER EXITED SUSPICION ZONE");

        IsPlayerInside = false;
    }
}