using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float currenthealth;
    public float maxHealth;
    Animator anim;
    public bool damaged;
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Start()
    {
        currenthealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (damaged) return;

        currenthealth -= amount;
        if (currenthealth <= 0)
        {
            Death();
        }
        else
        {
            damaged = true;
            anim.SetBool("Hurt", damaged);
            Invoke("DamagedToFalse", 0.4f);
        }
    }

    void DamagedToFalse()
    {
        damaged = false;
        anim.SetBool("Hurt", damaged);
    }

    void Death()
    {
        anim.SetTrigger("Death");
        Destroy(gameObject, 0.3f);
    }
}
