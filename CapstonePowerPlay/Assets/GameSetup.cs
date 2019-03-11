using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameSetup : MonoBehaviour {

    public static GameSetup GS;

    public GameObject[] playerSpawns;
    public GameObject[] BlueSpawns;
    public GameObject[] RedSpawns;
    // Use this for initialization
    void Start()
    {
        if (playerSpawns.Length == 0)
        {
            BlueSpawns = GameObject.FindGameObjectsWithTag("BlueSpawn");
            RedSpawns = GameObject.FindGameObjectsWithTag("RedSpawn");
            playerSpawns = GameObject.FindGameObjectsWithTag("playerSpawn");
        }
    }


    private void OnEnable()
    {
        if (GameSetup.GS == null)
        {
            GameSetup.GS = this;
        }
    }

    public Transform GrabSpawnPoint(int viewID)
    {
        Transform ReturnTransform = null;
        // Figure out who is asking
        NetPlayer ThisPlayer = PhotonView.Find(viewID).GetComponent<NetPlayer>();

        // Find All Players
        NetPlayer[] Players = FindObjectsOfType<NetPlayer>();

        // Find players on players team
        List<NetPlayer> TeamPlayers = new List<NetPlayer>();
        foreach(NetPlayer NP in Players)
        {
            if (NP.GetTeamNum() == ThisPlayer.GetTeamNum())
            {
                TeamPlayers.Add(NP);
            }
        }

        // find the players by view id
        List<NetPlayer> TeamPlayersByViewID = new List<NetPlayer>();
        for (int I = 0; I < TeamPlayers.Count; I++)
        {
            NetPlayer temp = null;
            // Find Next Lower Player
            foreach(NetPlayer NP in TeamPlayers)
            {
                // If the temp is null
                if (temp == null)
                {
                    // Add current player if it is not contained
                    if (!TeamPlayersByViewID.Contains(NP))
                        temp = NP;
                }
                // If the NP is lower then the temp
                if (temp != null && temp.GetTeamNum() > NP.GetTeamNum() && !TeamPlayersByViewID.Contains(NP))
                {
                    temp = NP;
                }
            }
            // Add player to list
            if (!TeamPlayersByViewID.Contains(temp))
            {
                TeamPlayersByViewID.Add(temp);
            }
        }
        // All the players should be sorted
        int index = TeamPlayersByViewID.IndexOf(ThisPlayer);

        // Set the return transform
        if (ThisPlayer.GetTeamNum() == 1)
            ReturnTransform = BlueSpawns[index].transform;
        if (ThisPlayer.GetTeamNum() == 2)
            ReturnTransform = RedSpawns[index].transform;
        //else
        //{
        //    Debug.Log("No Spawn Selected");
        //}
        return ReturnTransform;
    }
}
