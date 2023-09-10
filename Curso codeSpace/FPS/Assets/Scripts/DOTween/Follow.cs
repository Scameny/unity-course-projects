using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Follow : MonoBehaviour
{

    public Transform target;
    Vector3 targetLastPost;
    Tweener tween;
    // Start is called before the first frame update
    void Start()
    {
        tween = transform.DOMove(target.position, 2).SetAutoKill(false);
        targetLastPost = target.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetLastPost == transform.position) return;

        tween.ChangeEndValue(target.position, true).Restart();
        targetLastPost = target.position;
    }
}
