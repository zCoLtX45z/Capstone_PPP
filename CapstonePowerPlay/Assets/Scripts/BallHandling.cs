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
    private GameObject FakeBall;

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
                    if (Target != null)
                    {
                        Debug.Log(gameObject.name + " Passes");
                        Pass(Target);
                        ball = null;
                    }
                }
                else if (PassShootAxis > 0.1)
                {
                    // SHOOT
                    Debug.Log(gameObject.name + " Shoots");
                    Shoot();
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
    private void CmdPass(GameObject Target)
    {
        ball.gameObject.SetActive(true);
        ball.Pass(true, Target, PassForce);
        TurnOnFakeBall(false);
    }

    [ClientRpc]
    private void RpcPass(GameObject Target)
    {
        //ball.gameObject.SetActive(true);
        ball.Pass(true, Target, PassForce);
        TurnOnFakeBall(false);
    }

    private void Shoot()
    {
        //RpcShoot();
        CmdShoot();
    }

    private void Pass(GameObject Target)
    {
        //RpcPass(Target);
        CmdPass(Target);
    }

    //private void Pass(GameObject Target)
    //{
    //    //Direction = Target.transform.position - ball.transform.position;
    //    //Direction = Direction.normalized;
    //    //Direction *= PassForce;

    //    ball.SetPass(true, Target, PassForce);
    //    TurnOnFakeBall(false);
    //    ball.gameObject.SetActive(true);
    //}

    [Command]
    private void CmdShoot()
    {
        Direction = Cam.transform.forward;
        Direction *= ShootForce;
        ball.gameObject.SetActive(true);
        ball.ShootBall(Direction, playerTag);

        TurnOnFakeBall(false);
    }

    [ClientRpc]
    private void RpcShoot()
    {
        Direction = Cam.transform.forward;
        Direction *= ShootForce;
        //ball.gameObject.SetActive(true);
        ball.ShootBall(Direction, playerTag);

        TurnOnFakeBall(false);
    }

    //    private void Shoot()
    //{
    //    /*
    //    RaycastHit hit;
    //    Ray ray = Cam.ScreenPointToRay(Input.mousePosition);
    //    Physics.Raycast(ray, out hit, HitLayer);
    //    Direction = hit.point;
    //    Direction = Direction - ball.transform.position;
    //    Direction = Direction.normalized;
    //    Direction *= ShootForce;
    //    ball.Shoot(Direction, playerTag);
    //    */

    //    Direction = Cam.transform.forward;
    //    Direction *= ShootForce;
    //    ball.Shoot(Direction, playerTag);

    //    Debug.Log("Direction: " + Direction);
    //    TurnOnFakeBall(false);
    //    ball.gameObject.SetActive(true);
    //}

    public void SetBall(Ball b)
    {
        ball = b;
    }

    public void TurnOnFakeBall(bool b = true)
    {
        FakeBall.SetActive(b);
    }
}
