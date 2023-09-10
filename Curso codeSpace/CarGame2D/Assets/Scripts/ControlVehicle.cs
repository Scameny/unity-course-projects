using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlVehicle : MonoBehaviour
{

    [System.Serializable]
    public class Wheel_Options
    {
        public float dampingRatio = 0.7f;
        public float frequency = 6;
        public float mass = 0.1f;
        public float gravityScale = 1;
    }

    public float maxSpeed = 100;
    public float acceleration = 0.15f;
    public float breakForce = 0.1f;
    public bool reverse = true;
    public float reverseMaxSpeed = 5;
    public float frontForce = 15;
    public float backForce = 15;
    public GameObject[] wheels = new GameObject[2];
    public LayerMask whatIsGround;
    public Wheel_Options wheelOptions;
    public PhysicsMaterial2D groundMat;

    bool grounded;
    float horizontal;
    float vertical;
    Rigidbody2D rb2D;
    // Start is called before the first frame update
    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Start()
    {
        for(int i=0; i < wheels.Length; i++)
        {
            if (wheels[i] == null)
            {
                Debug.Log("Wheel not assigned"); ;
                return;
            }
            else if (wheels[i].GetComponent<CircleCollider2D>() == null)
            {
                Debug.Log("Circle collider not assigned to the wheel");
                return;
            }
            else
            {
                wheels[i].GetComponent<CircleCollider2D>().sharedMaterial = groundMat;
                AddWheeljoint2D(i);
            }
        }
        
    }

    void AddWheeljoint2D(int wheelPosition)
    {
        WheelJoint2D wheelJoint = gameObject.AddComponent<WheelJoint2D>() as WheelJoint2D;
        if (!wheels[wheelPosition].GetComponent<Rigidbody2D>())
            wheels[wheelPosition].AddComponent<Rigidbody2D>();
        wheelJoint.connectedBody = wheels[wheelPosition].GetComponent<Rigidbody2D>();

        JointSuspension2D suspension = new JointSuspension2D();
        suspension.angle = 90;
        suspension.dampingRatio = wheelOptions.dampingRatio;
        suspension.frequency = wheelOptions.frequency;
        wheelJoint.suspension = suspension;
        wheelJoint.anchor = wheels[wheelPosition].transform.localPosition;
        wheels[wheelPosition].GetComponent<Rigidbody2D>().mass = wheelOptions.mass;
        wheels[wheelPosition].GetComponent<Rigidbody2D>().gravityScale = wheelOptions.gravityScale;
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        isGrounded();
    }

    void isGrounded()
    {
        for(int i=0; i < wheels.Length; i++)
        {
            if (Physics2D.OverlapCircle(wheels[i].transform.position, wheels[i].GetComponent<CircleCollider2D>().radius, whatIsGround))
            {
                grounded = true;
                return;
            }
            else grounded = false;
        }
    }
    private void FixedUpdate()
    {
        AccelerateVehicle();
        RotateVehicle();
    }

    void AccelerateVehicle()
    {
        if (horizontal > 0 && grounded)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x + acceleration, rb2D.velocity.y);
            if (rb2D.velocity.x >= maxSpeed)
                rb2D.velocity = new Vector2(maxSpeed, rb2D.velocity.y);
        } else if(horizontal <0 && grounded)
        {
            if (rb2D.velocity.x > acceleration)
            {
                rb2D.velocity = new Vector2(rb2D.velocity.x - breakForce, rb2D.velocity.y);
            } else if (reverse)
            {
                rb2D.velocity = new Vector2(rb2D.velocity.x - acceleration, rb2D.velocity.y);
                if (rb2D.velocity.x <= reverseMaxSpeed * -1)
                {
                    rb2D.velocity = new Vector2(-reverseMaxSpeed, rb2D.velocity.y);
                }
            }
        }
    }

    void RotateVehicle()
    {
        if(vertical >0 && rb2D.velocity.x > acceleration * 2)
        {
            if (!grounded)
            {
                if (rb2D.angularVelocity > (-frontForce * 10))
                    rb2D.AddTorque(-frontForce);
            }
        }
        else if(vertical < 0 && rb2D.velocity.x > acceleration * 2)
        {
            if (!grounded)
            {
                if (rb2D.angularVelocity < (breakForce * 10))
                {
                    rb2D.AddTorque(backForce);
                }
            }
        }
    }
}
