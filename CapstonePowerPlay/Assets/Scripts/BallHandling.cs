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
    //private Vector3 TargetPosition;

    // Hand to where the ball goes
    [SerializeField]
    private Transform Hand;

    public Ball ball;
    private Ball FindBall;

    // get from input manager
    private float PassShootAxis = 0;

    [SyncVar]
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

    [SerializeField]
    private PlayerColor PC;

    // Animator
    [SerializeField]
    private AnimationController AnimationControl;

    // Player has control
    [HideInInspector]
    public bool HasControl = true;

    // Use this for initialization
    void Start () {
        canHold = true;
        playerTag = transform.root.tag;
	}

	// Update is called once per frame
	void Update () {
        if (PC.LocalPlayer == PC.ParentPlayer && HasControl)
        {
            if(ball != null)
            {
                ball.CmdMakeBallDisapear();
                ball.BH = this;
                ball.Hand = Hand;
                ball.Held = true;
            }


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
                        Target = softLockScript.target.gameObject;
                        //Debug.Log("target: " + Target);
                        //TargetPosition = softLockScript.targetPosition;
                        if (Target != null)
                        {
                            //Debug.Log("jadaadadadadadad");
                            //Debug.Log(gameObject.name + " Passes");
                          //  Debug.Log("target: " + Target.name);
                            CmdPass(Target, ball.gameObject, Hand.position, this.gameObject);
                            AnimationControl.CmdPassAnimation();
                            ball = null;
                        }
                    }
                    else if (PassShootAxis > 0.1)
                    {
                        // SHOOT
                        //Debug.Log(gameObject.name + " Shoots");
                        //RaycastHit RH;
                        Vector3 direction = Cam.transform.position + Cam.transform.forward * 100 - Hand.position;
                        //if(Physics.Raycast(Cam.transform.position, Cam.transform.forward, out RH, 100, HitLayer))
                        //{
                        //    direction = RH.collider.
                        //}
                        //else
                        //{

                        //}
                        //Debug.DrawLine(Cam.transform.position + Cam.transform.forward * 100, Hand.position, Color.blue, 6f);
                        //Debug.DrawRay(Hand.position, Cam.transform.position + Cam.transform.forward * 100 - Hand.position, Color.red, 3.75f);
                        CmdShoot(ball.gameObject, Hand.position, direction.normalized * ShootForce, this.gameObject);
                        AnimationControl.CmdPassAnimation();
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
        else
        {
            // nothing for now
        }
	}

    public void SetHand(Transform T)
    {
        Hand = T;
    }

    public Transform ReturnHand()
    {
        return Hand;
    }

    [Command]
    private void CmdPass(GameObject Target, GameObject ballObject, Vector3 HandPos, GameObject WhoThrew)
    {
        RpcPass(Target, ballObject, HandPos, WhoThrew);
    }

    [ClientRpc]
    private void RpcPass(GameObject Target, GameObject ballObject, Vector3 HandPos, GameObject WhoThrew)
    {
        
        Ball temp = ballObject.GetComponent<Ball>();
        temp.CmdSetPass(true, Target, PassForce, HandPos, WhoThrew);
        CmdTurnOnFakeBall(false);
    }

    [Command]
    private void CmdShoot(GameObject ballObject, Vector3 HandPos, Vector3 Direction, GameObject WhoThrew)
    {
        CmdTurnOnFakeBall(false);
        RpcShoot(Direction, ballObject, HandPos, WhoThrew);
    }

    [ClientRpc]
    private void RpcShoot(Vector3 Direction, GameObject ballObject, Vector3 HandPos, GameObject WhoThrew)
    {
        Ball temp = ballObject.GetComponent<Ball>();
        temp.CmdShoot(Direction, playerTag, HandPos, WhoThrew);
        CmdTurnOnFakeBall(false);
    }

    [Command]
    public void CmdSetBall(GameObject ballObject)
    {
        RpcSetBall(ballObject);
    }

    [ClientRpc]
    public void RpcSetBall(GameObject ballObjectb)
    {
        Ball b = ballObjectb.GetComponent<Ball>();
        ball = b;
    }

    [Command]
    public void CmdTurnOnFakeBall(bool b)
    {
        RpcTurnOnFakeBall(gameObject, b);
    }

    [ClientRpc]
    public void RpcTurnOnFakeBall(GameObject playerObject, bool b)
    {
        BallHandling bh = playerObject.GetComponent<BallHandling>();
        bh.FakeBall.SetActive(b);
    }






    [Command]
    public void CmdSteal(GameObject TargetHand, GameObject ballObject, Vector3 HandPos, GameObject WhoThrew)
    {
        //Debug.Log("CmdSteal");
        RpcSteal(TargetHand, ballObject, HandPos, WhoThrew);
    }

    [ClientRpc]
    private void RpcSteal(GameObject TargetHand, GameObject ballObject, Vector3 HandPos, GameObject WhoThrew)
    {
        //Debug.Log("RpcSteal");
        Ball temp = ballObject.GetComponent<Ball>();
        temp.thiefTransform = TargetHand.transform;
        temp.stolenInProgress = true;
        temp.CmdSetSteal(true, TargetHand, PassForce, HandPos, WhoThrew);
        CmdTurnOnFakeBall(false);
    }

    [Command]
    public void CmdDropBall()
    {
        if (ball != null)
        {
            float randomX = Random.Range(-1f, 1f);
            float randomZ = Random.Range(-1f, 1f);
            Vector3 DropPower = new Vector3(randomX, 1, randomZ) * ShootForce * 0.1f;
            CmdShoot(ball.gameObject, Hand.position, DropPower, this.gameObject);
        }
    }
}
