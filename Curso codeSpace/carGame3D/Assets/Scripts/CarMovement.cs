using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{

    public float speed;
    public float turnSpeed;
    public float hoverForce;
    public float hoverHeight;

    public float factorTurnZAxis;
    public float limitTurnAxisZ;
    public string powerAxis;
    public string turnAxis;

    float powerInput;
    float turnInput;
    Rigidbody rb;
    Ray ray;
    RaycastHit hit;
    CarHealth carHealth;

    void Awake()
    {
        carHealth = GetComponent<CarHealth>();
        rb = GetComponent<Rigidbody>();        
    }

    // Update is called once per frame
    void Update()
    {
        powerInput = Input.GetAxis(powerAxis);
        turnInput = Input.GetAxis(turnAxis);


    }

    private void FixedUpdate()
    {
        ray = new Ray(transform.position, -transform.up);
        if (Physics.Raycast(ray, out hit, hoverHeight))
        {
            float propotionalHeight = (hoverHeight - hit.distance) / hoverHeight;

            Vector3 appliedHoverForce = Vector3.up * hoverForce * propotionalHeight;

            rb.AddForce(appliedHoverForce, ForceMode.Acceleration);

            rb.AddRelativeForce(0, 0, - powerInput * speed);
            rb.AddRelativeTorque(0, turnInput * turnSpeed, 0);

            carHealth.ChangeMaterials(hit.collider.CompareTag("Road"));

        }
        TurnAxis();
    }

    void TurnAxis()
    {
        float zAngle = 0;
        zAngle -= rb.angularVelocity.y * factorTurnZAxis;
        zAngle = Mathf.Clamp(zAngle, -limitTurnAxisZ, limitTurnAxisZ);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, zAngle);
    }
}
