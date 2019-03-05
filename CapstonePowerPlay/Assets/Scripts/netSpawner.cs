using Photon.Pun;
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
	// Use this for initialization
	void Start ()
    {
        PV = GetComponent<PhotonView>();
       if (PhotonNetwork.IsMasterClient)
        {
            PV.RPC("RPC_SpawnNet", RpcTarget.AllBuffered);
        }
            

    }
	
    [PunRPC]
    public void RPC_SpawnNet()
    {
        
        PhotonNetwork.InstantiateSceneObject("NetObject", netSpawn.position, netSpawn.rotation);
        
    }
}
