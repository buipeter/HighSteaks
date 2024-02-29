using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAITest : MonoBehaviour
{
    public NavMeshAgent enemy;
    public Transform player;
    public LayerMask playerMask, groundMask;
    public Vector3 moveDirection;
    public float gravityScale;
    public float sightRange;
    public bool playerInSight;
    
    // Patrol
    public Vector3 walkTo;
    public float walkToRange;
    bool walkPointFound;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        enemy = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInSight = Physics.CheckSphere(transform.position, sightRange, playerMask);
        if (!playerInSight)
        {
            Patrol();
        } else
        {
            Chase();
        }

        ApplyGravity();
    }

    private void Patrol()
    {
        if (!walkPointFound)
        {
            float randomZ = Random.Range(-walkToRange, walkToRange);
            float randomX = Random.Range(-walkToRange, walkToRange);
            walkTo = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
            
            if (Physics.Raycast(walkTo, -transform.up, 2f, groundMask))
            {
                walkPointFound = true;
            }
        } else
        {
            enemy.SetDestination(walkTo);
        }

        Vector3 distanceToDestination = transform.position - walkTo;
        if (distanceToDestination.magnitude < 1f)
        {
            walkPointFound = false;
        }
    }

    private void Chase()
    {
        enemy.SetDestination(player.position);
    }

    private void ApplyGravity()
    {
        moveDirection.y += Physics.gravity.y * gravityScale * Time.deltaTime;
    }
}
