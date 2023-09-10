using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bullet;
    // Start is called before the first frame update
    Rigidbody rb;
    Transform parent;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
