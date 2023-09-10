using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    Rigidbody rig;
    Animator anim;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        rig.velocity = Input.GetAxis("Vertical") * speed * Vector3.forward + Input.GetAxis("Horizontal") * speed * Vector3.right + Vector3.up * rig.velocity.y;
        if (rig.velocity.magnitude > 0.2)
            transform.LookAt(transform.position + new Vector3(rig.velocity.x, 0, rig.velocity.z));
        anim.SetFloat("Velocity", rig.velocity.magnitude);
    }
}
