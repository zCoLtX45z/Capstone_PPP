using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerColor : NetworkBehaviour {

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

    public int TeamNum = 0;
    public NetPlayer LocalPlayer;

    [SerializeField]
    private ComponentsToDisable CD;

    [Command]
    public void CmdSetUpPlayer(int teamNum, GameObject localObject)
    {
        RpcSetUpPlayer(teamNum, localObject);
    }

    [ClientRpc]
    public void RpcSetUpPlayer(int teamNum, GameObject localObject)
    {
        LocalPlayer = localObject.GetComponent<NetPlayer>();
        SetTeamNum(teamNum);
    }

    public void SetTeamNum(int team)
    {
        CD.LocalPlayer = LocalPlayer;
        TeamNum = team;
        if (LocalPlayer.PlayerCode == gameObject.name)
        {
            SetBlueActive();
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
        CD.ForcedStart();
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
