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
   // [HideInInspector]
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
    public GameObject ChildPlayer;
    private PlayerColor PC;
    [HideInInspector]
    public bool ReadyToSetPlayer = false;
    private bool SetPlayerBool = false;
    private bool SetUpThePlayers = false;
    // Variables
    private bool SkipTeamSelect = false;

    // Use this for initialization
    void Start ()
    {
        if (!PV)
            PV = GetComponent<PhotonView>();
        if (PV.IsMine)
        {
            gameObject.name = PhotonNetwork.LocalPlayer.NickName;
            CodeNumbers = "#" + gameObject.name.Split('#')[1];
            PV.RPC("RPC_UpdateCode", RpcTarget.AllBuffered, CodeNumbers);
            PV.RPC("RPC_ChangeName", RpcTarget.All, gameObject.name, CodeNumbers);
        }
        PlayerCode = gameObject.name + CodeNumbers;
        if (PV.IsMine)
        {
            SetPlayerList();
            if ((int)PhotonNetwork.LocalPlayer.CustomProperties["Team"] != -1)
            {
                TeamNum = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];
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
                //p.CmdChangeName(p.gameObject.name, p.CodeNumbers);
                //PV.RPC("RPC_ChangeName", RpcTarget.All, gameObject.name, CodeNumbers);
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

                //if (PhotonNetwork.IsMasterClient)
                //{
                //    foreach (NetPlayer p in PlayerList)
                //    {
                //        p.CmdChangeName(p.gameObject.name, p.CodeNumbers);
                //        p.PV.RPC("RPC_ChangeName", RpcTarget.All, p.gameObject.name, p.CodeNumbers);
                //    }
                //}

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
            else if (!SetPlayerBool)
            {
                SetPlayerList();
                if (!SetUpThePlayers)
                {
                    SetUpThePlayers = true;
                    foreach (NetPlayer NP in PlayerList)
                    {
                        if (!NP.ReadyToSetPlayer)
                        {
                            SetUpThePlayers = false;
                            break;
                        }
                    }
                }
                if (SetUpThePlayers && !PC.PlayerLocalSet)
                {
                    // Set Local Player for everyone
                    PC.SetUpPlayer();
                    PlayerColor[] PlayerColorList = FindObjectsOfType<PlayerColor>();
                    bool FinalSetPlayers = true;
                    foreach(PlayerColor P in PlayerColorList)
                    {
                        P.LocalPlayer = this;
                        if (!P.PlayerLocalSet)
                        {
                            FinalSetPlayers = false;
                            break;
                        }
                    }
                    if (FinalSetPlayers)
                    {
                        PC.FinalPlayerSet();
                        SetPlayerBool = true;
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
    private void RPC_GetPlayerComponents()
    {
        BH = ChildPlayer.GetComponent<BallHandling>();
        HBS = ChildPlayer.GetComponent<hoverBoardScript>();
        PC = ChildPlayer.GetComponent<PlayerColor>();
        ChildPlayer.transform.SetParent(this.transform);
    }

    [PunRPC]
    public void RPC_UpdateCode(string code)
    {
        CodeNumbers = code;
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
        ChildPlayer = PhotonNetwork.Instantiate("PhotonPlayer", GameSetup.GS.playerSpawns[spawnPicker].transform.position, GameSetup.GS.playerSpawns[spawnPicker].transform.rotation);
        ChildPlayer.transform.SetParent(transform);
        ChildPlayer.name = PlayerCode;
        //NetworkServer.SpawnWithClientAuthority(GO, connectionToClient);
        if (ChildPlayer != null)
        {
            //ParentChild(GO);
            Debug.Log("GO != null");
            PV.RPC("RPC_ParentChild", RpcTarget.All, ChildPlayer.GetPhotonView().ViewID);
            ReadyToSetPlayer = true;
            PV.RPC("RPC_UpdateReady", RpcTarget.AllBuffered, ReadyToSetPlayer);
            // Set Components
            PV.RPC("RPC_GetPlayerComponents", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    private void RPC_UpdateReady(bool ready)
    {
        ReadyToSetPlayer = ready;
    }
    private void SetPlayer(GameObject spawningObject)
    {
        Debug.Log("setting up player");
        PlayerColor PC = spawningObject.GetComponent<PlayerColor>();
        PC.LocalPlayer = LocalPlayer;
        PC.SetUpPlayer1();
        HBS = spawningObject.GetComponent<hoverBoardScript>();
        BH = spawningObject.GetComponent<BallHandling>();
        Debug.Log("finished setting up player");
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
                if (p.PC != null)
                    p.PC.LocalPlayer = this;
            }
        }
    }

    [PunRPC]
    private void RPC_ParentChild(int child)
    {
        ParentChild(PhotonView.Find(child).gameObject);
    }


    private void ParentChild(GameObject child)
    {
        child.transform.SetParent(this.transform);
        ChildPlayer = child;
    }

    public void ConfirmTeamPlacement()
    {
        ConfirmTeam = true;
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
