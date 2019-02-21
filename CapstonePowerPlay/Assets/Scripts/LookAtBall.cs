using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtBall : MonoBehaviour {

    private Transform lookatBall;

    [SerializeField]
    private BallHandling thisPlayerBallHandling;


    public bool allow = false;

    public List<Transform> players = new List<Transform>();


    private CameraModeMedium cMM;

    [SerializeField]
    private Transform upRotationRootObj;

    // Use this for initialization
    void Start () {
        lookatBall = GameObject.FindGameObjectWithTag("Ball").transform;

        foreach (GameObject playerObj in GameObject.FindGameObjectsWithTag("Team 1"))
        {
            if(playerObj != thisPlayerBallHandling.transform)
                players.Add(playerObj.transform);
        }
        foreach (GameObject playerObj in GameObject.FindGameObjectsWithTag("Team 2"))
        {
            players.Add(playerObj.transform);
        }

        cMM = transform.GetComponent<CameraModeMedium>();

       
    }
	
	// Update is called once per frame
	void Update () {
        if (allow)
        {
            if (lookatBall.GetChild(0).gameObject.activeInHierarchy)
            {
                transform.LookAt(lookatBall, upRotationRootObj.up);
            }
            else
            {
                if (thisPlayerBallHandling.ball == null)
                {
                    for (int i = 0; i < players.Count; i++)
                    {
                        if (players[i].GetComponent<BallHandling>().ball != null)
                        {
                            transform.LookAt(players[i].GetChild(2), upRotationRootObj.up);
                        }
                    }
                }
                else
                {
                    Debug.Log("Change");
                    cMM.ChangeCameraMode();
                }

            }

            int numberOfPlayers = 0;


            foreach (GameObject playerObj in GameObject.FindGameObjectsWithTag("Team 1"))
            {
                numberOfPlayers++;
            }
            foreach (GameObject playerObj in GameObject.FindGameObjectsWithTag("Team 2"))
            {
                numberOfPlayers++;
            }

            if (numberOfPlayers > players.Count)
            {
                for (int i = players.Count - 1; i >= 0; i--)
                {
                    players.Remove(players[i]);
                }

                foreach (GameObject playerObj in GameObject.FindGameObjectsWithTag("Team 1"))
                {
                    players.Add(playerObj.transform);
                }

                foreach (GameObject playerObj in GameObject.FindGameObjectsWithTag("Team 2"))
                {
                    players.Add(playerObj.transform);
                }
            }
        }

    }
}