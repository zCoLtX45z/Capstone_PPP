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

    public int TeamNum = 0;
    public NetPlayer LocalPlayer;
    public NetPlayer ParentPlayer;

    [SerializeField]
    private ComponentsToDisable CD;

    [SerializeField]
    private PlayerSoftlockPassSight pSLPS;

    //photon variables
    [SerializeField]
    private PhotonView PV;
    private string Code = "None";

    // Setting up players
    [HideInInspector]
    public bool PlayerLocalSet = false;
    public GameObject ParentObject;

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


    public void SetUpPlayer1()
    {
        PV = GetComponent<PhotonView>();
        Debug.Log("PV: S" + PV);
        Debug.Log("SetUpPlayer Called");
        SetUpPlayer();
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

    public void FinalPlayerSet()
    {
        PV.RPC("RPC_SetUpPlayer", RpcTarget.AllBuffered);
    }
    [PunRPC]
    private void RPC_SetUpPlayer()
    {
        ParentPlayer = GetComponentInParent<NetPlayer>();
        if (LocalPlayer == null && ParentPlayer.LocalPlayer != null)
            LocalPlayer = ParentPlayer.LocalPlayer;
        //SetTeamNum(TeamNum);
        TextName.text = ParentPlayer.name;
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
    public void SetUpPlayer()
    {
        ParentPlayer = GetComponentInParent<NetPlayer>();
        if (LocalPlayer == null && ParentPlayer.LocalPlayer != null)
            LocalPlayer = ParentPlayer.LocalPlayer;
        PV.RPC("RPC_UpdateLocalPlayer", RpcTarget.AllBuffered, ParentPlayer.GetComponent<PhotonView>().ViewID);

        gameObject.name = ParentPlayer.name.Split('#')[0];
        Code = ParentPlayer.CodeNumbers;
        UpdateName(gameObject.name);
        PV.RPC("RPC_UpdateCode", RpcTarget.All, Code);
        TeamNum = ParentPlayer.GetTeamNum();
        PV.RPC("RPC_UpdateTeamNum", RpcTarget.All, TeamNum);
        //PV.RPC("RPC_SetUpPlayer", RpcTarget.AllBuffered);
        //SetTeamNum(TeamNum);

        TextName.text = ParentPlayer.name;
        if (LocalPlayer == ParentPlayer)
        {
            TextName.gameObject.SetActive(false);
        }
        if (LocalPlayer.GetTeamNum() != ParentPlayer.GetTeamNum())
        {
            TextName.gameObject.SetActive(false);
        }

        if (LocalPlayer != null)
        {
            PlayerLocalSet = true;
            PV.RPC("RPC_UpdateLocalSet", RpcTarget.AllBuffered, PlayerLocalSet);
        }
        else
        {
            LocalPlayer = ParentPlayer.LocalPlayer;
        }
    }

    [PunRPC]
    private void RPC_UpdateLocalPlayer(int PV_Index_Parent)
    {
        SetLocalPlayer(PhotonView.Find(PV_Index_Parent).gameObject);
    }

    private void SetLocalPlayer(GameObject LP)
    {
        ParentPlayer = LP.GetComponent<NetPlayer>();
        LocalPlayer = ParentPlayer.LocalPlayer;
    }

    private void SetTeamNum(int team)
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
        }
        else if (TeamNum == 2)
        {
            transform.tag = "Team 2";
        }
        //

        if (LocalPlayer == ParentPlayer)
        {
            print("Set Blue Avatar - Local = Parent");
            SetBlueActive();
            //
            pSLPS.enabled = true;
            pSLPS.teamTag = pSLPS.player.tag;
            //
        }
        else
        {
            if (LocalPlayer.GetTeamNum() == TeamNum)
            {
                print("Set Blue Avatar - Local != Parent");
                SetBlueActive();
            }
            else
            {
                print("Set Red Avatar - Local != Parent");
                SetRedActive();
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
            string parentCode = "#" + NP.gameObject.name.Split('#')[1];
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
