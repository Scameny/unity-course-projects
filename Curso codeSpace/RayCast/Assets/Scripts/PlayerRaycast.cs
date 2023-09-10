using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    public float lengthRay;
    public LayerMask layerMask;

    public GameObject redSphere;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ray.origin = transform.position;
        ray.direction = transform.forward;

        if (Physics.Raycast(ray, out hit, lengthRay, layerMask))
        {
            Debug.Log("Raycast choca con " + hit.collider.name);
            Debug.Log("Raycast choca en " + hit.point);

            if (hit.collider.tag.Equals("MyCube"))
            {
                if (hit.collider.GetComponent<Rigidbody>() == null) hit.collider.gameObject.AddComponent<Rigidbody>();
                hit.collider.GetComponent<Rigidbody>().AddForce(50 * Vector3.up);  
            }
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(redSphere, hit.point, new Quaternion());
            }

        }
        Debug.DrawRay(ray.origin, ray.direction, Color.red);
    }
}
