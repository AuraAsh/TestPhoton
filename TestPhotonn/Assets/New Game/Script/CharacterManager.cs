using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Security.Cryptography;

public class CharacterManager : MonoBehaviourPun, IPunObservable
{
    [SerializeField] internal Transform newPositionBullet;
    public int life = 100;
    Objectspool objectspool; // Almaceno variable a la funcion
    //public Transform mira;
    ControlCube controlC;
    int tempLife;
    // Start is called before the first frame update
     void Start()
    {
        controlC = GetComponent<ControlCube>();
        objectspool = Objectspool.Instance; // Asignacion grupo de objetos
      
        if (!this.photonView.IsMine)//No soy yo
        {
            Destroy(controlC);
        }
    }


    public void ShootB() 
    {
        this.photonView.RPC("RPC_Ball", RpcTarget.All);
    //    this.photonView.RPC("RPC_Life", RpcTarget.AllBuffered);
    }

    [PunRPC]
    void RPC_Ball()
    {
        Objectspool.Instance.SpawnFromPool("sphere", newPositionBullet.position, newPositionBullet.rotation); // Llamada a la funcion, cito
    }

 //   [PunRPC]
  //  void RPC_Life()
  //  {
        
  //  }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(life);
        }
        else if (stream.IsReading) {
            life = (int)stream.ReceiveNext();
        }

    }
}
