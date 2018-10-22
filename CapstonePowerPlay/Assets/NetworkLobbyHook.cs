using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Prototype.NetworkLobby;


    public class NetworkLobbyHook : LobbyHook
    {
        public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
        {
            LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();
            SetupLocalPlayer localplayer = gamePlayer.GetComponent<SetupLocalPlayer>();

        localplayer.pname = lobby.name;
        localplayer.playerColor = lobby.playerColor;
        
        }

    }


