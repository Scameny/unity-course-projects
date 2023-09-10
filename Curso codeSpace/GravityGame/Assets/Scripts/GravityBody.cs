using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBody : MonoBehaviour
{
    public GravityAttractor attractor = null;
    protected Rigidbody rb;

    public int Grounded
    {
        get
        {
            return _grounded;
        }
    }

    int _grounded = 0;

    protected void AwakeFather()
    {
        rb = GetComponent<Rigidbody>();
        rb.WakeUp();
        rb.useGravity = false;
        rb.freezeRotation = true;
    }


    protected void Gravity()
    {
        if (attractor != null) attractor.Attract(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Planet")) _grounded++;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Planet") && _grounded > 0) _grounded--;
    }
}
