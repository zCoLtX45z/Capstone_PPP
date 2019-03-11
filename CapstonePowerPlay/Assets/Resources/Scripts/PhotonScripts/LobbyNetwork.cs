using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using System.IO;

public class LobbyNetwork : MonoBehaviourPunCallbacks, IInRoomCallbacks {

    [SerializeField]
    private PhotonView PV;
    private string RoomIdentifier = "None";

	// Use this for initialization
	void Start () {
        if (!PhotonNetwork.InLobby && !PhotonNetwork.InRoom)
        {
            
            PhotonNetwork.OfflineMode = false;
            PhotonNetwork.LocalPlayer.NickName = PlayerNetwork.Instance.PlayerName;
            PhotonNetwork.AutomaticallySyncScene = true;
            //PhotonNetwork.GameVersion = "1";

            print("Connecting to Server...");
            PhotonNetwork.ConnectUsingSettings();
        }
            SceneManager.sceneLoaded += OnSceneFinishedLoading;

    }

    public override void OnConnected()
    {
        print("Connected");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        print("Disconnected Cause: " + cause);
    }

    public override void OnConnectedToMaster()
    {
        print("Connected to Master.");

        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        print("Connected to Lobby.");
        print("Lobby: " + PhotonNetwork.CurrentLobby);
    }

    public override void OnLeftLobby()
    {
        print("Left Lobby.");
        base.OnLeftLobby();
    }

    public override void OnLeftRoom()
    {
        print("Left Room.");
        base.OnLeftRoom();
    }

    private void OnFailedToConnectToPhoton()
    {
        print("Connected to Master: Invalid AppID or Network Issues");
    }

    private void OnConnectionFail()
    {
        print("Connected to Master: Invalid Region or Maxed out CCU");
    }

    private void OnApplicationQuit()
    {
        print("Closing Game.");
        if (PhotonNetwork.InRoom)
            PhotonNetwork.LeaveRoom();
        if (PhotonNetwork.InLobby)
            PhotonNetwork.LeaveLobby();
    }

    public void SetRoom(RoomListing DesiredRoom)
    {
        RoomIdentifier = DesiredRoom.RoomListingInfo.RoomIdentifier;
    }

    public void JoinPhotonRoom()
    {
        if (RoomIdentifier != "None")
        {
            print("RoomID: RoomIdentifier Attempted.");
            PhotonNetwork.JoinRoom(RoomIdentifier);
        }
        else
        {
            print("No RoomID - Cannot Rejoin.");
        }
    }

    public void LeavePhotonRoom()
    {
        if (PhotonNetwork.InRoom)
            PhotonNetwork.LeaveRoom();
    }

    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        //print(scene.name + ": OnSceneFinhishedLoadingCalled");
        if (scene.name == "DustinScene" || scene.name == "Marcscene" || scene.name == "MarcsceneDup")
        {
            //print("RPC_CreatePlayer" + ": Attempted");
            //PhotonNetwork.Instantiate("PhotonPrefabs/PhotonNetworkPlayer", transform.position, Quaternion.identity, 0);
            if (PhotonNetwork.IsMasterClient)
                PV.RPC("RPC_CreatePlayer", RpcTarget.All);
        }
    }

    [PunRPC]
    private void RPC_CreatePlayer()
    {
        //print("RPC_CreatePlayer" + ": Called");
        PhotonNetwork.Instantiate("PhotonPrefabs/PhotonNetworkPlayer", transform.position, Quaternion.identity, 0);
    }
}
