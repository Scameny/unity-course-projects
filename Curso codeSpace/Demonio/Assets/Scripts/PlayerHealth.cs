using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{

    public float maxHealth;
    float health;
    public Slider sliderHealth;
    public float inmuneTime;

    // Start is called before the first frame update
    void Start()
    {
        health = sliderHealth.value = sliderHealth.maxValue = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (inmuneTime > 0)
        {
            inmuneTime -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DamageEnemy") && inmuneTime <= 0)
        {
            health--;
            sliderHealth.value = health;
            inmuneTime = 2;
            if (health <= 0)
            {
                GetComponent<Animator>().Play("Death");
                GetComponent<PlayerMove>().enabled = false;
                GetComponent<PlayerAttack>().enabled = false;
                enabled = false;
                Debug.Log("You are death");
            }
        }
    }
}
