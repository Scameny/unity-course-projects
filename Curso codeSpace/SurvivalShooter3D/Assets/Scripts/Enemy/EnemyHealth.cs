using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    public float sinkSpeed = 2.5f;
    public bool isDead;
    public int scoreValue = 10;
    public AudioClip deathClip;
    public GameObject particleSystem;


    Animator anim;
    AudioSource audioSource;
    EnemyManager enemyManager;
    CapsuleCollider capsuleCollider;

    
    bool isSinking;
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        enemyManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<EnemyManager>();
        currentHealth = maxHealth;
    }


    // Update is called once per frame
    void Update()
    {
        if (isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        if (isDead) return;

        audioSource.Play();

        GameObject hitParticles = Instantiate(particleSystem, hitPoint, Quaternion.LookRotation(transform.forward, transform.up));

        particleSystem.transform.position = hitPoint;
        particleSystem.GetComponent<ParticleSystem>().Play();
        Destroy(hitParticles, particleSystem.GetComponent<ParticleSystem>().main.duration);
        currentHealth -= amount;
        if (currentHealth <= 0) Death();
    }

    void Death()
    {
        isDead = true;
        capsuleCollider.isTrigger = true;
        anim.SetTrigger("Death");
        enemyManager.EnemyDeath();
        audioSource.clip = deathClip;
        audioSource.Play();
    }

    void StartSinking()
    {
        isSinking = true;
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        GetComponent<CapsuleCollider>().isTrigger = true;


        GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreManager>().UpdateScore(scoreValue);
        GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreManager>().UpdateNumberEnemies(true);
        Destroy(gameObject, 2);
    }
}
