using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundTimer : MonoBehaviour {

    [SerializeField]
    private float maxRoundTime;

    private float roundTime;

    private bool allowTime;


    [SerializeField]
    private Text textTime;

    private PhotonView PV;



    [SerializeField]
    private float maxIntroTime;

    private float introTimer;

    private bool allowCountDown;

    [SerializeField]
    private Text textCountDown;


    private float roundNumber = 0;



    private GameObject ball;


    //private GameObject net;

    private netSpawner nSPawner;

    private ballHandler bhandler;


    private void Start()
    {
        PV = GetComponent<PhotonView>();
        nSPawner = FindObjectOfType<netSpawner>();
        bhandler = FindObjectOfType<ballHandler>();
    }

    public void AllowTime(bool allow)
    {
        allowTime = allow;
    }

    public void ResetTime()
    {
        roundTime = maxRoundTime;
    }

    public void ResetIntroTimer()
    {
        introTimer = maxIntroTime;
    }


    public void BeginCountdown()
    {
        PV.RPC("RPC_BeginCountdown", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void RPC_BeginCountdown()
    {
        ball = FindObjectOfType<Ball>().gameObject;
        //net = FindObjectOfType<Scoring>().gameObject;
        //Debug.Log("net: " + net.name);

        nSPawner.CallMoveNetUp();

        ResetIntroTimer();
        allowCountDown = true;
    }

    private void BeginMatch()
    {
        PV.RPC("RPC_BeginMatch", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void RPC_BeginMatch()
    {
        ResetTime();
        AllowTime(true);
    }


    // Update is called once per frame
    void Update () {
		if(allowTime)
        {
            if(roundTime > 0)
            {
                roundTime -= Time.deltaTime;
                textTime.text = Mathf.Ceil(roundTime).ToString();
            }
            else
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    NextRound();
                    allowTime = false;
                }
            }
        }
        if(allowCountDown)
        {
            introTimer -= Time.deltaTime;
            textCountDown.text = Mathf.Ceil(introTimer).ToString();

            if(introTimer <= 0)
            {
                allowCountDown = false;
                BeginMatch();
                textCountDown.text = "";
            }
        }
    }


    public void NextRound()
    {
        PV.RPC("RPC_ResetRound", RpcTarget.All);
    }

    [PunRPC]
    public void RPC_ResetRound()
    {
        Destroy(ball);
        //Destroy(net);
        ball = null;
        //net = null;

        nSPawner.CallMoveNetDown();



        if (PhotonNetwork.IsMasterClient)
        {
            bhandler.SpawnBall();
        }
        Debug.Log("ResetPos");
    }

}
