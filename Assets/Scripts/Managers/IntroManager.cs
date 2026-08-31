using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class IntroManager : MonoBehaviour
{
    [Header("Graphs")]
    public RuntimeDialogueGraph introGraph;
    public RuntimeDialogueGraph phoneTextGraph;
    
    [Header("AI Routing")]
    public AIBehavior partnerAI;
    public Transform livingRoomWaypoint;
    public AISuspicionZone suspicionZone; // NEW: Drag your Suspicion Zone object here
    
    [Header("Timing")]
    public float delayBeforePhoneText = 4f;

    private bool _introFinished = false;
    private NavMeshAgent _aiAgent;

    private void Awake()
    {
        if (partnerAI != null)
        {
            _aiAgent = partnerAI.GetComponent<NavMeshAgent>();
        }
    }

    private void OnEnable()
    {
        GameEvents.OnDialogueSequenceCompleted += OnDialogueEnded;
    }

    private void OnDisable()
    {
        GameEvents.OnDialogueSequenceCompleted -= OnDialogueEnded;
    }

    private void Start()
    {
        if (partnerAI != null && _aiAgent != null)
        {
            partnerAI.enabled = false;
            _aiAgent.isStopped = true;
        }

        if (suspicionZone != null)
        {
            suspicionZone.enabled = false;
        }

        if (introGraph != null)
        {
            GameEvents.RequestDialogueStart(introGraph, null);
        }
    }

    private void OnDialogueEnded()
    {
        if (!_introFinished)
        {
            _introFinished = true;
            StartCoroutine(SequenceAIExitAndPhone());
        }
    }

    private IEnumerator SequenceAIExitAndPhone()
    {
        if (partnerAI != null && _aiAgent != null)
        {
            _aiAgent.isStopped = false;
            _aiAgent.SetDestination(livingRoomWaypoint.position);
        }

        yield return new WaitForSeconds(delayBeforePhoneText);

        if (phoneTextGraph != null)
        {
            GameEvents.RequestDialogueStart(phoneTextGraph, null);
        }

        if (partnerAI != null) partnerAI.enabled = true;
        if (suspicionZone != null) suspicionZone.enabled = true;
    }
}