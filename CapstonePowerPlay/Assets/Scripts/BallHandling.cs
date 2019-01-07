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

    public bool canHold;

    // can hold timer
    public float canHoldTimer = 0;

    // Layer the player can pass on
    [SerializeField]
    private LayerMask HitLayer;

    // player's tag
    private string playerTag;

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
            if (PassShootAxis < -0.1)
            {
                // PASS
                // Get Target from Targeting Script
                Target = softLockScript.target;
                if (Target != null)
                {
                    CmdPass(Target);
                    Debug.Log(gameObject.name + " Passes");
                    ball = null;
                }
            }
            else if (PassShootAxis > 0.1)
            {
                // SHOOT
                CmdShoot();
                Debug.Log(gameObject.name + " Shoots");
                ball = null;
            }
        }

        if (!canHold)
        {
            if (canHoldTimer > 0)
                canHoldTimer -= Time.deltaTime;
            else if (canHoldTimer <= 0)
            {
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
        ball.SetPass(true, Target, PassForce);
    }

    private void Pass(GameObject Target)
    {
        //Direction = Target.transform.position - ball.transform.position;
        //Direction = Direction.normalized;
        //Direction *= PassForce;
        
        ball.SetPass(true, Target, PassForce);
    }

    [Command]
    private void CmdShoot()
    {
        Direction = Cam.transform.forward;
        Direction *= ShootForce;
        ball.Shoot(Direction, playerTag);
    }

        private void Shoot()
    {
        /*
        RaycastHit hit;
        Ray ray = Cam.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit, HitLayer);
        Direction = hit.point;
        Direction = Direction - ball.transform.position;
        Direction = Direction.normalized;
        Direction *= ShootForce;
        ball.Shoot(Direction, playerTag);
        */
        
        Direction = Cam.transform.forward;
        Direction *= ShootForce;
        ball.Shoot(Direction, playerTag);

        //Debug.Log("Direction: " + Direction);

    }

    public void SetBall(Ball b)
    {
        ball = b;
    }
}
