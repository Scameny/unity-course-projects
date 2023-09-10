using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    Transform player;
    public bool combat;
    public Vector2 limitMax, limitMin;

    Vector3 destination;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Start()
    {
        destination = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (combat)
            transform.position = new Vector3(0, 0, -10);
        else
        {
            if (player.position.x > limitMin.x && player.position.x < limitMax.x)
                destination.x = player.position.x;
            if (player.position.y > limitMin.y && player.position.y < limitMax.y)
                destination.y = player.position.y;
            transform.position = destination;
        }
       
    }
}
