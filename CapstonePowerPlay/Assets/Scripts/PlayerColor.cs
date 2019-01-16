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

    private int TeamNum = 0;
    private NetPlayer LocalPlayer;

    public void SetUpPlayer(int teamNum, NetPlayer localPlayer)
    {
        LocalPlayer = localPlayer;
        SetTeamNum(teamNum);
    }
    public void SetTeamNum(int team)
    {
        TeamNum = team;
        if (isLocalPlayer)
        {
            SetBlueActive();
        }
        else
        {
            if (LocalPlayer.TeamNum == TeamNum)
            {
                SetBlueActive();
            }
            else
            {
                SetRedActive();
            }
        }
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
