﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class netSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform netSpawn;
    [SerializeField]
    private GameObject netPrefab;
    private PhotonView PV;

    private GameObject net;

	// Use this for initialization
	void Start ()
    {
       PV = GetComponent<PhotonView>();
        CallSpawnNet();
    }


    private void CallSpawnNet()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PV.RPC("RPC_SpawnNet", RpcTarget.AllBuffered);
        }
    }
	
    [PunRPC]
    public void RPC_SpawnNet()
    {
        net = PhotonNetwork.InstantiateSceneObject("NetObject", netSpawn.position, netSpawn.rotation);
    }



    public void CallMoveNetUp()
    {
        //if (PhotonNetwork.IsMasterClient)
        {
            PV.RPC("RPC_MoveNetUp", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    public void RPC_MoveNetUp()
    {
        net.transform.position = new Vector3(net.transform.position.x, 67, net.transform.position.z);
    }


    public void CallMoveNetDown()
    {
       // if (PhotonNetwork.IsMasterClient)
        {
            PV.RPC("RPC_MoveNetDown", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    public void RPC_MoveNetDown()
    {
        net.transform.position = new Vector3(net.transform.position.x, 45, net.transform.position.z);
    }


}
