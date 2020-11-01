using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ConecctToPhoton : MonoBehaviourPunCallbacks
{
   
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Jugador Conectado al servidor Maestro");
        PhotonNetwork.AutomaticallySyncScene = true;// Para cuartos de logueo rápido
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No hay cuartos para unirse, creando...");
        int RandoRoomCode = Random.Range(0, 1000);
        RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 20 };
        PhotonNetwork.CreateRoom("RoomID" + RandoRoomCode, roomOptions);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Ya existe ese cuarto");
        int RandoRoomCode = Random.Range(0, 1000);
        RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 20 };
        PhotonNetwork.CreateRoom("RoomID" + RandoRoomCode, roomOptions);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Usuario se unió a un cuarto");
        PhotonNetwork.Instantiate("Cube",
          new Vector3(Random.Range(-5,5),0.5f, Random.Range(-5, 5)), 
          Quaternion.identity, 0);
    }


}
