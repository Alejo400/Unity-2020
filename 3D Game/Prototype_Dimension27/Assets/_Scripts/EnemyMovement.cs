using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    public float speedToFollowTarget = 4;

    NavMeshAgent navMesh;
    public Transform[] waypoints;
    public GameObject target;
    Rigidbody _rigibody;

    int currentWaypoints = 0, randomWaypoint;
    public bool isFirstDimensionActive = true, isSecondDimensionActive = false;

    Vector3 lookDirection;
    // Start is called before the first frame update
    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
        _rigibody = GetComponent<Rigidbody>();
        navMesh.SetDestination(waypoints[currentWaypoints].position);
    }

    // Update is called once per frame
    void Update()
    {
        if (isFirstDimensionActive)
            runAway();
        else
            Followtarget();
    }
    /// <summary>
    /// El agent AI correra en los puntos o waypoints asignados en la escena (lejos del player)
    /// (Primera dimension)
    /// </summary>
    void runAway()
    {
        _rigibody.velocity = Vector3.zero; //detener la velocidad del rigibody para evitar bug
        randomWaypoint = Random.Range(0, waypoints.Length);

        if (navMesh.remainingDistance < navMesh.stoppingDistance)
        {
            currentWaypoints = randomWaypoint;
            navMesh.SetDestination(waypoints[currentWaypoints].position);
        }
    }
    /// <summary>
    /// Perseguir al jugador, una vez la sala cambia. (Segunda dimension)
    /// </summary>
    void Followtarget()
    {
            lookDirection = target.transform.position - transform.position;
            lookDirection.Normalize();
            _rigibody.velocity = lookDirection * speedToFollowTarget;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
