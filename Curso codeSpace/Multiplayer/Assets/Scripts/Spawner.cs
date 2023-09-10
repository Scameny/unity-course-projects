using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Spawner : MonoBehaviour
{
    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            InvokeRepeating("Spawn", 1, 1);
            PhotonNetwork.Instantiate("Player1", new Vector3(0, 0.5f, 0), Quaternion.identity);
            PhotonNetwork.Instantiate("ScoreManager", new Vector3(0, 0, 0), Quaternion.identity);
        }
        else
        {
            PhotonNetwork.Instantiate("Player2", new Vector3(2, 0.5f, 0), Quaternion.identity);
        }
    }

    public void Spawn()
    {
        Vector3 position = new Vector3(Random.Range(-25, 25), 1, Random.Range(-25, 25));
        Vector3 rotation = new Vector3(0, Random.Range(0, 360), 0);

        PhotonNetwork.Instantiate("Door", position, Quaternion.Euler(rotation));
    }
}
