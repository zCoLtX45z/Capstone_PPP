using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerColor : MonoBehaviour {

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
    private PhotonView PV;

    void Start()
    {
        PV = GetComponent<PhotonView>();
    }

    
    public void SetUpPlayer1()
    {
        PV.RPC("RPC_SetUpPlayer", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void RPC_SetUpPlayer()
    {
        ParentPlayer = GetComponentInParent<NetPlayer>();
        LocalPlayer = ParentPlayer.LocalPlayer;
        TeamNum = ParentPlayer.GetTeamNum();
        SetTeamNum(TeamNum);
        TextName.text = ParentPlayer.name;
        if(LocalPlayer == ParentPlayer)
        {
            TextName.gameObject.SetActive(false);
        }
        if (LocalPlayer.GetTeamNum() != ParentPlayer.GetTeamNum())
        {
            TextName.gameObject.SetActive(false);
        }
    }

    public void SetUpPlayer()
    {
        ParentPlayer = GetComponentInParent<NetPlayer>();
        LocalPlayer = ParentPlayer.LocalPlayer;
        TeamNum = ParentPlayer.GetTeamNum();
        SetTeamNum(TeamNum);
        TextName.text = ParentPlayer.name;
        if (LocalPlayer == ParentPlayer)
        {
            TextName.gameObject.SetActive(false);
        }
        if (LocalPlayer.GetTeamNum() != ParentPlayer.GetTeamNum())
        {
            TextName.gameObject.SetActive(false);
        }
    }

    public void SetTeamNum(int team)
    {
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



        if (LocalPlayer.PlayerCode == ParentPlayer.PlayerCode)
        {
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
                SetBlueActive();
            }
            else
            {
                SetRedActive();
            }
        }
        CD.ForcedStart2();
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
