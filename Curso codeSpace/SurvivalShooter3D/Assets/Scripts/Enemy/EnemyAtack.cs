using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAtack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;

    Animator anim;
    GameObject player;
    EnemyHealth enemyHealth;
    bool playerInRange;
    float timer;
    PlayerHealth playerHealth;



    public void Awake()
    {
        anim = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player) playerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player) playerInRange = false;
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer>= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            Attack();
        }
        
    }

    void Attack()
    {
        timer = 0;
        playerHealth.TakeDamage(attackDamage);
    }
}
