using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// El player va a tener 3 tipos de ataque:
/// - Disparo parabolico -> ´botón derecho del ratón
/// - Disparo tipo bala a velocidad constante -> botón izquierdo del ratón
/// - Saltar sobre los enemigos
/// </summary>
public class PlayerAttack : MonoBehaviour
{

    public GameObject projectilePrefab;
    public Transform posRight;
    public Transform posLeft;
    public float timeBetweenBullets;

    int direction;
    float timer;
    SpriteRenderer spriteRenderer;
    Vector3 pos;
    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && timer >= timeBetweenBullets) Attack();
        if (Input.GetMouseButtonDown(1) && timer >= timeBetweenBullets) ParabolicAttack();
    }

    void Attack()
    {
        timer = 0;
        GameObject projectileClone = Instantiate(projectilePrefab) as GameObject;
        PositionClone();
        projectileClone.transform.position = pos;
        projectileClone.GetComponent<Projectile>().direction = direction;
    }

    void PositionClone()
    {
        if (!spriteRenderer.flipX)
        {
            pos = posRight.position;
            direction = 1;
        } else if (spriteRenderer.flipX)
        {
            pos = posLeft.position;
            direction = -1;
        }
    }
    void ParabolicAttack()
    {
        timer = 0;
        GameObject projectileClone = Instantiate(projectilePrefab) as GameObject;
        PositionClone();
        projectileClone.transform.position = pos;
        projectileClone.GetComponent<Projectile>().direction = direction;
        projectileClone.GetComponent<Projectile>().parabolShoot();
    }
}
