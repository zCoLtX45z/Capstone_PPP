﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSteal : MonoBehaviour
{

    // the max angle that a teamate must be in between, in order to be an eligable target for passing
    [SerializeField]
    private float stealMaxAngle = 40f;

    // the max distance for a teamate must be within, in order to be an eligable target for passing
    [SerializeField]
    private float maxDistance = 0.0f;


    // distance between player and target
    [SerializeField]
    private float distanceToTarget;


    // direction from the player
    private Vector3 directionFromPlayer;

    // current angle
    [SerializeField]
    private float angle = 0;

    // target of passing
    [SerializeField]
    public GameObject target;


    // current angle between a target and the player
    private float currentAngle;

    [SerializeField]
    private int teamNum;

    [SerializeField]
    private Transform ballTransform;

    [SerializeField]
    private Ball ballScript;

    // player gameObject
    [SerializeField]
    public GameObject player;

    [SerializeField]
    private Transform playerHandTransform;

    private BallHandling bH;

    private PhotonView PV;

    private PlayerColor PC;

    public bool isPaused;

    // Use this for initialization
    void Start()
    {
        PC = GetComponent<PlayerColor>();
        PV = GetComponent<PhotonView>();

        player = gameObject;

        teamNum = player.GetComponent<PlayerColor>().TeamNum;

        bH = transform.GetComponent<BallHandling>();

       if (ballTransform == null)
       {
            if (GameObject.FindGameObjectWithTag("Ball") != null)
            {
                ballTransform = GameObject.FindGameObjectWithTag("Ball").transform;
                if (ballTransform != null)
                {
                    ballScript = ballTransform.GetComponent<Ball>();
                }
            }
       }



    }

    // Update is called once per frame
    void Update()
    {


        if (teamNum == 0)
        {
            teamNum = player.GetComponent<PlayerColor>().TeamNum;
        }
        else
        {





            if (ballTransform == null)
            {
                if(GameObject.FindGameObjectWithTag("Ball") != null)
                    ballTransform = GameObject.FindGameObjectWithTag("Ball").transform;
                if (ballTransform != null)
                {
                    if (ballScript == null)
                    {
                        ballScript = ballTransform.GetComponent<Ball>();
                    }
                }
            }

            if (ballScript == null)
            {
                if (FindObjectOfType<Ball>())
                    ballScript = FindObjectOfType<Ball>();
                if (ballScript != null)
                {
                    if (ballTransform == null)
                    {
                        ballTransform = ballScript.transform;
                    }
                }
            }
            if (PC.LocalPlayer == PC.ParentPlayer)
            {
                if (ballTransform != null)
                {
                    if (ballScript.Hand && target == null)
                    {
                        if (ballScript.BH != null)
                        {
                            //Debug.Log("BH");
                            if (ballScript.BH.gameObject != null)
                            {
                                // Debug.Log("BH.gameObject");
                                if (ballScript.BH.gameObject.GetComponent<PlayerColor>() != null)
                                {
                                    // Debug.Log("BH.gameObject.GET<PlayerColor>");
                                    if (ballScript.BH.gameObject.GetComponent<PlayerColor>().TeamNum != teamNum)
                                    {
                                        // Debug.Log("BH.gameObject.GET<PlayerColor>(). teamNum");
                                        target = ballScript.BH.gameObject;
                                    }
                                }
                            }
                        }
                    }
                    if (ballScript.Hand == false)
                    {
                        target = null;
                    }
                    // Debug.Log("TeamNum: " + teamNum);
                    if (bH.ball == null)
                    {
                        //Debug.Log("Can steal");
                        if (target != null)
                        {
                            // Debug.Log("Target has been selected");
                            directionFromPlayer = target.transform.position - transform.position;
                            distanceToTarget = directionFromPlayer.magnitude;
                            angle = Vector3.Angle(directionFromPlayer, transform.forward);

                            if (distanceToTarget < maxDistance && angle < stealMaxAngle)
                            {
                                //Debug.Log("In range and in view");

                                //steal
                                if (Input.GetMouseButtonDown(0) && !isPaused)
                                {

                                    // ballScript.DropBall();
                                    BallHandling BH = target.GetComponent<BallHandling>();
                                    Debug.Log("BH.tag: " + target.tag);
                                    BH.Pass(player.gameObject, ballTransform.gameObject, playerHandTransform.position, target);
                                    PV.RPC("RPC_DisableBallOfTarget", RpcTarget.AllBuffered, target.GetPhotonView().ViewID);
                                }


                            }

                        }
                    }
                    

                }
            }

            

        }
    }
    [PunRPC]
    public void RPC_DisableBallOfTarget(int viewId)
    {
        BallHandling BH = PhotonView.Find(viewId).GetComponent<BallHandling>();
        BH.ball = null;
    }
}
