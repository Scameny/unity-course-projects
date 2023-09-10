using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycast : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;

    public int lengthRay;
    public LayerMask layerMask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, lengthRay, layerMask))
        {
            Debug.Log("Tocando suelo en " + hit.point);
        }
    }
}
