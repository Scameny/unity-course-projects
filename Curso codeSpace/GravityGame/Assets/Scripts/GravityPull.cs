using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityPull : GravityBody
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (attractor != null) attractor.Attract(this);
    }
}
