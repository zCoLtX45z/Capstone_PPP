using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class InGameMenu : MonoBehaviour {

    [SerializeField]
    private PhotonView PV;

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    private void OnApplicationQuit()
    {
        PhotonNetwork.LeaveRoom();
    }
}
