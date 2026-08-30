using UnityEngine;

[RequireComponent(typeof(Collider))]
public class AISuspicionZone : MonoBehaviour
{
    [Header("Dialogue Graphs")]
    public RuntimeDialogueGraph hostileGraph;
    public RuntimeDialogueGraph friendlyGraph;
    public RuntimeDialogueGraph gameOverGraph;
    
    [Header("Settings")]
    public float detectionCooldown = 5f; 
    private float _lastDetectionTime = -999f;

    public static bool IsPlayerInside { get; private set; }
    private Collider suspicionCollider;

    private void Awake()
    {
        suspicionCollider = GetComponent<Collider>();
        suspicionCollider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        IsPlayerInside = true;

        if (Time.time < _lastDetectionTime + detectionCooldown) return;
        _lastDetectionTime = Time.time;

        string playerLocation = MemoryBoard.GetVariable("PlayerLocation");
        string hidInCloset = MemoryBoard.GetVariable("HidInCloset");

        if (playerLocation != "Bedroom" && hidInCloset != "true")
        {
            string currentSusStr = MemoryBoard.GetVariable("SuspicionLevel");
            if (int.TryParse(currentSusStr, out int susLevel))
            {
                susLevel++;
                MemoryBoard.SetVariable("SuspicionLevel", susLevel.ToString());
                
                if (susLevel >= 3) 
                {
                    // Passes the Game Over graph directly, using the default entry node[cite: 17, 18]
                    GameEvents.RequestDialogueStart(gameOverGraph, null);
                    return;
                }
            }
            
            GameEvents.RequestDialogueStart(hostileGraph, null);
        }
        else
        {
            GameEvents.RequestDialogueStart(friendlyGraph, null);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        IsPlayerInside = false;
    }
}