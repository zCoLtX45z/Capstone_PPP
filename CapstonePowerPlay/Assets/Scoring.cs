﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Scoring : NetworkBehaviour
{
    [SerializeField]
    private Text scoreDisplay;
    [SerializeField]
    private int team1Score = 0;
    [SerializeField]
    private int team2Score = 0;
    private bool scored = false;



	// Update is called once per frame
	void Update ()
    {
        HandleScoreCanvas();

    }

    public void HandleScoreCanvas()
    {
        scoreDisplay.text = "team#1: " + team1Score + " | Team#2: " + team2Score;
    }


    [Command]
    public void CmdTeam1Score()
    {
        Debug.Log("Cmd team1");
        RpcTeam1Score();
    }
    [ClientRpc]
    public void RpcTeam1Score()
    {
        Debug.Log("add point to team 1");
        team1Score++;
    }




    [Command]
    public void CmdTeam2Score()
    {
        Debug.Log("Cmd team2");
        RpcTeam2Score();
    }
    [ClientRpc]
    public void RpcTeam2Score()
    {
        Debug.Log("add point to team 2");
        team2Score++;
    }



    public void OnTriggerEnter(Collider c)
    {
        Debug.Log("Net Triggered");

        if(c.gameObject.tag == "Ball" && !scored)
        {
            Debug.Log("Ball has triggered the net");
            if(c.gameObject.GetComponent<Ball>().WhoTossedTheBall.GetComponent<PlayerColor>().TeamNum == 1)
            {
                Debug.Log("Team1 Scored!");
                CmdTeam1Score();
                scored = true;
            }
            if (c.gameObject.GetComponent<Ball>().WhoTossedTheBall.GetComponent<PlayerColor>().TeamNum == 2)
            {
                Debug.Log("Team2 Scored!");
                CmdTeam2Score();
                scored = true;
            }
        }
    }
}
