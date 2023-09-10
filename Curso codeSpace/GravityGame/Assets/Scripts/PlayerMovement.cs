using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : GravityBody
{

    public GameObject planet;
    public float maxSpeed = 15;
    public float jumpSpeed = 1;
    public float rotationSpeed = 2;
    public int invert = -1;
    public float distanceToPlanet;
    public float force = 12;
    
    
    bool jump;

    float horizontal;
    float vertical;
    float rotate;
    Animator anim;
    Ray ray;
    RaycastHit hit;


    private void Awake()
    {
        AwakeFather();
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        rotate = Input.GetAxis("rotateAx");
        if (Input.GetKeyDown(KeyCode.Space)) Jump();
    }

    void FixedUpdate()
    {
        Gravity();
        anim.SetFloat("Velocity", rb.velocity.magnitude);
        if (rb.velocity.magnitude < maxSpeed)
        {
            rb.AddForce(transform.rotation * Vector3.forward * vertical * force);
            rb.AddForce(transform.rotation * Vector3.right * horizontal * force);
        }

        transform.Rotate(Vector3.up * rotate * rotationSpeed);
        if (jump)
        {
            anim.SetTrigger("Jump");
            rb.velocity = rb.velocity + (rb.transform.up * jumpSpeed);
            jump = false;
        }
        ray = new Ray();
        ray.origin = transform.position;
        ray.direction = planet.transform.position - transform.position;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Planet"))
            {
                if (hit.distance <= distanceToPlanet) attractor = hit.collider.gameObject.GetComponent<Planet>().gravityAttractor;
                Debug.Log("Distance" + hit.distance);
            }
        }
        
    }

    void Jump()
    {
        jump = true;
        if (Grounded == 0)
        {
            jump = false;
        }
    }
}
