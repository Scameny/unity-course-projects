using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 100;
    public LayerMask shootableMask;
    public float effectsDisplayTime = 0.2f;


    float timer;
    Ray ray;
    RaycastHit hit;


    LineRenderer lineRenderer;
    Light gunLight;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        gunLight = GetComponent<Light>();
        audioSource = GetComponent<AudioSource>();    
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetMouseButton(0) && timer >= timeBetweenBullets) Shoot();
        if (timer >= timeBetweenBullets * effectsDisplayTime) DisableEffects();
    }

    void Shoot()
    {
        timer = 0;
        audioSource.Play();
        gunLight.enabled = true;
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, transform.position);

        ray.origin = transform.position;
        ray.direction = transform.forward;

        if (Physics.Raycast(ray, out hit, range, shootableMask))
        {
            if(hit.collider.GetComponent<EnemyHealth>() != null)
            {
                hit.collider.GetComponent<EnemyHealth>().TakeDamage(damagePerShot, hit.point);
            }
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            lineRenderer.SetPosition(1, ray.origin + ray.direction * range);
        }
    }

    void DisableEffects()
    {
        gunLight.enabled = false;
        lineRenderer.enabled = false;
    }
}
