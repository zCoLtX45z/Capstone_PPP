using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerColor : MonoBehaviourPun
{

    [SerializeField]
    private GameObject BluePlayer;
    [SerializeField]
    private GameObject RedPlayer;
    [SerializeField]
    private BallHandling BH;
    [SerializeField]
    private Transform RedHand;
    [SerializeField]
    private Transform BlueHand;
    [SerializeField]
    private GameObject RedFakeBall;
    [SerializeField]
    private GameObject BlueFakeBall;
    [SerializeField]
    private TextMesh TextName;
    [SerializeField]
    private PlayerNameTag PNT;

    public int TeamNum = 0;
    public NetPlayer LocalPlayer;
    public NetPlayer ParentPlayer;

    [SerializeField]
    private ComponentsToDisable CD;

    [SerializeField]
    private PlayerSoftlockPassSight pSLPS;

    [SerializeField]
    private ChangeTags CT;

    //photon variables
    [SerializeField]
    private PhotonView PV;
    private string Code = "None";

    // Setting up players
    [HideInInspector]
    public bool PlayerLocalSet = false;
    [HideInInspector]
    public bool SetPlayerTag = false;
    [HideInInspector]
    public bool SetLocalPlayerCalled = false;
    public GameObject ParentObject;
    public string DisplayName = "";

    // Pick if your team is blue and enemy is red OR is your team color is set with team number
    [SerializeField]
    private bool SetColorByTeamNum = true;
    
    private void Start()
    {
        DisplayName = (string)PhotonNetwork.LocalPlayer.CustomProperties["DisplayName"];
    }

    [PunRPC]
    private void RPC_UpdateLocalSet(bool ready)
    {
        PlayerLocalSet = ready;
    }

    //void Start()
    //{

    //}
    ///ATTEMPTING TO SYNCH DATA ACROSS SERVER
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            Debug.Log("Writing data");
            //stream.SendNext(TeamNum);
        }
        else
        {
            //TeamNum = (int)stream.ReceiveNext();

        }
    }


    public void SetUpPlayer1(NetPlayer parent)
    {
        PV = GetComponent<PhotonView>();
        Debug.Log("PV: S" + PV);
        Debug.Log("SetUpPlayer Called");
        SetUpPlayer(parent);
        //if (PV.IsMine)
        //{
        //    PV.RPC("RPC_UpdateTag", RpcTarget.AllBuffered);
        //}

    }

    //[PunRPC]
    //public void RPC_SetUpPlayer()
    //{
    //    //Debug.Log("parent player is " + ParentPlayer);

    //    ParentPlayer = GetComponentInParent<NetPlayer>();


    //    Debug.Log("parent player is " + ParentPlayer);
    //    LocalPlayer = ParentPlayer.LocalPlayer;
    //    TeamNum = ParentPlayer.GetTeamNum();
    //    SetTeamNum(TeamNum);
    //    //PV.RPC("RPC_SetTeamNum", RpcTarget.All, TeamNum);
    //    TextName.text = ParentPlayer.name;
    //    if(LocalPlayer == ParentPlayer)
    //    {
    //        TextName.gameObject.SetActive(false);
    //    }
    //    if (LocalPlayer.GetTeamNum() != ParentPlayer.GetTeamNum())
    //    {
    //        TextName.gameObject.SetActive(false);
    //    }
    //}

    public void ResetPNT()
    {
        PNT.ForceStart();
        SetPlayerTag = true;
    }
    public void FinalPlayerSet(int ParentIndex, string displayName)
    {
        PNT.ForceStart();
        PV.RPC("RPC_SetUpPlayer", RpcTarget.AllBuffered, ParentIndex, displayName);
    }
    [PunRPC]
    private void RPC_SetUpPlayer(int ParentIndex, string displayName)
    {
        //Debug.LogError("Dummy Error");
        ParentObject = PhotonView.Find(ParentIndex).gameObject;
        if (ParentPlayer == null)
            ParentPlayer = ParentObject.GetComponent<NetPlayer>();
        if (LocalPlayer == null)
            LocalPlayer = ParentPlayer.LocalPlayer;
        //ParentPlayer = GetComponentInParent<NetPlayer>();
        //if (LocalPlayer == null && ParentPlayer.LocalPlayer != null)
        //    LocalPlayer = ParentPlayer.LocalPlayer;
        //SetTeamNum(TeamNum);
        TextName.text = displayName;
        PNT.ForceStart();
        if (LocalPlayer == ParentPlayer)
        {
            TextName.gameObject.SetActive(false);
        }
        if (LocalPlayer.GetTeamNum() != ParentPlayer.GetTeamNum())
        {
            TextName.gameObject.SetActive(false);
        }
        SetTeamNum(TeamNum);
    }
    public void SetUpPlayer(NetPlayer ParentNetObject)
    {
        ParentPlayer = ParentNetObject;
        if (LocalPlayer == null && ParentPlayer.LocalPlayer != null)
            LocalPlayer = ParentPlayer.LocalPlayer;
        PV.RPC("RPC_UpdateLocalPlayer", RpcTarget.AllBuffered, ParentNetObject.GetComponent<PhotonView>().ViewID);

        gameObject.name = ParentPlayer.name.Split('#')[0];
        Code = ParentPlayer.CodeNumbers;
        UpdateName(gameObject.name);
        PV.RPC("RPC_UpdateCode", RpcTarget.All, Code);
        TeamNum = ParentPlayer.GetTeamNum();
        transform.GetComponentInChildren<LookAtPostionFollow>().UnParent();
        PV.RPC("RPC_UpdateTeamNum", RpcTarget.All, TeamNum);
        //PV.RPC("RPC_SetUpPlayer", RpcTarget.AllBuffered);
        //SetTeamNum(TeamNum);

        if (DisplayName == "")
            DisplayName = (string)PhotonNetwork.LocalPlayer.CustomProperties["DisplayName"];
        TextName.text = DisplayName;
        if (LocalPlayer == ParentPlayer)
        {
            TextName.gameObject.SetActive(false);
        }
        if (LocalPlayer.GetTeamNum() != ParentPlayer.GetTeamNum())
        {
            TextName.gameObject.SetActive(false);
        }

        if (LocalPlayer != null && SetLocalPlayerCalled)
        {
            PlayerLocalSet = true;
            PV.RPC("RPC_UpdateLocalSet", RpcTarget.AllBuffered, PlayerLocalSet);
        }
    }

    [PunRPC]
    private void RPC_UpdateLocalPlayer(int PV_Index_Parent)
    {
        SetLocalPlayer(PhotonView.Find(PV_Index_Parent).gameObject);
    }

    private void SetLocalPlayer(GameObject LP)
    {
        ParentObject = LP;
        ParentPlayer = LP.GetComponent<NetPlayer>();
        LocalPlayer = ParentPlayer.LocalPlayer;

        if (transform.parent == null || transform.parent != ParentObject.transform)
        {
            transform.SetParent(ParentObject.transform);
        }
        SetLocalPlayerCalled = true;
    }

    public void SetTeamNum(int team)
    {
        if (LocalPlayer == null)
            LocalPlayer = ParentPlayer.LocalPlayer;
        CD.LocalPlayer = LocalPlayer;
        CD.ParentPlayer = ParentPlayer;
        TeamNum = team;

        //
        if (TeamNum == 1)
        {
            transform.tag = "Team 1";
            //CT.ChangeObjectTags("Team 1");
        }
        else if (TeamNum == 2)
        {
            transform.tag = "Team 2";
            //CT.ChangeObjectTags("Team 2");
        }
        //

        if (SetColorByTeamNum)
        {
            if (LocalPlayer.GetTeamNum() == 1)
            {
                //print("Set Blue Avatar - Local != Parent");
                SetBlueActive();
            }
            else
            {
                //print("Set Red Avatar - Local != Parent");
                SetRedActive();
            }
        }
        else
        {
            if (LocalPlayer == ParentPlayer)
            {
                //print("Set Blue Avatar - Local = Parent");
                SetBlueActive();
                //
                pSLPS.enabled = true;
                pSLPS.teamTag = pSLPS.player.tag;
                // Debug.Log("call set changes");
                pSLPS.SetChanges();
                //
            }
            else
            {
                if (LocalPlayer.GetTeamNum() == TeamNum)
                {
                    //print("Set Blue Avatar - Local != Parent");
                    SetBlueActive();
                }
                else
                {
                    //print("Set Red Avatar - Local != Parent");
                    SetRedActive();
                }
            }
        }
        PV.RPC("RPC_UpdateTag", RpcTarget.All, gameObject.tag);


        CD.ForcedStart2();
    }

    [PunRPC]
    public void RPC_UpdateTeamNum(int Team)
    {
        TeamNum = Team;

    }

    public void UpdateName(string name)
    {
        PV.RPC("RPC_UpdateName", RpcTarget.AllBuffered, name);
    }
    [PunRPC]
    public void RPC_UpdateName(string name)
    {
        gameObject.name = name;
    }

    [PunRPC]
    public void RPC_UpdateCode(string code)
    {
        Code = code;
        NetPlayer[] Players = FindObjectsOfType<NetPlayer>();
        foreach (NetPlayer NP in Players)
        {
            string parentCode = NP.CodeNumbers;
            if (Code == code)
            {
                transform.SetParent(NP.gameObject.transform);
                break;
            }
        }
    }

    public string GetCode()
    {
        return Code;
    }
    [PunRPC]
    public void RPC_UpdateTag(string ThisTag)
    {
        this.tag = ThisTag;
        //CT.ChangeObjectTags(ThisTag);
    }

    public void SetBlueActive()
    {
        BluePlayer.SetActive(true);
        RedPlayer.SetActive(false);
        BH.SetHand(BlueHand);
        BH.FakeBall = BlueFakeBall;
    }

    public void SetRedActive()
    {
        BluePlayer.SetActive(false);
        RedPlayer.SetActive(true);
        BH.SetHand(RedHand);
        BH.FakeBall = RedFakeBall;
    }
}
