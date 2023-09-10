using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    public float distance;
    public float height;
    public float damping;
    public float rotationDamping;

    // Start is called before the first frame update
    void FixedUpdate()
    {
        Vector3 desiredPosition = target.TransformPoint(0, height, distance);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * damping);

        Quaternion desiredRotation = Quaternion.LookRotation(target.position - transform.position, target.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * rotationDamping);
    }

}
