using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Vector2[] points;
    public int speed;

    Vector3 posToGo;
    int i;
    // Start is called before the first frame update
    void Start()
    {
        posToGo = points[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == posToGo)
        {
            i = (i + 1) % points.Length;
            posToGo = points[i];
        }
        

        transform.position = Vector3.MoveTowards(transform.position, posToGo, speed * Time.deltaTime);
    }
}
