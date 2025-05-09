using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class SafariAgent : MonoBehaviour
{
    public List<Transform> wayPoint;
    private NavMeshAgent navMeshAgent;
    public int currentWaypointIndex;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        if (wayPoint.Count == 0)
        {
            return;
        }
        if(navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !navMeshAgent.pathPending)
        {
            Walking();
        }
    }
    void Walking()
    {
        if (wayPoint.Count == 0)
        {
            return;
        }
        if (currentWaypointIndex < wayPoint.Count)
        {
            currentWaypointIndex++;
        }
        else
        {
            currentWaypointIndex = 0;
        }
        navMeshAgent.SetDestination(wayPoint[currentWaypointIndex].position);
    }
}
