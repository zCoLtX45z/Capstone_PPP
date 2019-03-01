using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class NetPlayer : MonoBehaviour {

    // Player Components
    [SerializeField]
    private Chat ChatSystem;
    [SerializeField]
    private PhotonView PV;
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
   
    public string CodeNumbers = "";
    
    public string PlayerCode = "";

    // Player Child Components
    private hoverBoardScript HBS;
    private BallHandling BH;

    // Variables
    private bool SkipTeamSelect = false;
    
    // Use this for initialization
    void Start ()
    {
        if (!PV)
            PV = GetComponent<PhotonView>();
        string randNum = PhotonNetwork.LocalPlayer.NickName.Split('#')[1];
        CodeNumbers = "#" + randNum;
        PlayerCode = PhotonNetwork.LocalPlayer.NickName + CodeNumbers;
        SetPlayerList();
        if (PV.IsMine)
        {
            Debug.Log(PhotonNetwork.LocalPlayer.NickName);
            gameObject.name = PhotonNetwork.LocalPlayer.NickName.Split('#')[0];
            if ((int)PhotonNetwork.LocalPlayer.CustomProperties["Team"] != -1)
            {
                UpdateTeamNum(TeamNum);
                SkipTeamSelect = true;
            }
            else
                StartingCanvas.gameObject.SetActive(true);

        }
        else
        {
            StartingCanvas.gameObject.SetActive(false);
        }
	}

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine)
        {
            if (SkipTeamSelect)
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    foreach (NetPlayer p in PlayerList)
                    {
                        //p.CmdChangeName(p.gameObject.name, p.CodeNumbers);
                        p.PV.RPC("RPC_ChangeName", RpcTarget.All, p.gameObject.name, p.CodeNumbers);
                    }
                }
                // Set player Team
                TeamNum = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];

                // Spawn player character
                Debug.Log("Spawning player Skip");
                SkipTeamSelect = false;
                StartingCanvas.gameObject.SetActive(false);
                SpawnPlayer();
            }
            //Debug.Log(" IT IS MINE");
            else if (StartingCanvas.gameObject.activeSelf)
            {
                if (PlayerList == null)
                    SetPlayerList();

                if (PhotonNetwork.IsMasterClient)
                {
                    foreach (NetPlayer p in PlayerList)
                    {
                        //p.CmdChangeName(p.gameObject.name, p.CodeNumbers);
                        p.PV.RPC("RPC_ChangeName", RpcTarget.All, p.gameObject.name, p.CodeNumbers);
                    }
                }

                if (PV.IsMine)
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
                            UpdateTeamNum(TeamNum);
                            StartingCanvas.gameObject.SetActive(false);
                            //CmdSpawnPlayer();
                            Debug.Log("Spawning player Starting Canvas");
                            //PV.RPC("RPC_SpawnPlayer", RpcTarget.All);
                            SpawnPlayer();
                        }
                    }
                }
            }
            else
            {
                if (PV.IsMine)
                {
                    if (Input.GetKeyDown(KeyCode.T) || Input.GetKeyDown(KeyCode.RightShift))
                    {
                        if (!ChatSystem.GetEnabled())
                            ChatSystem.ToggleChat();
                    }
                    if (ChatSystem.GetEnabled() && HBS != null)
                    {
                        HBS.BoardHasControl = false;
                    }
                    else if (!ChatSystem.GetEnabled() && HBS != null)
                    {
                        HBS.BoardHasControl = true;
                    }

                    // If the curser should be active or not
                    if (ChatSystem.GetEnabled())
                    {
                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.None;
                    }
                    else
                    {
                        Cursor.visible = false;
                        Cursor.lockState = CursorLockMode.Locked;
                    }
                }
            }
        }
    }
    [PunRPC]
    public void RPC_UpdateTeamNum(int i)
    {
        TeamNum = i;
    }

    public void UpdateTeamNum(int i)
    {
        PV.RPC("RPC_UpdateTeamNum", RpcTarget.AllBuffered, i);
    }
    //[PunRPC]
    public void SpawnPlayer()
    {
        SetPlayerList();
        int spawnPicker = Random.Range(0, GameSetup.GS.playerSpawns.Length);
        GameObject GO = PhotonNetwork.Instantiate("PhotonPrefabs/PhotonPlayer", GameSetup.GS.playerSpawns[spawnPicker].transform.position, GameSetup.GS.playerSpawns[spawnPicker].transform.rotation, 0, null);
        GO.transform.parent = transform;
        GO.name = PlayerCode;
        //NetworkServer.SpawnWithClientAuthority(GO, connectionToClient);
        if (GO != null)
        {
            //ParentChild(GO);
            SetPlayer(GO);
            ParentChild(GO);
        }
    }

    
    private void SetPlayer(GameObject spawningObject)
    {
        print("SetPlayer Called: " + PhotonNetwork.LocalPlayer.NickName);
        PlayerColor PC = null;

        PC = spawningObject.GetComponent<PlayerColor>();
        if (PC == null)
        {
            print("PC not found: ");
        }
        PC.SetUpPlayer1(spawningObject);
        HBS = spawningObject.GetComponent<hoverBoardScript>();
        BH = spawningObject.GetComponent<BallHandling>();
    }

    public void SetPlayerList()
    {
        PlayerList = FindObjectsOfType<NetPlayer>();
        if (PV.IsMine)
        {
            LocalPlayer = this;
            foreach (NetPlayer p in PlayerList)
            {
                p.LocalPlayer = this;
            }
        }
    }

    [PunRPC]
    private void RPC_ParentChild(GameObject child)
    {
        ParentChild(child);
    }

    
    private void ParentChild(GameObject child)
    {
        child.transform.SetParent(this.transform);
    }

    public void ConfirmTeamPlacement()
    {
        ConfirmTeam = true;
    }

    public void ChangeTeam(int i)
    {
        PV.RPC("RPC_ChangeTeam", RpcTarget.All, i);
    }

    [PunRPC]
    public void RPC_ChangeTeam(int i)
    {
        TeamNum = i;
    }

    
   

    public int GetTeamNum()
    {
        return TeamNum;
    }

    [PunRPC]
    private void RPC_ChangeName(string name, string code)
    {
        ChangeName(name, code);
    }

    
    private void ChangeName(string name, string code)
    {
        gameObject.name = name;
        CodeNumbers = code;
        // this.GetComponentInChildren<TextMesh>().text = name;
    }


}
