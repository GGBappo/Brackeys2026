using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIBehavior : MonoBehaviour
{
    [SerializeField] private List<Transform> waypoints = new List<Transform>();
    [SerializeField] private NavMeshAgent agent;
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
    }

    private void Start()
    {

        if (waypoints.Count == 0 || agent == null)
        {
            return;
        }

        SelectRandomWaypoint();
        agent.SetDestination(waypoints[randomIndex].position);
        isPatrolling = true;
    }

    private void Update()
    {
        if (agent == null || waypoints.Count == 0 || isWaiting)
        {
            return;
        }

        if (!agent.pathPending && agent.hasPath && agent.remainingDistance <= agentStopDistance)
        {
            StartCoroutine(WaitAtWaypoint());
        }
    }

    private IEnumerator WaitAtWaypoint()
    {
        isWaiting = true;
        isPatrolling = false;
        agent.isStopped = true;

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
            return;

        do
        {
            randomIndex = Random.Range(0, waypoints.Count);
        }
        while (waypoints.Count > 1 && randomIndex == lastIndex);

        lastIndex = randomIndex;
    }
}
