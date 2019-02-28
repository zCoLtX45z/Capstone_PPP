using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhotonRoom2 : MonoBehaviourPunCallbacks, IInRoomCallbacks
{

    public static PhotonRoom2 room;
    private PhotonView PV;
    public bool IsGameLoaded;
    public int CurrentScene;

    //player info
    private Player[] photonPlayers;
    public int PlayersInRoom;
    public int MyNumberInRoom;

    public int playerInGame;

    //Delayed Start
    private bool readyToCount;
    private bool readyToStart;
    public float startingTime;
    private float lessThanMaxPlayers;
    private float atMaxPlayer;
    private float timeToStart;






    private void Awake()
    {
        if (PhotonRoom2.room == null)
        {
            PhotonRoom2.room = this;
        }
        else
        {
            if (PhotonRoom2.room != this)
            {
                Destroy(PhotonRoom2.room.gameObject);
                PhotonRoom2.room = this;
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded += OnSceneFinishedLoading;

    }

    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
        SceneManager.sceneLoaded -= OnSceneFinishedLoading;
    }
    // Use this for initialization
    void Start()
    {
        PV = GetComponent<PhotonView>();
        readyToCount = false;
        readyToStart = false;
        lessThanMaxPlayers = startingTime;
        atMaxPlayer = 6;
        timeToStart = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (MultiplayerSettings.MPS.DelayStart)
        {
            if (PlayersInRoom == 1)
            {
                RestartTimer();
            }
            if (!IsGameLoaded)
            {
                if (readyToStart)
                {
                    atMaxPlayer -= Time.deltaTime;
                    lessThanMaxPlayers = atMaxPlayer;
                    timeToStart = atMaxPlayer;
                }
                else if (readyToCount)
                {
                    bool AllPlayersReady = true;
                    foreach (Player P in PhotonNetwork.PlayerList)
                    {
                        if (!(bool)P.CustomProperties["ReadyInRoom"])
                        {
                            AllPlayersReady = false;
                            break;
                        }
                    }
                    if (AllPlayersReady)
                    {
                        lessThanMaxPlayers -= Time.deltaTime;
                        timeToStart = lessThanMaxPlayers;
                    }
                    else
                    {
                        timeToStart = 0;
                    }
                }
                Debug.Log("time to start :" + timeToStart);
                if (timeToStart <= 0)
                {
                    StartGame();
                }
            }
        }
    }
    [PunRPC]
    public void RPC_ChangeTeam(Player P)
    {

    }

    public override void OnJoinedRoom()
    {
        Debug.Log("joined a room");
        base.OnJoinedRoom();
        photonPlayers = PhotonNetwork.PlayerList;
        PlayersInRoom = photonPlayers.Length;
        MyNumberInRoom = PlayersInRoom;
        PhotonNetwork.NickName = MyNumberInRoom.ToString();
        if (MultiplayerSettings.MPS.DelayStart)
        {
            Debug.Log("players in room out of max players possible(" + PlayersInRoom + ":" + MultiplayerSettings.MPS.MaxPlayers + ")");
            if (PlayersInRoom > 1)
            {
                readyToCount = true;
            }
            if (PlayersInRoom == MultiplayerSettings.MPS.MaxPlayers)
            {
                bool AllPlayersReady = true;
                foreach(Player P in PhotonNetwork.PlayerList)
                {
                    if (!(bool)P.CustomProperties["ReadyInRoom"])
                    {
                        AllPlayersReady = false;
                        break;
                    }
                }
                readyToStart = AllPlayersReady;
                if (!PhotonNetwork.IsMasterClient)
                    return;
                PhotonNetwork.CurrentRoom.IsOpen = false;


            }
        }
        else
        {
            //StartGame();
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log("A new player has joined the room");

        // Set Player Properties
        ExitGames.Client.Photon.Hashtable CustomHash = new ExitGames.Client.Photon.Hashtable();
        CustomHash.Add("Team", -1);
        CustomHash.Add("ReadyInRoom", true);
        CustomHash.Add("RoomID", PhotonNetwork.CurrentRoom.Name);
        newPlayer.SetCustomProperties(CustomHash);


        photonPlayers = PhotonNetwork.PlayerList;
        PlayersInRoom++;
        if (MultiplayerSettings.MPS.DelayStart)
        {
            if (PlayersInRoom > 1)
            {
                readyToCount = true;
            }
            if (PlayersInRoom == MultiplayerSettings.MPS.MaxPlayers)
            {
                readyToStart = true;
                if (!PhotonNetwork.IsMasterClient)
                    return;
                PhotonNetwork.CurrentRoom.IsOpen = false;
            }
        }
    }

    void StartGame()
    {
        IsGameLoaded = true;
        if (!PhotonNetwork.IsMasterClient)
            return;
        if (MultiplayerSettings.MPS.DelayStart)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
        PhotonNetwork.LoadLevel(MultiplayerSettings.MPS.MultiplayerScene);
    }

    void RestartTimer()
    {
        lessThanMaxPlayers = startingTime;
        timeToStart = startingTime;
        atMaxPlayer = 6;
        readyToCount = false;
        readyToStart = false;
    }

    void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        CurrentScene = scene.buildIndex;
        if (CurrentScene == MultiplayerSettings.MPS.MultiplayerScene)
        {

            IsGameLoaded = true;
            if (MultiplayerSettings.MPS.DelayStart)
            {
                PV.RPC("RPC_LoadedGameScene", RpcTarget.MasterClient);

            }
            else
            {
                RPC_CreatePlayer();
            }
        }
    }
    [PunRPC]
    private void RPC_LoadedGameScene()
    {
        playerInGame++;
        if (playerInGame == PhotonNetwork.PlayerList.Length)
        {
            PV.RPC("RPC_CreatePlayer", RpcTarget.All);
        }
    }
    [PunRPC]
    void RPC_CreatePlayer()
    {
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonNetworkPlayer"), transform.position, Quaternion.identity, 0);
    }

}
