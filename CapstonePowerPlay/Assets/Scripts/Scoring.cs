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

    [SerializeField]
    //private GameObject scoreUICanvas;
    //[SerializeField]
    //private Text textUiTeam1;
    //[SerializeField]
    //private Text textUiTeam2;

    private ScoreTracker sTracker;


    [SerializeField]
    private Transform localPlayer;


    private void Start()
    {
        sTracker = GameObject.FindGameObjectWithTag("ScoreUI").GetComponent<ScoreTracker>();



        if (!localPlayer)
        {
            foreach (GameObject player in GameObject.FindGameObjectsWithTag("Team 1"))
            {
                if (player.GetComponent<hoverBoardScript>().isActiveAndEnabled)
                {
                    Debug.Log(player.name + " is the local player");
                    localPlayer = player.transform;

                    if (player.GetComponent<PlayerColor>().TeamNum == 1)
                    {
                        for (int i = 0; i < sTracker.scoreCanvases.Length; i++)
                        {
                            sTracker.t1Scoring[i].color = Color.blue;
                            sTracker.t2Scoring[i].color = Color.red;
                        }
                    }
                    else if (player.GetComponent<PlayerColor>().TeamNum == 2)
                    {
                        for (int i = 0; i < sTracker.scoreCanvases.Length; i++)
                        {
                            sTracker.t1Scoring[i].color = Color.red;

                            float tempX = sTracker.t1Scoring[i].rectTransform.position.x;

                            sTracker.t1Scoring[i].rectTransform.position = sTracker.t2Scoring[i].rectTransform.position;

                            sTracker.t2Scoring[i].color = Color.blue;

                            sTracker.t2Scoring[i].rectTransform.position = new Vector3(tempX, sTracker.t1Scoring[i].rectTransform.position.y, sTracker.t1Scoring[i].rectTransform.position.z);

                        }
                    }
                }
            }

            foreach (GameObject player in GameObject.FindGameObjectsWithTag("Team 2"))
            {
                if (player.GetComponent<hoverBoardScript>().isActiveAndEnabled)
                {
                    Debug.Log(player.name + " is the local player");
                    localPlayer = player.transform;

                    if (player.GetComponent<PlayerColor>().TeamNum == 1)
                    {
                        for (int i = 0; i < sTracker.scoreCanvases.Length; i++)
                        {
                            sTracker.t1Scoring[i].color = Color.blue;
                            sTracker.t2Scoring[i].color = Color.red;
                        }
                    }
                    else if (player.GetComponent<PlayerColor>().TeamNum == 2)
                    {
                        for (int i = 0; i < sTracker.scoreCanvases.Length; i++)
                        {
                            sTracker.t1Scoring[i].color = Color.red;

                            float tempX = sTracker.t1Scoring[i].rectTransform.position.x;

                            sTracker.t1Scoring[i].rectTransform.position = sTracker.t2Scoring[i].rectTransform.position;

                            sTracker.t2Scoring[i].color = Color.blue;

                            sTracker.t2Scoring[i].rectTransform.position = new Vector3(tempX, sTracker.t1Scoring[i].rectTransform.position.y, sTracker.t1Scoring[i].rectTransform.position.z);

                        }
                    }
                }
            }
        }



        //scoreUICanvas = GameObject.FindGameObjectWithTag("ScoreUI");
        //textUiTeam1 = scoreUICanvas.transform.GetChild(0).GetComponent<Text>();
        //textUiTeam2 = scoreUICanvas.transform.GetChild(1).GetComponent<Text>();

        HandleScoreCanvas();
    }

    

    public void HandleScoreCanvas()
    {
        //scoreDisplay.text = "Team1: " + team1Score + " | Team#2: " + team2Score;
        for (int i = 0; i < sTracker.scoreCanvases.Length; i++)
        {
            sTracker.t1Scoring[i].text = team1Score.ToString();
            sTracker.t2Scoring[i].text = team2Score.ToString();
        }
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

        if (!localPlayer)
        {
            foreach (GameObject player in GameObject.FindGameObjectsWithTag("Team 1"))
            {
                if (player.GetComponent<hoverBoardScript>().isActiveAndEnabled)
                {
                    Debug.Log(player.name + " is the local player");
                    localPlayer = player.transform;

                    if(player.GetComponent<PlayerColor>().TeamNum == 1)
                    {
                        for (int i = 0; i < sTracker.scoreCanvases.Length; i++)
                        {
                            sTracker.t1Scoring[i].color = Color.blue;
                            sTracker.t2Scoring[i].color = Color.red;
                        }
                    }
                    else if (player.GetComponent<PlayerColor>().TeamNum == 2)
                    {
                        for (int i = 0; i < sTracker.scoreCanvases.Length; i++)
                        {
                            sTracker.t1Scoring[i].color = Color.red;

                            float tempX = sTracker.t1Scoring[i].rectTransform.position.x;

                            sTracker.t1Scoring[i].rectTransform.position = sTracker.t2Scoring[i].rectTransform.position;

                            sTracker.t2Scoring[i].color = Color.blue;

                            sTracker.t2Scoring[i].rectTransform.position = new Vector3(tempX, sTracker.t1Scoring[i].rectTransform.position.y, sTracker.t1Scoring[i].rectTransform.position.z);
                        }
                    }


                }
            }

            foreach (GameObject player in GameObject.FindGameObjectsWithTag("Team 2"))
            {
                if (player.GetComponent<hoverBoardScript>().isActiveAndEnabled)
                {
                    //Debug.Log(player.name + " is the local player");
                    localPlayer = player.transform;


                    if (player.GetComponent<PlayerColor>().TeamNum == 1)
                    {
                        for (int i = 0; i < sTracker.scoreCanvases.Length; i++)
                        {
                            sTracker.t1Scoring[i].color = Color.blue;
                            sTracker.t2Scoring[i].color = Color.red;
                        }
                    }
                    else if (player.GetComponent<PlayerColor>().TeamNum == 2)
                    {
                        for (int i = 0; i < sTracker.scoreCanvases.Length; i++)
                        {
                            sTracker.t1Scoring[i].color = Color.red;

                            float tempX = sTracker.t1Scoring[i].rectTransform.position.x;

                            sTracker.t1Scoring[i].rectTransform.position = sTracker.t2Scoring[i].rectTransform.position;

                            sTracker.t2Scoring[i].color = Color.blue;

                            sTracker.t2Scoring[i].rectTransform.position = new Vector3(tempX, sTracker.t1Scoring[i].rectTransform.position.y, sTracker.t1Scoring[i].rectTransform.position.z);
                        }
                    }


                }
            }
        }
    }
    // not working
    // 
    private void OnTriggerEnter(Collider c)
    {

        Debug.Log("Net Triggered");

        if(c.gameObject.tag == "Ball_Score_Trigger" && !scored)
        {
            int teamScored = c.transform.parent.GetComponent<Ball>().WhoTossedTheBall.GetComponent<PlayerColor>().TeamNum;
            Debug.Log("Ball has triggered the net");
            if(teamScored == 1)
            {
                Debug.Log("Team1 Scored!");
                CmdTeam1Score();
                timeUntilScoreReset = maxTimeUntilScoreReset;
                scored = true;
            }
            else if (teamScored == 2)
            {
                Debug.Log("Team2 Scored!");
                CmdTeam2Score();
                timeUntilScoreReset = maxTimeUntilScoreReset;
                scored = true;
            }
        }
    }
}
