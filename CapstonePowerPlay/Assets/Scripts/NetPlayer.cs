using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[RequireComponent(typeof(NetworkIdentity))]
public class NetPlayer : NetworkBehaviour {

    [SerializeField]
    private GameObject PlayerObject;

    [SyncVar]
    public int TeamNum = 0;

    [HideInInspector]
    public NetPlayer LocalPlayer;
    [HideInInspector]
    public NetPlayer[] PlayerList = null;

    [SerializeField]
    private Canvas StartingCanvas;

    [HideInInspector]
    public bool ConfirmTeam = false;

    // Use this for initialization
    void Start () {
        if (isLocalPlayer)
        {
            StartingCanvas.gameObject.SetActive(true);
        }
	}

    // Update is called once per frame
    void Update()
    {
        if (StartingCanvas.gameObject.activeSelf)
        {
            if (isLocalPlayer)
            {
                if (PlayerList == null)
                    SetPlayerList();

                if (ConfirmTeam)
                {
                    if (TeamNum == 0)
                    {
                        ConfirmTeam = false;
                    }
                    else
                    {
                        CmdSpawnPlayer();
                        StartingCanvas.gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    [Command]
    public void CmdSpawnPlayer()
    {
        SetPlayerList();
        if (TeamNum != 0)
        {
            GameObject GO = Instantiate(PlayerObject);
            PlayerColor PC = GO.GetComponent<PlayerColor>();
            PC.SetUpPlayer(TeamNum, LocalPlayer);
            GO.gameObject.name = name;
            NetworkServer.Spawn(GO);
        }
    }

    public void SetPlayerList()
    {
        PlayerList = FindObjectsOfType<NetPlayer>();
        if (isLocalPlayer)
        {
            LocalPlayer = this;
            foreach (NetPlayer p in PlayerList)
            {
                p.LocalPlayer = this;
            }
        }
    }

    public void ConfirmTeamPlacement()
    {
        ConfirmTeam = true;
    }
}
