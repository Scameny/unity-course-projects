using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAudio : MonoBehaviour
{
    public float factorPitch;

    AudioSource audioSource;
    float lowPitch = 0.1f;
    float highPitch = 2;
    Vector3 carVelocity;
    Rigidbody rb;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        carVelocity = rb.velocity;
        float engineVel = Mathf.Abs(carVelocity.z) * factorPitch;
        audioSource.pitch = Mathf.Clamp(engineVel, lowPitch, highPitch);
    }
}
