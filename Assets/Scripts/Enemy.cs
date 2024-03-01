using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public enum State
    {
        Patrolling,
        Chasing,
        Attacking
    }

    public State currentState;
    private NavMeshAgent agent;
    public Transform player;

    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float detectionRange = 15;
    [SerializeField] private float attackingRange = 5;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        SetRandomPoint();
        currentState = State.Patrolling;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case State.Patrolling:
                Patrol();
            break;

            case State.Chasing:
                Chase();
            break;

            case State.Attacking:
                Attack();
            break;
        }
    }

    void Patrol()
    {
        if(IsInRange (detectionRange) == true)
        {
            currentState = State.Chasing;
        }

        if(agent.remainingDistance < 0.5f)
        {
            SetRandomPoint();
        }
    }

    void Chase()
    {
        if(IsInRange (detectionRange) == false)
        {
            SetRandomPoint();
            currentState = State.Patrolling;
        }

        if(IsInRange (attackingRange) == true)
        {
            currentState = State.Attacking;
        }

        agent.destination = player.position;
    }

    void Attack()
    {
        Debug.Log("Atacando");
        currentState = State.Chasing;
    }

    void SetRandomPoint()
    {
        agent.destination = patrolPoints[Random.Range (0, 4)].position;
    }

    bool IsInRange (float range)
    {
        if(Vector3.Distance (transform.position, player.position) < range)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
