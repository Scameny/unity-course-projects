using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Player : MonoBehaviour
{
    public float speed;
    public float speedRot;
    Rigidbody rig;
    int score = 0;

    private void Start()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            rig = GetComponent<Rigidbody>();
            Camera.main.transform.SetParent(transform);
            Camera.main.transform.localPosition = new Vector3(-5, 2, 0);
        }
        else
            Destroy(this);
    }

    private void Update()
    {

        // Movimiento
        Vector3 movement = transform.up * rig.velocity.y + transform.right * Input.GetAxis("Vertical");
        rig.velocity = movement.normalized * speed * Time.deltaTime;

        transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * speedRot);
    }
}
