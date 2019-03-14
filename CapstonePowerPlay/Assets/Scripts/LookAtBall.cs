using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtBall : MonoBehaviour
{

    private Transform lookatBall;

    [SerializeField]
    private BallHandling thisPlayerBallHandling;


    public bool allow = false;

    //public List<Transform> players = new List<Transform>();


    private CameraModeMedium cMM;

    [SerializeField]
    private Transform upRotationRootObj;


    [SerializeField]
    private float maxAngle;

    [SerializeField]
    private float speedRot;

    public bool hardLock = false;
    [SerializeField]
    private Transform handTransform;

    // Use this for initialization
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Ball") != null)
            lookatBall = GameObject.FindGameObjectWithTag("Ball").transform;
        //if (lookatBall != null)
        //{
        //    foreach (GameObject playerObj in GameObject.FindGameObjectsWithTag("Team 1"))
        //    {
        //        if (playerObj != thisPlayerBallHandling.transform)
        //            players.Add(playerObj.transform);
        //    }
        //    foreach (GameObject playerObj in GameObject.FindGameObjectsWithTag("Team 2"))
        //    {
        //        players.Add(playerObj.transform);
        //    }

        //    cMM = transform.GetComponent<CameraModeMedium>();
        //}
        cMM = transform.GetComponent<CameraModeMedium>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lookatBall != null)
        {
            if (allow)
            {

                if (lookatBall.parent != handTransform)
                {
                    if (!hardLock)
                    {
                        Vector3 direction = lookatBall.position - transform.position;
                        float angle = Vector3.Angle(direction, transform.forward);
                        if (angle >= maxAngle)
                        {
                            Quaternion rot = Quaternion.LookRotation(direction);
                            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, speedRot * angle * Time.deltaTime);
                        }
                        else
                            hardLock = true;
                    }
                    else
                    {
                        transform.LookAt(lookatBall, upRotationRootObj.up);
                    }
                }
                else
                {
                    Debug.Log("Swip");
                    cMM.ChangeCameraMode();
                }
                /*
                else
                {
                    if (thisPlayerBallHandling.ball == null)
                    {
                        for (int i = 0; i < players.Count; i++)
                        {
                            if (players[i].GetComponent<BallHandling>().ball != null)
                            {
                                // origin location
                                if (!hardLock)
                                {
                                    Vector3 direction = players[i].GetChild(2).position - transform.position;
                                    float angle = Vector3.Angle(direction, transform.forward);
                                    if (angle >= maxAngle)
                                    {
                                        Quaternion rot = Quaternion.LookRotation(direction);
                                        transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, speedRot * angle * Time.deltaTime);
                                    }
                                    else
                                    {
                                        hardLock = true;
                                    }
                                }

                                else
                                {
                                    // original
                                    transform.LookAt(players[i].GetChild(2), upRotationRootObj.up);
                                }
                            }
                        }
                    }
                    else
                    {
                        Debug.Log("Change");
                        cMM.ChangeCameraMode();
                    }

                }
                */
                /*
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
                */
            }
        }
        else
        {
            if (GameObject.FindGameObjectWithTag("Ball") != null)
                lookatBall = GameObject.FindGameObjectWithTag("Ball").transform;
        }
    }
}