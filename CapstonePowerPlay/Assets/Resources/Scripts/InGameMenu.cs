using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class InGameMenu : MonoBehaviourPunCallbacks {

    [SerializeField]
    private PhotonView PV;

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(0);
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    private void OnApplicationQuit()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LeaveLobby();
    }
}
