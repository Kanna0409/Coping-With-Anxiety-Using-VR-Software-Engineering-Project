using UnityEngine;
using System.Collections.Generic;

public class Boatingagent : MonoBehaviour
{
    public List<Transform> wayPoint;
    float speed = 1.5f;
    public float reachThreshold = 1f;
    public int currentWaypointIndex = 0;

    void Update()
    {
        if (wayPoint.Count == 0)
        {
            return;
        }

        MoveTowardsWaypoint();
    }

    void MoveTowardsWaypoint()
    {
        Transform targetWaypoint = wayPoint[currentWaypointIndex];
        Vector3 direction = (targetWaypoint.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, targetWaypoint.position);
        transform.position += direction * speed * Time.deltaTime;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 2f);

        if (distance <= reachThreshold)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % wayPoint.Count;
        }
    }
}

