using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
public class Door : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Player") && PhotonNetwork.IsMasterClient)
        {
            if (other.gameObject.name.Contains("Player1"))
                ScoreManager.sm.GetComponent<PhotonView>().RPC("UpdateScore", RpcTarget.All, 1);
            else
                ScoreManager.sm.GetComponent<PhotonView>().RPC("UpdateScore", RpcTarget.All, 2);
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
