using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float forceJump;
    public LayerMask groundMask;
    public float lengthRay;
    public bool isGrounded;

    Ray ray;
    RaycastHit hit;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        ray.origin = transform.position;
        ray.direction = -Vector3.up;

        if (Physics.Raycast(ray, out hit, lengthRay, groundMask))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * forceJump);
        }
    }
}
