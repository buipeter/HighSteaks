using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAITest2 : MonoBehaviour
{
    public float lookRadius = 10f;
    public Transform[] waypoints;
    int currWaypointIndex = 0;

    Transform target;
    NavMeshAgent enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        enemy.speed = 3;
        enemy.SetDestination(waypoints[currWaypointIndex].position);
    }

    // If we've reached a waypoint, move onto next waypoint
    // Otherwise keep going to current waypoint
    void Patrol()
    {
        enemy.speed = 3;
        if (ReachedDestinationOrNot(enemy))
        {
            currWaypointIndex = (currWaypointIndex + 1) % waypoints.Length;
            enemy.SetDestination(waypoints[currWaypointIndex].position);
        } else
        {
            enemy.SetDestination(waypoints[currWaypointIndex].position);
        }
        //currWaypointIndex = (currWaypointIndex + 1) % waypoints.Length;
    }

    // 3 checks to see if enemy has reached their destination yet
    bool ReachedDestinationOrNot(NavMeshAgent agent)
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }
        return false;
    }

    // Start chasing player
    void Chase()
    {
        enemy.speed = 11;
        enemy.SetDestination(target.position);
    }

    // Update is called once per frame
    void Update()
    {
        // If player is within lookRadius, start chasing
        // Otherwise just patrol
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            Chase();
            if (distance <= enemy.stoppingDistance)
            {
                FaceTarget();
            }
        } else
        {
            Patrol();
        }
    }

    // Making sure character model faces player at all times
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    // Draw lookRadius for debugging
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}