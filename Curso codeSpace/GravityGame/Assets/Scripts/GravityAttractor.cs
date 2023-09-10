using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAttractor : MonoBehaviour
{
    public float gravity;


    public void Attract(GravityBody body)
    {
        Transform transformBody = body.transform;
        Rigidbody rb = body.GetComponent<Rigidbody>();

        Vector3 gravityUp = transformBody.position - transform.position;
        gravityUp.Normalize();

        rb.AddForce(gravityUp * gravity * rb.mass);

        if (body.Grounded >= 1) rb.drag = 0.1f;
        else rb.drag = 1;

        if (rb.freezeRotation == true)
        {
            Quaternion quaternion = Quaternion.FromToRotation(transformBody.up, gravityUp);
            quaternion *= transformBody.rotation;
            transformBody.rotation = Quaternion.Slerp(transformBody.rotation, quaternion, 0.1f);
        }
    }
}
