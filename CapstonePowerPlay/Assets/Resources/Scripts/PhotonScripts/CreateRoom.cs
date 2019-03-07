using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class CreateRoom : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private InputField RoomInputField;
    private string RoomName;
    private string RoomIdentifier;

    [SerializeField]
    private RoomLayoutGroup RLG;

    public void CreatePhotonRoom()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            RoomName = RoomInputField.text;
            int RoomNum = PhotonNetwork.CountOfRooms + 1;
            int RndInt = Random.Range(1000, 99999);
            RoomIdentifier = PlayerNetwork.Instance.name + "'s Room#" + RoomNum;

            RoomOptions RO = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 12 };
            RO.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable();
            RO.CustomRoomProperties.Add("RoomNameKey", RoomName);
            RO.CustomRoomProperties.Add("RoomTypeKey", "Custom");
            RO.CustomRoomProperties.Add("StartedGame", false);
            RO.CustomRoomProperties.Add("RoomNumber", RoomNum);
            RO.CustomRoomProperties.Add("RoomIdentifier", RoomName + RndInt);

            RO.CustomRoomPropertiesForLobby = new string[3];
            RO.CustomRoomPropertiesForLobby[0] = "RoomNameKey";
            RO.CustomRoomPropertiesForLobby[1] = "RoomTypeKey";
            RO.CustomRoomPropertiesForLobby[2] = "RoomNum";

            if (RoomName == "")
            {
                RoomName = PlayerNetwork.Instance.name + "'s Room";
            }
            if (PhotonNetwork.CreateRoom(RoomIdentifier, RO, TypedLobby.Default))
            {
                print("Create Room Request Sent.");
            }
            else
            {
                print("Create Room Request Failed to Send.");
            }
        }
    }

    public void CreatePhotonTrainingRoom()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            RoomName = RoomInputField.text;
            int RoomNum = PhotonNetwork.CountOfRooms + 1;
            int RndInt = Random.Range(1000, 99999);
            RoomIdentifier = PlayerNetwork.Instance.name + "'s Training Room#" + RoomNum;

            RoomOptions RO = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 12 };
            RO.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable();
            RO.CustomRoomProperties.Add("RoomNameKey", RoomName);
            RO.CustomRoomProperties.Add("RoomTypeKey", "TrainingRoom");
            RO.CustomRoomProperties.Add("StartedGame", false);
            RO.CustomRoomProperties.Add("RoomNumber", RoomNum);
            RO.CustomRoomProperties.Add("RoomIdentifier", RoomName + RndInt);

            RO.CustomRoomPropertiesForLobby = new string[3];
            RO.CustomRoomPropertiesForLobby[0] = "RoomNameKey";
            RO.CustomRoomPropertiesForLobby[1] = "RoomTypeKey";
            RO.CustomRoomPropertiesForLobby[2] = "RoomNum";
            if (RoomName == "")
            {
                RoomName = PlayerNetwork.Instance.name + "'s Training Room";
            }
            if (PhotonNetwork.CreateRoom(RoomIdentifier, RO, TypedLobby.Default))
            {
                print("Create Room Request Sent.");
            }
            else
            {
                print("Create Room Request Failed to Send.");
            }
        }
    }

    private void OnPhotonCreateRoomFailed(object[] CodeAndMessege)
    {
        print("Room Failed Error: " + CodeAndMessege[1]);
    }

    public override void OnCreatedRoom()
    {
        print("Room Created.");
        PhotonNetwork.JoinRoom(RoomIdentifier);
        //if (PhotonNetwork.GetCustomRoomList(TypedLobby.Default, ""))
        //{
        //    print("Finding Room Created.");
        //}
    }

    //public override void OnRoomListUpdate(List<RoomInfo> roomList)
    //{
    //    foreach (RoomInfo RI in roomList)
    //    {
    //        if (RI.Name == RoomIdentifier)
    //        {
    //            print("Found Room Created.");
    //            RoomInfo CreatedRoom = RI;
    //        }
    //    }
    //}
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        print("Failed to Join Room: " + message + ", " + returnCode);
        //base.OnJoinRoomFailed(returnCode, message);
    }

    public override void OnJoinedRoom()
    {
        print("Room Joined.");
        if (PhotonNetwork.IsMasterClient)
        {
            Room Current = PhotonNetwork.CurrentRoom;
            Current.MaxPlayers = 12;
            RoomIdentifier = Current.Name;
        }
        
        
        base.OnJoinedRoom();
    }
}
