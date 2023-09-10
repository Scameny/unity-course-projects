using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    [Header("Patrol")]
    Vector3 destination;
    public LayerMask mask;

    [Header("Health")]
    public float maxHealth;
    float health;
    public Slider sliderHealth;

    [Header("Alert")]
    public float visionAngle;
    public float visionRange;
    Transform player;

    NavMeshAgent agent;
    Animator anim;
    
    [Header("Attack")]
    public Collider col;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        health = sliderHealth.value = sliderHealth.maxValue = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine("Patrol");
        StartCoroutine("Alert");
    }

    public void RandomDestination()
    {
        Vector3 randomPoint = transform.position + Random.insideUnitSphere * 20;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomPoint, out hit, 50, mask);
        destination = hit.position;
    }


    private IEnumerator Alert()
    {
        while (true)
        {
            if (Vector3.Distance(transform.position, player.position) < visionRange)
            {
                Vector3 direction = player.position - transform.position;
                if (Vector3.Angle(direction, transform.forward) < visionAngle)
                {
                    StartCoroutine("Attack");
                    print("jugador detectado");
                }
            }
            yield return new WaitForFixedUpdate();
        }
    }
    private IEnumerator Patrol()
    {
        while(true)
        {
            RandomDestination();
            agent.SetDestination(destination);
            anim.Play("Walk");
            while(Vector3.Distance(transform.position, destination) > 2)
            {
                yield return new WaitForEndOfFrame();
            }
            agent.SetDestination(transform.position);
            anim.Play("Idle");
            yield return new WaitForSeconds(Random.Range(1.5f, 4f));
        }
    }

    private IEnumerator Attack()
    {
        StopCoroutine("Patrol");
        StopCoroutine("Alert");
        while (true)
        {
            anim.Play("Run");
            while(Vector3.Distance(transform.position, player.position) > 1)
            {
                agent.SetDestination(player.position);
                yield return new WaitForFixedUpdate();
            }
            anim.Play("Attack");

            yield return new WaitForSeconds(2);
        }
    }

    // Update is called once per frame

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FireBall"))
        {
            StopAllCoroutines();
            StartCoroutine("Attack");
            sliderHealth.gameObject.SetActive(true);
            health--;
            sliderHealth.value = health;
        }
        if (other.CompareTag("AoEFire"))
        {
            StopAllCoroutines();
            StartCoroutine("Attack");
            sliderHealth.gameObject.SetActive(true);
            health -= 2;
            sliderHealth.value = health;

        }
        if (health < 0)
            {
                //GameOver
                GetComponent<Collider>().enabled = false;
                sliderHealth.gameObject.SetActive(false);
                anim.Play("Death");
                StopAllCoroutines();
            }
    }

    public void ActivateDamageCollider()
    {
        col.enabled = true;

    }
    public void DesactivateDamageCollider()
    {
        col.enabled = false;
    }
}
