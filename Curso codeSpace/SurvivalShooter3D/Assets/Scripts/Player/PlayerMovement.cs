using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int speed;
    public LayerMask floorMask;

    Animator anim;
    Rigidbody rb;
    Vector3 movement;
    Ray ray;
    RaycastHit hit;

    float rayLength = 100;
    float horizontal, vertical;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        Move();
        Animating();
        Turning();
    }

    void Move()
    {
        movement.Set(horizontal, 0, vertical);
        movement.Normalize();
        rb.MovePosition(transform.position + movement * speed * Time.deltaTime);
    }

    void Animating()
    {
        bool moving = horizontal != 0 || vertical != 0;
        anim.SetBool("isMoving", moving);
    }

    void Turning()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow);
        if (Physics.Raycast(ray, out hit, rayLength, floorMask))
        {
            Vector3 playerToMouse = hit.point - transform.position;
            playerToMouse.y = 0;
            Quaternion rotation = Quaternion.LookRotation(playerToMouse);
            rb.MoveRotation(rotation);
        }
    }
}
