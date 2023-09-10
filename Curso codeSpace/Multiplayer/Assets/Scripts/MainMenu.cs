using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class MainMenu : MonoBehaviourPunCallbacks
{
    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void FixedUpdate()
    {
        Debug.Log(PhotonNetwork.IsMasterClient);
        if(PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount > 1)
        {
            PhotonNetwork.LoadLevel(1);
            DestroyImmediate(this);
        }
    }
    public void Play()
    {
        PhotonNetwork.JoinOrCreateRoom("SalaNueva", new RoomOptions() { MaxPlayers = 3 }, TypedLobby.Default);
    }
    
    override
    public void OnConnectedToMaster()
    {
        print("Conectado correctamente");
    }

    override
    public void OnJoinedRoom()
    {
        print("Conectado a la sala");
    }
}
