using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Velocity")]
    public int speed;
    public float acceleration;


    [Header("Raycast")]
    public Transform groundCheck;
    public LayerMask groundMask;
    public float rayLength;

    [Header("Jump")]
    public int jumpForce;

    bool isGrounded;


    Animator anim;
    Rigidbody2D rb;
    Vector2 targetVelocity;
    Vector3 dampVelocity;
    SpriteRenderer spriteRenderer;
    bool jumpPressed;
    private void Awake()
    { 
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");

        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, rayLength, groundMask);
        jumpPressed = Input.GetKeyDown(KeyCode.Space) && isGrounded;
        anim.SetBool("isJumping", !isGrounded);
        targetVelocity = new Vector2(h * speed, rb.velocity.y);
        Animating(h);
        Flip(h);
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref dampVelocity, acceleration);
        if (jumpPressed) Jump();
    }

    void Animating(float _h)
    {
        if (_h != 0) anim.SetBool("isRunning", true);
        else anim.SetBool("isRunning", false);
    }

    void Flip(float _h)
    {
        if (_h > 0) spriteRenderer.flipX = false;
        else if (_h < 0) spriteRenderer.flipX = true;
    }

    void Jump()
    {
        jumpPressed = false;
        rb.AddForce(Vector2.up * jumpForce);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Platform"))
        {
            transform.SetParent(collision.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Platform"))
        {
            transform.SetParent(null);
        }
    }
}
