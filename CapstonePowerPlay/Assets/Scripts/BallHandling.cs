using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHandling : MonoBehaviour {

    // Set Shoot force 
    [SerializeField]
    public float ShootForce;
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

    private Ball ball;

    // get from input manager
    private float PassShootAxis = 0;

    public bool canHold;

    // can hold timer
    public float canHoldTimer = 0;

    // Layer the player can pass on
    [SerializeField]
    private LayerMask HitLayer;
	// Use this for initialization
	void Start () {
        canHold = true;
	}
	
	// Update is called once per frame
	void Update () {
        PassShootAxis = Input.GetAxis("PassShoot");
        Debug.Log("Pass / Shoot Axis: " + PassShootAxis);

        if (ball != null)
        {
            if (PassShootAxis < -0.1)
            {
                // PASS
                // Get Target from Targeting Script
                Target = softLockScript.target;
                Pass(Target);

            }
            else if (PassShootAxis > 0.1)
            {
                // SHOOT
                Shoot();
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

    private void Pass(GameObject Target)
    {
        //Direction = Target.transform.position - ball.transform.position;
        //Direction = Direction.normalized;
        //Direction *= PassForce;
        ball.SetPass(true, Target, PassForce);
    }

    private void Shoot()
    {
        RaycastHit hit;
        Ray ray = Cam.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit, HitLayer);
        Direction = hit.point;
        Direction = Direction - ball.transform.position;
        Direction = Direction.normalized;
        Direction *= ShootForce;
        ball.Shoot(Direction);
    }

    public void SetBall(Ball b)
    {
        ball = b;
    }
}
