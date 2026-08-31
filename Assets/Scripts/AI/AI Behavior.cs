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

    private int randomIndex;
    private int lastIndex;
    private float agentStopDistance = 1f;

    private bool isPatrolling = false;
    private bool isWaiting = false;

    private void OnValidate()
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

    private void Start()
    {
        Debug.Log("AIBehavior Start called");

        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }

        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }

        Debug.Log($"Waypoints count: {waypoints.Count}");
        Debug.Log($"Agent found: {agent != null}");
        Debug.Log($"Agent enabled: {agent.enabled}");
        Debug.Log($"Agent on NavMesh: {agent.isOnNavMesh}");
        Debug.Log($"Animator found: {animator != null}");

        if (waypoints.Count == 0 || agent == null)
        {
            Debug.LogWarning("AIBehavior: Missing agent or waypoints.");
            return;
        }

        if (!agent.isOnNavMesh)
        {
            Debug.LogWarning("AIBehavior: Agent is not on the NavMesh.");
            return;
        }

        agent.isStopped = false;
        agent.ResetPath();

        SelectRandomWaypoint();

        Debug.Log($"Setting destination to: {waypoints[randomIndex].name}");

        agent.SetDestination(waypoints[randomIndex].position);

        isPatrolling = true;
    }

    private void Update()
    {
        if (agent == null || waypoints.Count == 0)
        {
            return;
        }

        UpdateAnimation();

        if (isWaiting)
        {
            return;
        }

        if (!agent.pathPending &&
            agent.hasPath &&
            agent.remainingDistance <= agentStopDistance)
        {
            Debug.Log("Reached waypoint");

            StartCoroutine(WaitAtWaypoint());
        }
    }

    private void UpdateAnimation()
    {
        if (animator == null)
        {
            return;
        }

        // Check if the NavMeshAgent is actually moving
        bool isWalking = agent.velocity.magnitude > 0.1f && !isWaiting;

        animator.SetBool("isWalking", isWalking);
    }

    private IEnumerator WaitAtWaypoint()
    {
        isWaiting = true;
        isPatrolling = false;

        agent.isStopped = true;

        // Make sure walking animation stops immediately
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
}