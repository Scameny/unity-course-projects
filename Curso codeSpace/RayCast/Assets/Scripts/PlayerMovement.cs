using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float turnSpeed;
    Animator anim;
    public GameObject sphereAttack;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Movement(h, v);
    }

    void Movement(float h, float v)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") || anim.GetCurrentAnimatorStateInfo(0).IsName("SlowAttack"))
            return;

        if (v > 0)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        transform.Translate(v * Vector3.forward * speed * Time.deltaTime);
        transform.Rotate(h * Vector3.up * turnSpeed * Time.deltaTime);
        Attack(v);
    }

    void Attack(float v)
    {
        if (v != 0) return;
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("attack");
        }

        if (Input.GetMouseButtonDown(1))
        {
            anim.SetTrigger("slowAttack");
        }
    }

    void ActivateSphereAttack()
    {
        sphereAttack.SetActive(true);
        Invoke("DesactivateSphereAttack", 0.1f);
    }

    void DesactivateSphereAttack()
    {
        sphereAttack.SetActive(false);
    }
}
