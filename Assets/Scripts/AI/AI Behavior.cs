using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIBehavior : MonoBehaviour
{
    [SerializeField] private List<Transform> waypoints = new List<Transform>();
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animator;
    [SerializeField] private float waitTime = 2f;
    [SerializeField] private bool startPatrolOnStart = true;

    private int randomIndex;
    private int lastIndex;
    private float agentStopDistance = 1f;

    private bool isPatrolling = false;
    private bool isWaiting = false;
    private bool suppressAutoStart = false;

    private void Awake()
    {
        CacheComponents();
    }

    private void OnEnable()
    {
        CacheComponents();

        GameEvents.OnRequestDialogueStart += PausePatrol;
        GameEvents.OnDialogueSequenceCompleted += ResumePatrol;
    }

    private void OnDisable()
    {
        GameEvents.OnRequestDialogueStart -= PausePatrol;
        GameEvents.OnDialogueSequenceCompleted -= ResumePatrol;
    }

    private void OnValidate()
    {
        CacheComponents();
    }

    private void Start()
    {
        if (startPatrolOnStart && !suppressAutoStart)
        {
            BeginPatrol();
        }
    }

    private void Update()
    {
        if (agent == null || waypoints == null || waypoints.Count == 0 || isWaiting || !isPatrolling)
        {
            return;
        }

        UpdateAnimation();

        if (!agent.pathPending &&
            agent.hasPath &&
            agent.remainingDistance <= agentStopDistance)
        {
            StartCoroutine(WaitAtWaypoint());
        }
    }

    public void BeginPatrol()
    {
        suppressAutoStart = false;

        if (agent == null)
        {
            CacheComponents();
        }

        if (agent == null || waypoints == null || waypoints.Count == 0)
        {
            return;
        }

        if (!agent.isOnNavMesh)
        {
            return;
        }

        isWaiting = false;
        isPatrolling = true;

        agent.isStopped = false;
        agent.ResetPath();

        SelectRandomWaypoint();
        agent.SetDestination(waypoints[randomIndex].position);
    }

    public void StopPatrol(bool suppressNextAutoStart = true)
    {
        if (suppressNextAutoStart)
        {
            suppressAutoStart = true;
        }

        if (agent == null)
        {
            CacheComponents();
        }

        isPatrolling = false;
        isWaiting = false;

        if (agent != null)
        {
            agent.isStopped = true;
            agent.ResetPath();
        }

        if (animator != null)
        {
            animator.SetBool("isWalking", false);
        }
    }

    public bool MoveTo(Vector3 destination)
    {
        if (agent == null)
        {
            CacheComponents();
        }

        if (agent == null || !agent.isOnNavMesh)
        {
            return false;
        }

        isWaiting = false;
        isPatrolling = false;

        agent.isStopped = false;
        agent.ResetPath();

        return agent.SetDestination(destination);
    }

    private void CacheComponents()
    {
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }

        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
    }

    private void UpdateAnimation()
    {
        if (animator == null || agent == null)
        {
            return;
        }

        bool isWalking = agent.velocity.magnitude > 0.1f && !isWaiting && isPatrolling;
        animator.SetBool("isWalking", isWalking);
    }

    private IEnumerator WaitAtWaypoint()
    {
        isWaiting = true;
        isPatrolling = false;
        agent.isStopped = true;

        if (animator != null)
        {
            animator.SetBool("isWalking", false);
        }

        yield return new WaitForSeconds(waitTime);

        SelectRandomWaypoint();

        if (waypoints.Count > 0)
        {
            agent.isStopped = false;
            agent.SetDestination(waypoints[randomIndex].position);
            isPatrolling = true;
        }

        isWaiting = false;
    }

    private void SelectRandomWaypoint()
    {
        if (waypoints.Count == 0)
        {
            return;
        }

        do
        {
            randomIndex = Random.Range(0, waypoints.Count);
        }
        while (waypoints.Count > 1 && randomIndex == lastIndex);

        lastIndex = randomIndex;
    }

    private void PausePatrol(RuntimeDialogueGraph graph, string nodeID)
    {
        if (graph != null)
        {
            string targetID = string.IsNullOrEmpty(nodeID) ? graph.EntryNodeID : nodeID;
            RuntimeDialogueNode startNode = graph.AllNodes.Find(n => n.NodeID == targetID);

            // Ignores phone texts so the AI does not freeze when your phone buzzes
            if (startNode != null && startNode.IsPhoneText)
            {
                return;
            }
        }

        StopPatrol(false);
    }

    private void ResumePatrol()
    {
        if (agent != null && agent.isActiveAndEnabled && waypoints.Count > 0)
        {
            BeginPatrol();
        }
    }
}