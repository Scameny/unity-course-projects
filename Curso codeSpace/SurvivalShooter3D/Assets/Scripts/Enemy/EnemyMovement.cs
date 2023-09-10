using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

    GameObject player;
    Animator anim;
    NavMeshAgent agent;
    EnemyHealth enemyHealth;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth.isDead) return;
        agent.SetDestination(player.transform.position);

        float h = agent.velocity.x;
        float v = agent.velocity.z;
        bool moving = h != 0 || v != 0;
        anim.SetBool("isMoving", moving);
    }
}
