using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[RequireComponent(typeof(NetworkIdentity))]
public class NetPlayer : NetworkBehaviour {

    [SerializeField]
    private GameObject PlayerObject;

    private int TeamNum = 0;

    [HideInInspector]
    public NetPlayer LocalPlayer;
    [HideInInspector]
    public NetPlayer[] PlayerList = null;

    [SerializeField]
    private Canvas StartingCanvas;

    [HideInInspector]
    public bool ConfirmTeam = false;

    //[HideInInspector]
    public string PlayerCode = "";

    // Use this for initialization
    void Start () {
        int randNum = Random.Range(1000, 99999);
        PlayerCode = gameObject.name + "#" + randNum;
        SetPlayerList();
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
        GameObject GO = Instantiate(PlayerObject, transform);
        GO.name = PlayerCode;
        NetworkServer.SpawnWithClientAuthority(GO, connectionToClient);
        CmdParentChild(GO);
        RpcSpawnPlayer(GO);
    }

    [ClientRpc]
    private void RpcSpawnPlayer(GameObject spawningObject)
    {
        PlayerColor PC = spawningObject.GetComponent<PlayerColor>();
        PC.CmdSetUpPlayer();
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

    [Command]
    private void CmdParentChild(GameObject child)
    {
        RpcParentChild(child);
    }

    [ClientRpc]
    private void RpcParentChild(GameObject child)
    {
        child.transform.parent = this.transform;
    }

    public void ConfirmTeamPlacement()
    {
        ConfirmTeam = true;
    }

    [Command]
    public void CmdChangeTeam(int i)
    {
        RpcChangeTeam(i);
    }

    [ClientRpc]
    public void RpcChangeTeam(int i)
    {
        TeamNum = i;
    }

    public int GetTeamNum()
    {
        return TeamNum;
    }
}
