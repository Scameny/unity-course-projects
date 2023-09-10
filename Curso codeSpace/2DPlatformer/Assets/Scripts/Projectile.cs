using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int speed;
    public int direction;
    public bool parabol;
    public int damage;
    public float parabolShootForce;
    public float parabolRotateForce;

    Rigidbody2D rb;
    // Start is called before the first frame update

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Destroy(gameObject, 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (!parabol)
        {
            rb.isKinematic = true;
            transform.Translate(direction * speed * Vector2.right * Time.deltaTime);
        }
    }

    public void parabolShoot()
    {
        parabol = true;
        GetComponent<CircleCollider2D>().isTrigger = false;
        Vector2 force = (Vector2.up + Vector2.right * direction) * parabolShootForce;
        rb.AddTorque(parabolRotateForce * direction, ForceMode2D.Impulse);
        rb.AddForce(force, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (!enemyHealth.damaged)
            {
                collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (!enemyHealth.damaged)
            {
                collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
