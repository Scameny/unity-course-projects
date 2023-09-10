using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DOTweenBasics : MonoBehaviour
{
    public Vector3 pos;
    public Vector3 rot;
    public float time;
    // Start is called before the first frame update

    Tween t;
    void Start()
    {
        t = transform.DOMove(pos, time).SetEase(Ease.InElastic).OnComplete(ChangeMaterial);
        // transform.DORotate(rot, time);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O)) t.Pause();
        else if (Input.GetKeyDown(KeyCode.T)) t.Play();
    }

    void ChangeMaterial()
    {
        GetComponent<Renderer>().material.DOColor(Color.yellow, 2).SetLoops(-1, LoopType.Yoyo);

    }
}
