using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform leftLimit;
    public Transform rightLimit;
    public Transform bird;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = transform.position;
        newPosition.x = bird.position.x;
        newPosition.x = Mathf.Clamp(newPosition.x, leftLimit.position.x, rightLimit.position.x);
        transform.position = newPosition;
    }
}
