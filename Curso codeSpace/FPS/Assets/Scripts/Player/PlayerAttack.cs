using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject redSphere;
    public Material matA;
    public Material matB;
    public LayerMask shootableLayer;
    LineRenderer lineRenderer;
    Light gunLight;
    Ray ray;
    RaycastHit hit;
    bool attack;
    // Start is called before the first frame update
    void Start()
    {
        gunLight = GetComponent<Light>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        ray.origin = transform.position;
        ray.direction = transform.forward;

        if (Physics.Raycast(ray, out hit))
        {
           
            if(hit.collider.gameObject.layer == 6)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    attack = true;
                    ActivateLineRenderer(matB, hit.point, true);
                }
                redSphere.SetActive(true);
                if (!attack) lineRenderer.enabled = false;
                redSphere.transform.position = hit.point;
            }
            else
            {
                ActivateLineRenderer(matA, hit.point, false);
            }
        }
        else
        {
            lineRenderer.enabled = false;
            redSphere.SetActive(false);
        }
    }

    void ActivateLineRenderer(Material material, Vector3 point, bool attack)
    {
        redSphere.SetActive(false);
        lineRenderer.enabled = true;
        lineRenderer.material = material;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, point);

        if (attack)
        {
            gunLight.enabled = true;
            Invoke("DesactivateEffects", 0.2f);
        }
    }

    void DesactivateEffects()
    {
        attack = false;
        gunLight.enabled = false;
    }
}
