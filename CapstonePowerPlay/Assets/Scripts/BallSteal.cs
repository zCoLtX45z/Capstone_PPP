using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class BallSteal : NetworkBehaviour
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

    // Use this for initialization
    void Start()
    {

        player = gameObject;

        teamNum = player.GetComponent<PlayerColor>().TeamNum;

        
       if(ballTransform == null)
       {
            ballTransform = GameObject.FindGameObjectWithTag("Ball").transform;
            if(ballTransform != null)
            {
                ballScript = ballTransform.GetComponent<Ball>();
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
                ballTransform = GameObject.FindGameObjectWithTag("Ball").transform;
                if (ballTransform != null)
                {
                    ballScript = ballTransform.GetComponent<Ball>();
                }
            }
            else
            {
                if (ballScript.Hand && target == null)
                {
                    if (ballScript.BH.gameObject.GetComponent<PlayerColor>().TeamNum != teamNum)
                        target = ballScript.BH.gameObject;
                }
                if (ballScript.Hand == false)
                {
                    target = null;
                }

                if(target != null)
                {                    
                    directionFromPlayer = target.transform.position - transform.position;
                    distanceToTarget = directionFromPlayer.magnitude;
                    angle = Vector3.Angle(directionFromPlayer, transform.forward);

                    if(distanceToTarget < maxDistance && angle < stealMaxAngle)
                    {
                        //Debug.Log("In range and in view");

                        //steal
                        if(Input.GetMouseButtonDown(2))
                            target.GetComponent<BallHandling>().CmdSteal(player, ballTransform.gameObject, playerHandTransform.position, target);
                        

                    }
                    //if (distanceToTarget > maxDistance)
                    //{
                    //    Debug.Log("out of range");
                    //}
                    //if(angle > stealMaxAngle)
                    //{
                    //    Debug.Log("out of view");
                    //}


                }

                //Debug.Log("target: " + target);

            }
        }
    }
}


