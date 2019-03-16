using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallHandling : MonoBehaviour {

    private PlayerVoiceLine PVL;

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
    private float FindBallDistance = 10;

    // get from input manager
    private float PassShootAxis = 0;

    //OLD SYNCVAR
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

    //effects
    public ParticleSystem PassEffectBlue;
    public ParticleSystem PassEffectRed;

    //photon variables
    private PhotonView PV;
    // Use this for initialization
    void Start() {
        PVL = FindObjectOfType<PlayerVoiceLine>();
        canHold = true;
        playerTag = transform.root.tag;
        PV = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update() {
        if (PC.LocalPlayer == PC.ParentPlayer && HasControl)
        {
            //if (ball != null)
            //{
            //    if (ball.BH != this)
            //    {
            //        //ball.MakeBallDisapear();
            //        ball.BH = this;
            //        ball.UpdateBH(this);
            //    }
            //    else if (ball.Hand != Hand)
            //    {
            //        ball.Hand = Hand;
            //        ball.UpdateHand(this);
            //    }
            //    else
            //    {
            //        ball.Held = true;
            //        ball.UpdateHeld(true);
            //    }
            //}


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
                        if (softLockScript.target != null)
                            Target = softLockScript.target.gameObject;
                        //Debug.Log("target: " + Target);
                        //TargetPosition = softLockScript.targetPosition;
                        if (Target != null)
                        {
                            //Debug.Log("jadaadadadadadad");
                            //Debug.Log(gameObject.name + " Passes");
                            //  Debug.Log("target: " + Target.name);
                            Pass(Target, ball.gameObject, Hand.position, this.gameObject);
                            AnimationControl.PassAnimation();
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
                        Shoot(ball.gameObject, Hand.position, direction.normalized * ShootForce, this.gameObject);
                        AnimationControl.PassAnimation();
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

            if (FindBall == null)
            {
                FindBall = FindObjectOfType<Ball>();
            }
            else
            {
                if (!FindBall.Held)
                {
                    if (!FindBall.GetThrown())
                    {
                        FindBallDistance = (FindBall.transform.position - transform.position).magnitude;
                        if (FindBallDistance <= FindBall.PickUpRadius)
                        {
                            PVL.PlayBallPickup();
                            FindBall.PickUp(gameObject);
                        }
                    }
                }
            }
        }
        else if (PC.LocalPlayer != PC.ParentPlayer)
        {
            if (FindBall == null)
            {
                FindBall = FindObjectOfType<Ball>();
            }
            else
            {
                if (!FindBall.Held)
                {
                    if (!FindBall.GetThrown())
                    {
                        FindBallDistance = (FindBall.transform.position - transform.position).magnitude;
                        if (FindBallDistance <= FindBall.PickUpRadius)
                        {
                            FindBall.SlowDown();
                        }
                    }
                }
            }
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

    private void Pass(GameObject Target, GameObject ballObject, Vector3 HandPos, GameObject WhoThrew)
    {
        PVL.PlayShoot();
        PV.RPC("RPC_PlayPassEffect", RpcTarget.All);
        //RpcPass(Target, ballObject, HandPos, WhoThrew);
        RPC_Pass(Target.GetPhotonView().ViewID, ballObject.GetPhotonView().ViewID, HandPos, WhoThrew.GetPhotonView().ViewID);
        //PV.RPC("RPC_Pass", RpcTarget.AllBuffered, Target.GetPhotonView().ViewID, ballObject.GetPhotonView().ViewID, HandPos, WhoThrew.GetPhotonView().ViewID);
    }

    //[PunRPC]
    private void RPC_Pass(int Target, int ballObject, Vector3 HandPos, int WhoThrew)
    {

        Ball temp = PhotonView.Find(ballObject).gameObject.GetComponent<Ball>();
        temp.SetPass(true, PhotonView.Find(Target).gameObject, PassForce, HandPos, PhotonView.Find(WhoThrew).gameObject);
        //TurnOnFakeBall(false);
    }

    
    private void Shoot(GameObject ballObject, Vector3 HandPos, Vector3 Direction, GameObject WhoThrew)
    {
        PVL.PlayShoot();
        //TurnOnFakeBall(false);
        //PV.RPC("RPC_PlayPassEffect", RpcTarget.All);
        RPC_Shoot(Direction, ballObject.GetPhotonView().ViewID, HandPos, WhoThrew.GetPhotonView().ViewID);

        //Debug.Log("Direction: " + Direction + " ballObject: " + ballObject + " HandPos: " + HandPos + " WhoThrew: " + WhoThrew);
        //Debug.Log("PV: " + PV);

        //PV.RPC("RPC_Shoot", RpcTarget.All, Direction, ballObject.GetPhotonView().ViewID, HandPos, WhoThrew.GetPhotonView().ViewID);
    }

    //[PunRPC]
    private void RPC_Shoot(Vector3 Direction, int ballObject, Vector3 HandPos, int WhoThrew)
    {
        Ball temp = PhotonView.Find(ballObject).gameObject.GetComponent<Ball>();
        temp.Shoot(Direction, playerTag, HandPos, PhotonView.Find(WhoThrew).gameObject);
        //TurnOnFakeBall(false);
    }

    
    public void SetBall(GameObject ballObject)
    {
        //RpcSetBall(ballObject);
        PV.RPC("RPC_SetBall", RpcTarget.All, ballObject.GetPhotonView().ViewID);
    }

    [PunRPC]
    public void RPC_SetBall(int ballObjectb)
    {
        Ball b = PhotonView.Find(ballObjectb).GetComponent<Ball>();
        ball = b;
    }

    
    //public void TurnOnFakeBall(bool b)
    //{
    //    //RpcTurnOnFakeBall(gameObject, b);
    //    PV.RPC("RPC_TurnOnFakeBall", RpcTarget.All, gameObject.GetPhotonView().ViewID, b);
    //}

    //[PunRPC]
    //public void RPC_TurnOnFakeBall(int playerObject, bool b)
    //{
    //    BallHandling bh = PhotonView.Find(playerObject).gameObject.GetComponent<BallHandling>();
    //    bh.FakeBall.SetActive(b);
    //}
    /// <summary>
    /// /passing effects
    /// </summary>
    [PunRPC]
    public void RPC_PlayPassEffect()
    {
        if (PC.LocalPlayer == PC.ParentPlayer)
        {
            if (PassEffectBlue != null)
                PassEffectBlue.Play();
        }
        else
        {
            if (PC.LocalPlayer.GetTeamNum() == PC.ParentPlayer.GetTeamNum())
            {
                if (PassEffectBlue != null)
                    PassEffectBlue.Play();
            }
            else
            {
                if (PassEffectRed != null)
                    PassEffectRed.Play();
            }
        }
    }
    
    public void Steal(GameObject TargetHand, GameObject ballObject, Vector3 HandPos, GameObject WhoThrew)
    {
        //Debug.Log("CmdSteal");
        //RpcSteal(TargetHand, ballObject, HandPos, WhoThrew);
        PV.RPC("RPC_Steal", RpcTarget.All, TargetHand.GetPhotonView().ViewID, ballObject.GetPhotonView().ViewID, HandPos, WhoThrew.GetPhotonView().ViewID);
    }

    [PunRPC]
    private void RPC_Steal(int TargetHand, int ballObject, Vector3 HandPos, int WhoThrew)
    {
        //Debug.Log("RpcSteal");
        Ball temp = (PhotonView.Find(ballObject).gameObject.GetComponent<Ball>());
        temp.thiefTransform = PhotonView.Find(TargetHand).gameObject.transform;
        temp.stolenInProgress = true;
        temp.SetSteal(true, PhotonView.Find(TargetHand).gameObject, PassForce, HandPos, PhotonView.Find(WhoThrew).gameObject);
        //TurnOnFakeBall(false);
    }

    
    public void DropBall()
    {
        if (ball != null)
        {
            float randomX = Random.Range(-1f, 1f);
            float randomZ = Random.Range(-1f, 1f);
            Vector3 DropPower = new Vector3(randomX, 1, randomZ) * ShootForce * 0.1f;
            Shoot(ball.gameObject, Hand.position, DropPower, this.gameObject);
        }
    }
}
