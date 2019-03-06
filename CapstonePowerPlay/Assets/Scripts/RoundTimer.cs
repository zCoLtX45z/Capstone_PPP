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

    [SerializeField]
    private float maxRoundNumber = 3;

    private float roundNumber = 0;



    private GameObject ball;


    //private GameObject net;

    private netSpawner nSPawner;

    private ballHandler bhandler;

    [SerializeField]
    private GameObject ballSpawnLocation;






    [SerializeField]
    private GameObject drawGameText;

    [SerializeField]
    private GameObject winGameText;

    [SerializeField]
    private GameObject loseGameText;

    private Scoring scoring;

    private void Start()
    {
        PV = GetComponent<PhotonView>();
        nSPawner = FindObjectOfType<netSpawner>();
        bhandler = FindObjectOfType<ballHandler>();
        scoring = FindObjectOfType<Scoring>();
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
        ball.GetComponent<Ball>().hasBeenPickedUpBefore = true;

        //Debug.Log("net: " + net.name);

        nSPawner.CallMoveNetUp();

        ResetIntroTimer();
        ResetTime();
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

        roundNumber++;
        if(roundNumber >= maxRoundNumber)
        {
            Debug.Log("End");
            PV.RPC("RPC_End", RpcTarget.All);
        }
        else
        {
            
            ball.GetComponent<Ball>().ResetBall();
            ball.transform.position = ballSpawnLocation.transform.position;
            ball.GetComponent<Rigidbody>().isKinematic = false;
            ball.GetComponent<Rigidbody>().useGravity = true;
            //ball.GetComponent<Ball>().hasBeenPickedUpBefore = false;
            //ball.transform.SetParent(null);
            //ball.GetComponent<Rigidbody>().isKinematic = false;
            //ball.GetComponent<Rigidbody>().useGravity = true;
            //ball.GetComponent<Rigidbody>().velocity = Vector3.zero;

            nSPawner.CallMoveNetDown();
            textTime.text = "";

            Debug.Log("ResetPos");
        }
    }

    [PunRPC]
    public void RPC_End()
    {
        Debug.Log("End_RPC");

        if(scoring.team1Score > scoring.team2Score)
        {
            ShowTheWinner(1);
        }
        else if (scoring.team1Score < scoring.team2Score)
        {
            ShowTheWinner(2);
        }
        else 
        {
            ShowTheWinner(0);
        }

        
    }

    public void ShowTheWinner(int teamNum)
    {
        Transform localPlayer = null;
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Team 1"))
        {
            if (player.GetComponent<hoverBoardScript>().isActiveAndEnabled)
            {
                Debug.Log(player.name + " is the local player");
                localPlayer = player.transform;
            }
        }
        if (localPlayer == null)
        {
            foreach (GameObject player in GameObject.FindGameObjectsWithTag("Team 2"))
            {
                if (player.GetComponent<hoverBoardScript>().isActiveAndEnabled)
                {
                    Debug.Log(player.name + " is the local player");
                    localPlayer = player.transform;
                }
            }
        }

        if (teamNum == 0)
        {
            drawGameText.SetActive(true);
        }

        else if (localPlayer.GetComponent<PlayerColor>().TeamNum == teamNum)
        {
            // win
            winGameText.SetActive(true);
        }
        else
        {
            // lose
            loseGameText.SetActive(true);
        }


        Invoke("ReturnToMenu", 5);

    }

    public void ReturnToMenu()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(0);
    }
}
