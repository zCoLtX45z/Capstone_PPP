﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour {

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

    private ballHandler bHandler;

    [SerializeField]
    private GameObject ballSpawnLocation;






    [SerializeField]
    private GameObject drawGameText;

    [SerializeField]
    private GameObject winGameText;

    [SerializeField]
    private GameObject loseGameText;

    private Scoring scoring;



    [SerializeField]
    private Transform[] spawnLocationsTeam1;

    [SerializeField]
    private Transform[] spawnLocationsTeam2;


    [SerializeField]
    private GameObject[] objectGroup_Round1;

    [SerializeField]
    private GameObject[] objectGroup_Round2;

    [SerializeField]
    private GameObject[] objectGroup_Round3;

   



    private void Start()
    {
        PV = GetComponent<PhotonView>();
        nSPawner = FindObjectOfType<netSpawner>();
        bHandler = FindObjectOfType<ballHandler>();
        scoring = FindObjectOfType<Scoring>();


        // set round 1 level layout
        for (int i = objectGroup_Round1.Length - 1; i >= 0; i--)
        {
            objectGroup_Round1[i].SetActive(true);
        }

        for (int i = objectGroup_Round2.Length - 1; i >= 0; i--)
        {
            objectGroup_Round2[i].SetActive(false);
        }

        for (int i = objectGroup_Round3.Length - 1; i >= 0; i--)
        {
            objectGroup_Round3[i].SetActive(false);
        }


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
                //if (PhotonNetwork.IsMasterClient)
                //{
                    NextRound();
                    allowTime = false;
                //
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
        Debug.Log("Reseting round");
        roundNumber++;
        if(roundNumber >= maxRoundNumber)
        {
            Debug.Log("End");
            PV.RPC("RPC_End", RpcTarget.All);
        }
        else
        {
            // round 2
            if(roundNumber == 1)
            {
                for (int i = objectGroup_Round1.Length - 1; i >= 0; i--)
                {
                    objectGroup_Round1[i].SetActive(false);
                }

                for (int i = objectGroup_Round2.Length - 1; i >= 0; i--)
                {
                    objectGroup_Round2[i].SetActive(true);
                }

                for (int i = objectGroup_Round3.Length - 1; i >= 0; i--)
                {
                    objectGroup_Round3[i].SetActive(false);
                }
            }
            // round 3
            if (roundNumber == 2)
            {
                for (int i = objectGroup_Round1.Length - 1; i >= 0; i--)
                {
                    objectGroup_Round1[i].SetActive(false);
                }

                for (int i = objectGroup_Round2.Length - 1; i >= 0; i--)
                {
                    objectGroup_Round2[i].SetActive(false);
                }

                for (int i = objectGroup_Round3.Length - 1; i >= 0; i--)
                {
                    objectGroup_Round3[i].SetActive(true);
                }
            }


            //// round 3
            //if (roundNumber == 2)
            //{

            //}

            Debug.Log("Destroy ball");
            PhotonNetwork.Destroy(ball.gameObject);

            bHandler.SpawnBall();
            ball = FindObjectOfType<Ball>().gameObject;
            

            nSPawner.CallMoveNetDown();
            textTime.text = "";

            Debug.Log("ResetPos");


            if(PhotonNetwork.IsMasterClient)
            {
                PV.RPC("ResetPlayerPositions", RpcTarget.All);
            }
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


    //[SerializeField]
    private List<Transform> team1Players = new List<Transform>();
    //[SerializeField]
    private List<Transform> team2Players = new List<Transform>();

    [PunRPC]
    public void ResetPlayerPositions()
    {
        foreach (GameObject team1 in GameObject.FindGameObjectsWithTag("Team 1"))
        {
            team1Players.Add(team1.transform);
        }

        foreach (GameObject team2 in GameObject.FindGameObjectsWithTag("Team 2"))
        {
            team1Players.Add(team2.transform);
        }

        for (int i = 0; i < team1Players.Count; i++)
        {
            team1Players[i].position = spawnLocationsTeam1[i].position;
            team1Players[i].rotation = spawnLocationsTeam1[i].rotation;
        }
        for (int i = team1Players.Count - 1; i >= 0; i--)
        {
            team1Players.RemoveAt(i);
        }
        
        for (int i = 0; i < team2Players.Count; i++)
        {
            team2Players[i].position = spawnLocationsTeam2[i].position;
            team2Players[i].rotation = spawnLocationsTeam2[i].rotation;
        }

        for (int i = team2Players.Count - 1; i >= 0; i--)
        {
            team2Players.RemoveAt(i);
        }
    }

}
