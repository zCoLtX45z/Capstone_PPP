using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[RequireComponent(typeof(NetworkIdentity))]
public class NetPlayer : NetworkBehaviour {

    // Player Components
    [SerializeField]
    private Chat ChatSystem;

    // Spawninng Player Object
    [SerializeField]
    private GameObject PlayerObject;

    // Player Team
    private int TeamNum = 0;

    // Player and Other Player 
    [HideInInspector]
    public NetPlayer LocalPlayer;
    [HideInInspector]
    public NetPlayer[] PlayerList = null;

    // Team Select Canvas
    [SerializeField]
    private Canvas StartingCanvas;

    [HideInInspector]
    public bool ConfirmTeam = false;

    // Player Indetifiers
    //[HideInInspector]
    [SyncVar]
    public string CodeNumbers = "";
    [SyncVar]
    public string PlayerCode = "";

    // Player Child Components
    private hoverBoardScript HB;

    // Use this for initialization
    void Start () {
        int randNum = Random.Range(1000, 99999);
        CodeNumbers = "#" + randNum;
        PlayerCode = gameObject.name + CodeNumbers;
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
            if (PlayerList == null)
                SetPlayerList();

            if (isServer)
            {
                foreach (NetPlayer p in PlayerList)
                {
                    p.CmdChangeName(p.gameObject.name, p.CodeNumbers);
                }
            }

            if (isLocalPlayer)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

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
        else
        {
            if (isLocalPlayer)
            {
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.RightShift))
                {
                    ChatSystem.ToggleChat();
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
        HB = spawningObject.GetComponent<hoverBoardScript>();
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

    [Command]
    private void CmdChangeName(string name, string code)
    {
        RpcChangeName(name, code);
    }

    [ClientRpc]
    private void RpcChangeName(string name, string code)
    {
        gameObject.name = name;
        CodeNumbers = code;
        // this.GetComponentInChildren<TextMesh>().text = name;
    }


}
