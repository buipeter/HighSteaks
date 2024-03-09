using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAITest2 : MonoBehaviour
{
    public NavMeshAgent enemy;
    public float waitTime = 4;
    public float timeToRotate = 2;
    public float walkSpeed = 6;
    public float runSpeed = 9;

    public float viewRadius = 15;
    public float viewAngle = 90;

    public LayerMask playerMask;
    public LayerMask groundMask;

    public float meshRes = 1f;
    public int edgeIterations = 4;
    public float edgeDistance = 0.5f;
    
    Vector3 playerLastPos = Vector3.zero;
    Vector3 playerPos;

    public Transform[] destinations;
    int currDestinIndex;

    float m_waitTime;
    float m_timeToRotate;
    bool playerInRange;
    bool playerNear;
    bool patrolling;
    bool playerCaught;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = Vector3.zero;
        patrolling = true;
        playerCaught = false;
        playerInRange = false;
        m_waitTime = waitTime;
        m_timeToRotate = timeToRotate;

        currDestinIndex = 0;
        enemy = GetComponent<NavMeshAgent>();

        enemy.isStopped = false;
        enemy.speed = walkSpeed;
        enemy.SetDestination(destinations[currDestinIndex].position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CatchPlayer()
    {
        playerCaught = true;
    }
}
