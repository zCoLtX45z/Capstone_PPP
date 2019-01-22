using System.Collections;
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

    [SerializeField]
    private float maxTimeUntilScoreReset;
    private float timeUntilScoreReset;

    private GameObject scoreUICanvas;
    private Text textUiTeam1;
    private Text textUiTeam2;

    [SerializeField]
    private List<Transform> players = new List<Transform>();


    private void Start()
    {
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Team1"))
        {
            players.Add(player.transform);
        }

        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Team2"))
        {
            players.Add(player.transform);
        }

        scoreUICanvas = GameObject.FindGameObjectWithTag("ScoreUI");
        textUiTeam1 = scoreUICanvas.transform.GetChild(0).GetComponent<Text>();
        textUiTeam2 = scoreUICanvas.transform.GetChild(1).GetComponent<Text>();

        HandleScoreCanvas();
    }

    

    public void HandleScoreCanvas()
    {
        scoreDisplay.text = "team#1: " + team1Score + " | Team#2: " + team2Score;
        textUiTeam1.text = "Team1: " + team1Score;
        textUiTeam2.text = "Team2: " + team2Score;
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
        HandleScoreCanvas();
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
        HandleScoreCanvas();
    }

    private void Update()
    {
        if(scored)
        {
            timeUntilScoreReset -= Time.deltaTime;
            if(timeUntilScoreReset <= 0)
            {
                scored = false;
            }
        }


        //foreach (GameObject player in GameObject.FindGameObjectsWithTag("Team1"))
        //{
        //    for (int i = 0; i < players.Count; i++)
        //    {
        //        if(players[i] == player)
        //        {
        //            break;
        //        }
        //    }
        //    players.Add(player.transform);
        //}

        //foreach (GameObject player in GameObject.FindGameObjectsWithTag("Team2"))
        //{
        //    for (int i = 0; i < players.Count; i++)
        //    {
        //        if (players[i] == player)
        //        {
        //            break;
        //        }
        //    }
        //    players.Add(player.transform);
        //}

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
                timeUntilScoreReset = maxTimeUntilScoreReset;
                scored = true;
            }
            if (c.gameObject.GetComponent<Ball>().WhoTossedTheBall.GetComponent<PlayerColor>().TeamNum == 2)
            {
                Debug.Log("Team2 Scored!");
                CmdTeam2Score();
                timeUntilScoreReset = maxTimeUntilScoreReset;
                scored = true;
            }
        }
    }
}
