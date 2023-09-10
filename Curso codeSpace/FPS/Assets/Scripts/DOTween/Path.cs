using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Path : MonoBehaviour
{
    public PathType pathType;
    public Vector3[] waypoints = new[]
    {
        new Vector3(4,2,6),
        new Vector3(8,6,14),
        new Vector3(4,6,14),
        new Vector3(0,6,6),
        new Vector3(-3,0,0),
    };

    Tween t;
    // Start is called before the first frame update
    void Start()
    {
        t = transform.DOPath(waypoints, 4, pathType).SetOptions(true).SetLookAt(0.001f);
        t.SetEase(Ease.Linear).SetLoops(-1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
