using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class WaypointsAI : MonoBehaviour
{
    NavMeshAgent navMesh;
    public Transform[] waypoints;
    int currentWaypoint = 0;
    // Start is called before the first frame update
    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
        navMesh.SetDestination(waypoints[currentWaypoint].position);
        Debug.Log(currentWaypoint);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(currentWaypoint);
        if (navMesh.remainingDistance < navMesh.stoppingDistance)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
            navMesh.SetDestination(waypoints[currentWaypoint].position);
        }
    }
}
