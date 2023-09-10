using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    public float attackDistance;
    public Transform[] points;
    public zombieState state;
    public float alertDistance;
    public GameObject attackSphere;
    public float timeToStop;
    public enum zombieState { Idle, Patrol, Alert, Attack}
    Animator anim;
    int currentPos;
    NavMeshAgent agent;
    Transform player;
    float timerPatrol = 0;
    float timerIdle = 0;
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case zombieState.Idle:
                Idle();
                break;
            case zombieState.Patrol:
                Patrol();
                break;
            case zombieState.Alert:
                Alert();
                break;
            case zombieState.Attack:
                Attack();
                break;
        }
    }

    void Patrol()
    {
        agent.isStopped = false;
        agent.SetDestination(points[currentPos].position);
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            currentPos = (currentPos + 1) % points.Length;
        }
        anim.SetBool("isWalking", true);
        anim.SetInteger("Walk", Random.Range(0, 2));
        CheckDistanceToPlayer();
        timerPatrol += Time.deltaTime;
        if (timerPatrol >= timeToStop)
        {
            state = zombieState.Idle;
            timerPatrol = 0;
        }
    }

    void Idle()
    {
        StopZombie();
        CheckDistanceToPlayer();
        timerIdle += Time.deltaTime;
        if (timerPatrol >= timeToStop)
        {
            state = zombieState.Patrol;
            timerIdle = 0;
        }
    }

    void Alert()
    {
        StopZombie();
        TurnToPoint(player.position);
        CheckDistanceToPlayer();
    }

    void StopZombie()
    {
        agent.isStopped = true;
        anim.SetBool("isWalking", false);
        anim.SetBool("isRunning", false);
    }

    void TurnToPoint(Vector3 point)
    { 
        Vector3 zombieToPoint = point - transform.position;
        zombieToPoint.y = 0;
        Quaternion rotation = Quaternion.LookRotation(zombieToPoint);
        transform.rotation = rotation;
    }

    void CheckDistanceToPlayer()
    {
        float d = Vector3.Distance(transform.position, player.position);
        if (d <= attackDistance) state = zombieState.Attack;
        else if (d <= alertDistance)
        {
            state = zombieState.Alert;
        }
        else if (d > alertDistance && state == zombieState.Alert)
        {
            state = zombieState.Patrol;
        }
    }

    void Attack()
    {
        CheckDistanceToPlayer();
        Vector3 zombiePosition = transform.position;
        zombiePosition.y = 0;
        Vector3 playerPosition = player.position;
        playerPosition.y = 0;
        if (Vector3.Distance(zombiePosition, playerPosition) <= agent.stoppingDistance)
        {
            StopZombie();
            anim.SetTrigger("Attack");
        }
        else if(Vector3.Distance(zombiePosition, playerPosition) > agent.stoppingDistance + 0.2f)
        {
            if (agent.isStopped) agent.isStopped = false;
            agent.SetDestination(player.position);
            anim.SetBool("isRunning", true);
        }
    }

    public void ActivateAttackSphere()
    {
        StartCoroutine("AttackSphere");
    }

    IEnumerator AttackSphere()
    {
        attackSphere.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        attackSphere.SetActive(false);
    }
}
