using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    public float smoothing;

    Vector3 offset;
    Vector3 velocity;
    float zCamera;
    // Start is called before the first frame update
    void Start()
    {
        zCamera = transform.position.z;
        offset = target.transform.position - transform.position;
        offset = new Vector3(offset.x, offset.y, zCamera);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!target) return;

        Vector3 destination = target.transform.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, smoothing);
    }
}
