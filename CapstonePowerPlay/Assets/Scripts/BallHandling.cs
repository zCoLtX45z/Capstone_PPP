using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BallHandling : NetworkBehaviour {

    // Set Shoot force
    [SerializeField]
    public float ShootForce = 200;
    [SerializeField]
    public float PassForce;

    // Get Force Direction from camera
    [SerializeField]
    private Camera Cam;
    private Vector3 Direction;
    [SerializeField]
    private PlayerSoftlockPassSight softLockScript;

    // Target for passes
    private GameObject Target;

    // Target position for passes
    private Vector3 TargetPosition;

    // Hand to where the ball goes
    [SerializeField]
    private Transform Hand;

    public Ball ball;

    // get from input manager
    private float PassShootAxis = 0;

    public bool canHold = true;

    // can hold timer
    public float canHoldTimer = 1;

    // Layer the player can pass on
    [SerializeField]
    private LayerMask HitLayer;

    // player's tag
    private string playerTag;

    // Reference fake ball
    [SerializeField]
    public GameObject FakeBall;

	// Use this for initialization
	void Start () {
        canHold = true;
        playerTag = transform.root.tag;
	}

	// Update is called once per frame
	void Update () {
        PassShootAxis = Input.GetAxis("PassShoot");
        //Debug.Log("Pass / Shoot Axis: " + PassShootAxis);

        //Debug.Log("ball = " + ball);
        if (ball != null)
        {
            if (!ball.GetThrown())
            {
                if (PassShootAxis < -0.1)
                {
                    // PASS
                    // Get Target from Targeting Script
                    Target = softLockScript.target;
                    TargetPosition = softLockScript.targetPosition;
                    if (Target != null)
                    {
                        Debug.Log(gameObject.name + " Passes");
                        CmdPass(Target, ball.gameObject);
                        ball = null;
                    }
                }
                else if (PassShootAxis > 0.1)
                {
                    // SHOOT
                    Debug.Log(gameObject.name + " Shoots");
                    CmdShoot(ball.gameObject);
                    ball = null;
                }
            }
        }

        if (!canHold)
        {
            if (canHoldTimer > 0)
                canHoldTimer -= Time.deltaTime;
            else if (canHoldTimer <= 0)
            {
                canHoldTimer = 1;
                canHold = true;
            }
        }
	}

    public Transform ReturnHand()
    {
        return Hand;
    }

    [Command]
    private void CmdPass(GameObject Target, GameObject ballObject)
    {
        RpcPass(Target, ballObject);
    }

    [ClientRpc]
    private void RpcPass(GameObject Target, GameObject ballObject)
    {
        ballObject.SetActive(true);
        Ball temp = ballObject.GetComponent<Ball>();
        temp.CmdSetPass(true, Target, PassForce);
        CmdTurnOnFakeBall(false);
    }

    [Command]
    private void CmdShoot(GameObject ballObject)
    {

        CmdTurnOnFakeBall(false);
        Direction = Cam.transform.forward;
        Direction *= ShootForce;
        RpcShoot(Direction, ballObject);
    }

    [ClientRpc]
    private void RpcShoot(Vector3 Direction, GameObject ballObject)
    {
        ballObject.SetActive(true);
        Ball temp = ballObject.GetComponent<Ball>();
        temp.CmdShoot(Direction, playerTag);
        CmdTurnOnFakeBall(false);
    }

    public void SetBall(Ball b)
    {
        ball = b;
    }

    [Command]
    public void CmdTurnOnFakeBall(bool b = true)
    {
        RpcTurnOnFakeBall(gameObject, b);
    }

    [ClientRpc]
    public void RpcTurnOnFakeBall(GameObject playerObject, bool b = true)
    {
        BallHandling bh = playerObject.GetComponent<BallHandling>();
        bh.FakeBall.SetActive(b);
    }
}
