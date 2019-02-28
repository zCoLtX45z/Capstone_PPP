﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class PhotonRoom : MonoBehaviourPunCallbacks, IPunObservable {

    [SerializeField]
    private int MaxTeamPlayers = 6;
    private int Team1Players = 0;
    private int Team2Players = 0;
    private int QueuePlayers = 0;

    [SerializeField]
    private Text RoomText;
    [SerializeField]
    private Text RoomType;
    [SerializeField]
    private RectTransform Team1Content;
    [SerializeField]
    private RectTransform Team2Content;
    [SerializeField]
    private RectTransform QueueContent;
    [SerializeField]
    private PlayerRoomUI RPU_Prefab;
    [SerializeField]
    private Button ReadyButton;
    [SerializeField]
    private Text ReadyButtonText;
    [SerializeField]
    private Text CountDownText;

    private List<PlayerRoomUI> PlayerRoomUIList = new List<PlayerRoomUI>();

    private List<Player> Team1PlayerList = new List<Player>();
    private List<Player> Team2PlayerList = new List<Player>();
    private List<Player> QueuePlayerList = new List<Player>();

    private Player[] RoomPlayerList;

    private bool ChangedTeam = false;
    private bool ChangedReady = false;
    private int TeamNum = -1;
    private int TeamNumOld = -1;
    private bool IsReady = false;
    private bool IsReadyOld = false;
    // Game starts to play
    private bool StartGame = false;
    private bool StartedGame = false;
    [SerializeField]
    private float StartTime = 3;
    private float StartTimer = 0;

    // Photon
    [SerializeField]
    private PhotonView PV;

    private void Awake()
    {
        if (PhotonNetwork.InRoom)
        {
            RoomText.text = (string)PhotonNetwork.CurrentRoom.CustomProperties["RoomNameKey"];
            RoomType.text = (string)PhotonNetwork.CurrentRoom.CustomProperties["RoomTypeKey"];

            RoomPlayerList = PhotonNetwork.PlayerList;
            for (int i = 0; i < RoomPlayerList.Length; i++)
            {
                Player P = RoomPlayerList[i];

                // Set new custom properties for each player
                ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
                hash.Add("Team", -1);
                hash.Add("ReadyToPlay", false);
                hash.Add("RoomIndentifier", PhotonNetwork.CurrentRoom.Name);
                P.SetCustomProperties(hash);
            }
        }

        if (ReadyButton.enabled)
        {
            ReadyButton.enabled = false;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // If new Data, send
            if (ChangedTeam || ChangedReady)
            {
                ChangedTeam = false;
                ChangedReady = false;
                stream.SendNext(TeamNum);
                stream.SendNext(IsReady);
                stream.SendNext(PhotonNetwork.LocalPlayer.ActorNumber);
            }
            
        }
        else
        {
            // When packets arrive, change
            int ChangedNum = (int)stream.ReceiveNext();
            bool ChangedReady = (bool)stream.ReceiveNext();
            int ChangedActor = (int)stream.ReceiveNext();

            Player ChangedPlayer = null;
            foreach(Player P in RoomPlayerList)
            {
                if (P.ActorNumber == ChangedActor)
                {
                    ChangedPlayer = P;
                    break;
                }
            }

            if (ChangedPlayer != null)
            {
                ExitGames.Client.Photon.Hashtable hash = ChangedPlayer.CustomProperties;
                hash.Remove("Team");
                hash.Remove("ReadyToPlay");
                hash.Add("Team", ChangedNum);
                hash.Add("ReadyToPlay", ChangedReady);
                ChangedPlayer.SetCustomProperties(hash);
                print("Changed Player " + ChangedActor);
            }
        }
    }

    void Update()
    {
        CheckLocalPlayer();
        CheckPlayerTeam();
        UpdateLists();
        CheckStartGame();
    }

    [PunRPC]
    private void RPC_UpdateStartGameTimer(string Countdown)
    {
        CountDownText.text = Countdown;
    }

    private void CheckStartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (CheckIfReady())
            {
                if (!ReadyButton.enabled)
                {
                    ReadyButton.enabled = true;
                    ReadyButtonText.text = "Play";
                }
                if (StartGame)
                {
                    if (StartTimer < StartTime)
                    {
                        string tempText = Mathf.CeilToInt(3 - StartTimer) + "";
                        StartTimer += Time.deltaTime;
                        PV.RPC("RPC_UpdateStartGameTimer", RpcTarget.All, tempText);
                    }
                    else
                    {
                        if (!StartedGame)
                        {
                            StartedGame = true;
                            // Start The Game
                            PV.RPC("RPC_UpdateStartGameTimer", RpcTarget.All, "Start");

                            // Update Room Started Bool
                            PV.RPC("RPC_SetRoomProperty", RpcTarget.All, "StartedGame", true);


                            // Load scene and spawn player in
                            if ((string)PhotonNetwork.CurrentRoom.CustomProperties["RoomTypeKey"] == "Custom")
                            {
                                LoadArena(5);
                            }
                            else if ((string)PhotonNetwork.CurrentRoom.CustomProperties["RoomTypeKey"] == "TrainingRoom")
                            {
                                LoadArena(3);
                            }
                        }
                    }
                }
                else
                {
                    StartTimer = 0;
                    PV.RPC("RPC_UpdateStartGameTimer", RpcTarget.All, "");
                }
            }
            else
            {
                if (ReadyButton.enabled)
                {
                    ReadyButton.enabled = false;
                    ReadyButtonText.text = "Pause";
                }
            }
        }
    }

    [PunRPC]
    private void RPC_SetRoomProperty(string key, object value)
    {
        PhotonNetwork.CurrentRoom.CustomProperties[key] = value;
    }

    void LoadArena(int scene)
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
            Debug.Log("Scene " + scene + " Attempted to be Loaded");
        }
        else
        {
            Debug.Log("Scene " + scene + " Loaded");
            PhotonNetwork.LoadLevel(scene);
        }
    }

    public void ToggleStartGame()
    {
        StartGame = !StartGame;
    }

    private bool CheckIfReady()
    {
        bool IsReady = true;
        foreach (Player P in RoomPlayerList)
        {
            if (!(bool)P.CustomProperties["ReadyToPlay"])
            {
                IsReady = false;
                break;
            }
        }
        return IsReady;
    }

    private void CheckPlayerTeam()
    {
        foreach (Player P in RoomPlayerList)
        {
            object value;
            if (P.CustomProperties.TryGetValue("Team", out value))
            {
                if ((int)value == -1)
                {
                    QueuePlayerList.Add(P);
                    if (Team1PlayerList.Contains(P))
                    {
                        Team1PlayerList.Remove(P);
                    }
                    if (Team2PlayerList.Contains(P))
                    {
                        Team2PlayerList.Remove(P);
                    }
                }
                else if ((int)value == 1)
                {
                    Team1PlayerList.Add(P);
                    if (QueuePlayerList.Contains(P))
                    {
                        QueuePlayerList.Remove(P);
                    }
                    if (Team2PlayerList.Contains(P))
                    {
                        Team2PlayerList.Remove(P);
                    }
                }
                else if ((int)value == 2)
                {
                    Team2PlayerList.Add(P);
                    if (QueuePlayerList.Contains(P))
                    {
                        QueuePlayerList.Remove(P);
                    }
                    if (Team1PlayerList.Contains(P))
                    {
                        Team1PlayerList.Remove(P);
                    }
                }
            }
        }
    }

    private void UpdateLists()
    {
        List<PlayerRoomUI> NewPlayerRoomUIList = new List<PlayerRoomUI>();
        for(int i = 1; i <= RoomPlayerList.Length; i++)
        {
            if (PlayerRoomUIList.Count < i)
            {
                PlayerRoomUI temp = Instantiate(RPU_Prefab);
                int Team = (int)RoomPlayerList[i - 1].CustomProperties["Team"];
                RectTransform rt = temp.GetComponent<RectTransform>();
                temp.PlayerIdentifier ="Player " + RoomPlayerList[i - 1].ActorNumber;
                temp.SetName();
                if (Team == -1)
                {
                    rt.SetParent(QueueContent, false);
                }
                else if (Team == 1)
                {
                    rt.SetParent(Team1Content, false);
                }
                else if (Team == 2)
                {
                    rt.SetParent(Team2Content, false);
                }
                NewPlayerRoomUIList.Add(temp);
            }
            else
            {
                PlayerRoomUI temp = PlayerRoomUIList[i - 1];
                int Team = (int)RoomPlayerList[i - 1].CustomProperties["Team"];
                RectTransform rt = temp.GetComponent<RectTransform>();
                temp.PlayerIdentifier = "Player " + RoomPlayerList[i - 1].ActorNumber;
                temp.SetName();
                if (Team == -1)
                {
                    rt.SetParent(QueueContent, false);
                }
                else if (Team == 1)
                {
                    rt.SetParent(Team1Content, false);
                }
                else if (Team == 2)
                {
                    rt.SetParent(Team2Content, false);
                }
            }
        }
        if (NewPlayerRoomUIList.Count > 0)
            PlayerRoomUIList.AddRange(NewPlayerRoomUIList);

        List<PlayerRoomUI> RemovePlayerRoomUIList = new List<PlayerRoomUI>();
        RoomPlayerList = PhotonNetwork.PlayerList;
        if (PlayerRoomUIList.Count > RoomPlayerList.Length)
        {
            foreach (PlayerRoomUI RPU in PlayerRoomUIList)
            {
                int actorNum;
                if (int.TryParse(RPU.PlayerIdentifier.Split(' ')[1], out actorNum))
                {
                    bool RemovePlayer = true;
                    foreach (Player P in RoomPlayerList)
                    {
                        if (P.ActorNumber == actorNum)
                        {
                            RemovePlayer = false;
                            break;
                        }
                    }
                    if (RemovePlayer)
                    {
                        RemovePlayerRoomUIList.Add(RPU);
                    }
                }
            }
        }

        if (RemovePlayerRoomUIList.Count > 0)
        {
            foreach(PlayerRoomUI RPU in RemovePlayerRoomUIList)
            {
                if (PlayerRoomUIList.Contains(RPU))
                {
                    PlayerRoomUIList.Remove(RPU);
                    Destroy(RPU.gameObject, 0.1f);
                }
            }
        }
    }

    private void CheckLocalPlayer()
    {
        // Checks for New Data
        TeamNumOld = TeamNum;
        TeamNum = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];
        IsReadyOld = IsReady;
        IsReady = (bool)PhotonNetwork.LocalPlayer.CustomProperties["ReadyToPlay"];
        if (TeamNum != TeamNumOld)
        {
            ChangedTeam = true;
            PV.TransferOwnership(PhotonNetwork.LocalPlayer.ActorNumber);
        }
        else if (IsReady != IsReadyOld)
        {
            ChangedReady = true;
            PV.TransferOwnership(PhotonNetwork.LocalPlayer.ActorNumber);
        }
    }

    public override void OnJoinedRoom()
    {
        RoomText.text = (string)PhotonNetwork.CurrentRoom.CustomProperties["RoomNameKey"];
        RoomType.text = (string)PhotonNetwork.CurrentRoom.CustomProperties["RoomTypeKey"];
        base.OnJoinedRoom();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        // Set new custom properties for each new player
        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        hash.Add("Team", -1);
        hash.Add("ReadyToPlay", false);
        hash.Add("RoomIndentifier", PhotonNetwork.CurrentRoom.Name);
        newPlayer.SetCustomProperties(hash);
        base.OnPlayerEnteredRoom(newPlayer);

        // Update PlayerList
        RoomPlayerList = PhotonNetwork.PlayerList;
    }

    public void ChangeToTeam1()
    {
        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        hash = PhotonNetwork.LocalPlayer.CustomProperties;
        int Team = (int)hash["Team"];
        if (Team != 1)
        {
            hash.Remove("Team");
            hash.Add("Team", 1);

            PhotonNetwork.LocalPlayer.CustomProperties = hash;
        }
        bool IsReady = (bool)PhotonNetwork.LocalPlayer.CustomProperties["ReadyToPlay"];
        if (!IsReady)
        {
            hash.Remove("ReadyToPlay");
            hash.Add("ReadyToPlay", true);

            PhotonNetwork.LocalPlayer.CustomProperties = hash;
        }
    }

    public void ChangeToTeam2()
    {
        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        hash = PhotonNetwork.LocalPlayer.CustomProperties;
        int Team = (int)hash["Team"];
        if (Team != 2)
        {
            hash.Remove("Team");
            hash.Add("Team", 2);

            PhotonNetwork.LocalPlayer.CustomProperties = hash;
        }
        bool IsReady = (bool)PhotonNetwork.LocalPlayer.CustomProperties["ReadyToPlay"];
        if (!IsReady)
        {
            hash.Remove("ReadyToPlay");
            hash.Add("ReadyToPlay", true);

            PhotonNetwork.LocalPlayer.CustomProperties = hash;
        }
    }

    public void ChangeToQueue()
    {
        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        hash = PhotonNetwork.LocalPlayer.CustomProperties;
        int Team = (int)hash["Team"];
        if (Team != -1)
        {
            hash.Remove("Team");
            hash.Add("Team", -1);

            PhotonNetwork.LocalPlayer.CustomProperties = hash;
        }
        bool IsReady = (bool)PhotonNetwork.LocalPlayer.CustomProperties["ReadyToPlay"];
        if (IsReady)
        {
            hash.Remove("ReadyToPlay");
            hash.Add("ReadyToPlay", false);

            PhotonNetwork.LocalPlayer.CustomProperties = hash;
        }
    }
}