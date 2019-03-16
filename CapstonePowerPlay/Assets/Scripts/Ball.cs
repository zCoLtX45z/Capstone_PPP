﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private Transform Handle;
    [SerializeField]
    public BallHandling BH;
    //OLD SYNC VAR
    public GameObject WhoTossedTheBall = null;
    [SerializeField]
    public Transform Hand;
    //OLD SYNC VAR
    public bool Held = false;
    private Rigidbody RB;

    public SphereCollider HardCol;

    private float timer = 0;

    [SerializeField]
    private bool isInPassing = false;


    private GameObject passedTarget;

    //[SerializeField]
    //private float RotSpeed = 0.5f;

    [SerializeField]
    private float maxDegree = 50;

    // Who threw the ball (based on tag string)
    public string teamTag;

    [SerializeField]
    private float KonstantForce = 900.0f;
    public float PickUpRadius = 9;

    private float CanBeCaughtTimer = 1;
    private bool Thrown = false;
    //private float SlerpRatio = 0;

    [SerializeField]
    private float maxTimePass = 2.0f;

    private float timePassTimer = 0.0f;

    // Physical Components
    [SerializeField]
    private GameObject ChildObject;
    [SerializeField]
    private SphereCollider SoftCol;

    //UI Elements
    [SerializeField]
    private RectTransform UiCanvas;

    //PHOTON VARIABLES
    private PhotonView PV;


    // Net Spawner Script
    private netSpawner nSpawner;

    public bool hasBeenPickedUpBefore;


    private RoundManager rTimerScript;

    [SerializeField]
    private bool inPlay = false;

    [SerializeField]
    private Animation animFloat = null;

    private float maxMass;
    [SerializeField]
    private float maxIncreaseAcceleration = 1;

    // Use this for initialization
    void Start()
    {
        PV = GetComponent<PhotonView>();
        Handle = GetComponent<Transform>();
        RB = GetComponent<Rigidbody>();

        maxMass = RB.mass;

        nSpawner = FindObjectOfType<netSpawner>();
        rTimerScript = FindObjectOfType<RoundManager>();
        SoftCol.radius = PickUpRadius;

        animFloat = transform.GetChild(0).GetComponent<Animation>();
    }

    private void Update()
    {
        Debug.Log("parent of ball: " + transform.parent);

        //if (PhotonNetwork.IsMasterClient)
        //{
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            Held = false;
        }

        if (RB.mass < maxMass)
        {
            RB.mass += Time.deltaTime / maxIncreaseAcceleration;
        }
        else if (RB.mass > maxMass)
        {
            RB.mass = maxMass;
        }

        if (Thrown)
        {




            if (BH != null || Hand != null || Held)
                ResetBall();
            CanBeCaughtTimer -= Time.deltaTime;
            if (CanBeCaughtTimer <= 0)
            {
                Thrown = false;
                CanBeCaughtTimer = 0.2f;
            }
        }

        if (isInPassing)
        {
            if (RB.useGravity)
                RB.useGravity = false;


            timePassTimer += Time.deltaTime;

            if (timePassTimer >= maxTimePass)
            {
                timePassTimer = 0;
                isInPassing = false;

                RB.useGravity = true;
            }

            Vector3 forwardVector = transform.forward;
            float lengthOfForwardV = forwardVector.magnitude;
            //Debug.Log("passTarget: " + passedTarget);

            //
            Vector3 posOffset = (new Vector3(passedTarget.transform.up.x, passedTarget.transform.up.y, passedTarget.transform.up.z) / 4) * 3;
            //

            float angle = Mathf.Acos(Vector3.Dot(transform.forward, ((passedTarget.transform.position + posOffset) - transform.position)) / (Mathf.Abs(lengthOfForwardV * ((passedTarget.transform.position + posOffset) - transform.position).magnitude)));

            angle *= 180 / Mathf.PI;

            angle = Mathf.Abs(angle);
            //Debug.Log("Within angle");
            //
            Vector3 lookPos = (passedTarget.transform.position + posOffset) - transform.position;
            Vector3 direction = lookPos.normalized;

            //var rotation = Quaternion.LookRotation(passedTarget.transform.position);
            //SlerpRatio = Time.deltaTime * RotSpeed;
            //
            // float angle = Vector3.Angle(directionFromPlayer, transform.forward);
            if (angle <= maxDegree)
            {



                //if (SlerpRatio > 1)
                //{
                //    SlerpRatio = 0;
                //    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1);
                //    isInPassing = false;
                //    RB.useGravity = true;
                //}
                //else
                //{
                //    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, SlerpRatio);
                //}

                //transform.rotation = rotation;
                //transform.LookAt(passedTarget);
                //RB.velocity = Vector3.zero;
                RB.AddForce(direction * KonstantForce, ForceMode.Force);

            }
            else
            {
                //transform.rotation = rotation;
                //transform.LookAt(passedTarget);
                //RB.velocity = Vector3.zero;
                RB.AddForce(direction * KonstantForce / 2, ForceMode.Force);
            }
            //RB.velocity = (transform.forward * constantForce);
        }

        if (!Held)
        {
            if (RB.useGravity == false)
            {
                if (!isInPassing)
                {
                    gameObject.layer = 10;
                    if (inPlay)
                        RB.useGravity = true;
                }
            }
        }

        if (Held && !Thrown)
        {
            transform.localPosition = Vector3.zero;
            RB.useGravity = false;

            if (RB.mass != maxMass)
            {
                RB.mass = maxMass;
            }
            //if (Hand != null)
            //    UiCanvas.position = Hand.transform.position;
        }
        else if (UiCanvas.localPosition != Vector3.zero)
        {
            UiCanvas.localPosition = Vector3.zero;
        }
        if (stolenInProgress)
        {
            //Debug.Log("stolenInProgress");
            if ((thiefTransform.position - transform.position).magnitude <= thiefCatchDistance)
            {
                //Debug.Log("witin range");
                CatchThief();
            }

        }
    }

    public void SlowDown(float Rate = 4)
    {
        RB.velocity = RB.velocity / Rate;
    }
    public void SendData(string Func, string Indentifier)
    {
        // Sett Who picked Up The Ball
        if (Func == "SetBall")
        {

        }

    }
    public bool stolenInProgress;
    public Transform thiefTransform;

    [SerializeField]
    private float thiefCatchDistance;


    private void CatchThief()
    {
        gameObject.layer = 2;
        HardCol.isTrigger = true;
        Held = true;

        // Set who has the the ball
        BH = thiefTransform.GetComponent<BallHandling>();
        SetBallHandling(BH.gameObject);

        if (BH.canHold)
        {
            Hand = BH.ReturnHand();
            UpdateHandTransform(BH.gameObject);
            BH.SetBall(gameObject);
            //BH.TurnOnFakeBall(true);
            //MakeBallDisapear();
        }
        else
        {
            BH = null;
            //Debug.Log("Can not hold catch thief");
        }
        // Debug.Log("End of catch thief");
        stolenInProgress = false;
        thiefTransform = null;
    }

    private void OnCollisionEnter(Collision c)
    {
        if (!inPlay)
        {
            inPlay = true;
            animFloat.enabled = false;
            transform.GetChild(0).transform.localPosition = Vector3.zero;
        }
        if (Thrown)
        {
            if (c.gameObject.tag == "Default" || c.gameObject.tag == "Ground")
            {
                isInPassing = false;
                RB.useGravity = true;
            }
        }
    }

    [PunRPC]
    private void RPC_OnTriggerEnter()
    {
        if (!inPlay)
        {
            inPlay = true;
            animFloat.enabled = false;
            transform.GetChild(0).transform.localPosition = Vector3.zero;
        }
        gameObject.layer = 2;
        HardCol.isTrigger = true;
        Held = true;
    }

    [PunRPC]
    private void RPC_SetPlayerBH(int Code)
    {
        //Debug.Log("RPC_SetPlayer Called");
        GameObject pc = PhotonView.Find(Code).gameObject;
        Debug.Log("PC: " + pc.name);
        BH = pc.GetComponent<BallHandling>();
        //SetBallHandling(BH.gameObject);

        //Debug.Log("BH: " + BH);
        if (BH.canHold)
        {
            BH.ball = this;
            //Debug.Log("BH Can Hold");
            Hand = BH.ReturnHand();
            transform.SetParent(Hand);
            Debug.Log("the parent is: " + transform.parent);
            transform.localPosition = Vector3.zero;
            BH.canHold = false;
            BH.canHoldTimer = 1;
            Held = true;
            RB.useGravity = false;
            RB.isKinematic = true;
            RB.detectCollisions = false;
            HardCol.isTrigger = true;
        }
        else
        {
            Debug.Log("Cannot hold");
            //Hand = null;
            //Held = false;
            //BH = null;
            //Debug.Log("BH Can't Hold");
        }


    }

    /*
    [PunRPC]
    private void RPC_SetHand(bool set = true)
    {
        if (BH != null)
        {
            if (set)
            {
                RB.useGravity = false;
                RB.isKinematic = true;
                RB.detectCollisions = false;
                //Debug.Log("Hand Set");
                transform.SetParent(BH.ReturnHand());
                Debug.Log("the parent is: " + transform.parent);
                BH.SetBall(this.gameObject);
            }
            else
            {
                Debug.Log("Hand UnSet");
                transform.SetParent(null);
                BH.SetBall(null);
            }
        }
        else
        {
            Debug.Log("no BH");
            transform.SetParent(null);
        }
    }
    */
    public void PickUp(GameObject c)
    {
        if (!hasBeenPickedUpBefore)
        {
            //nSpawner.CallMoveNetUp();
            rTimerScript.BeginCountdown();
        }

        PV.RPC("RPC_OnTriggerEnter", RpcTarget.All);

        // Set who has the the ball
        PlayerColor pc = c.GetComponent<PlayerColor>();
        // Every persons code has to be shared
        //string PlayerCode = pc.GetCode();
        PV.RPC("RPC_SetPlayerBH", RpcTarget.AllViaServer, c.GetPhotonView().ViewID);
    }
    //private void OnTriggerEnter(Collider c)
    //{
    //    //Debug.Log("triged: " + c.name + " tag: " + c.tag);
    //    if ((c.tag == "Team 1" || c.tag == "Team 2") && !Held && !Thrown)
    //    {
    //        //if(!PV.IsMine)
    //        //{
    //        //    PV.TransferOwnership(c.gameObject.GetPhotonView().ViewID);
    //        //    Debug.Log("transfering ownership");
    //        //}

    //        if (!hasBeenPickedUpBefore)
    //        {
    //            //Debug.Log("has not been will be");




    //            //if (PhotonNetwork.IsMasterClient)
    //            //{
    //            nSpawner.CallMoveNetUp();
    //            rTimerScript.BeginCountdown();
    //            //Debug.Log("IsMasterCilent");

    //            // }
    //        }


    //        PV.RPC("RPC_OnTriggerEnter", RpcTarget.All);

    //        //c.GetComponent<PhotonView>().TransferOwnership(GetComponent<PhotonView>()..ID);


    //        // Set who has the the ball
    //        PlayerColor pc = c.GetComponent<PlayerColor>();
    //        // Every persons code has to be shared
    //        string PlayerCode = pc.GetCode();
    //        PV.RPC("RPC_SetPlayerBH", RpcTarget.AllViaServer, PlayerCode);
    //        //transform.gameObject.layer = 2;

    //        //if (BH.canHold )
    //        //{
    //        //    Debug.Log("BH can hold");
    //        //    //MakeBallDisapear();
    //        //    //Hand = BH.ReturnHand();
    //        //    //UpdateHandTransform(BH.gameObject);
    //        //    //PV.RPC("RPC_SetHand", RpcTarget.All);
    //        //    //Handle.position = Hand.position;
    //        //    //Handle.parent = Hand.parent;

    //        //    //BH.SetBall(gameObject);
    //        //    //RBS.CmdSetPlayerHolding(BH.gameObject);
    //        //    //BH.TurnOnFakeBall(true);
    //        //    //CmdTurnOnBall(false);
    //        //}
    //        //else
    //        //{ 
    //        //    BH = null;
    //        //    Debug.Log("can not hold tag of team");
    //        //}
    //    }
    //}

    private void OnTriggerExit(Collider other)
    {
        if (!stolenInProgress && (other.tag == "Team 1" || other.tag == "Team 2"))
        {
            PV.RPC("RPC_OnTriggerExit", RpcTarget.All);
        }
    }

    [PunRPC]
    private void RPC_OnTriggerExit()
    {
        if (!inPlay)
        {
            inPlay = true;
            animFloat.enabled = false;
            transform.GetChild(0).transform.localPosition = Vector3.zero;
        }
        //Debug.Log("PLAYER HAS LEFT THE AREA!!!");
        HardCol.isTrigger = false;
        timer = 1;
    }

    public void ShootBall(Vector3 power, string tag, Vector3 HandPos, GameObject WhoThrew)
    {
        Shoot(power, tag, HandPos, WhoThrew);
    }


    public void Shoot(Vector3 power, string tag, Vector3 HandPos, GameObject WhoThrew)
    {
        //transform.gameObject.layer = 0;
        //RpcShoot(power, tag, HandPos, WhoThrew);
        PV.RPC("RPC_Shoot", RpcTarget.All, power, tag, HandPos, WhoThrew.GetPhotonView().ViewID);
    }

    [PunRPC]
    public void RPC_Shoot(Vector3 power, string tag, Vector3 HandPos, int WhoThrew)
    {
        //transform.gameObject.layer = 0;
        //Debug.Log("unParetning Ball shoot. old parent: " + transform.parent);
        //Debug.Log("Hand: " + Hand);
        //transform.GetComponentInParent<BallHandling>().ReturnHand().DetachChildren();
        RB.mass = 5;
        //Hand.DetachChildren();
        transform.SetParent(null);
        Thrown = true;
        CanBeCaughtTimer = 0.15f;
        //Handle.position = HandPos;
        //Debug.Log("power is " + power);
        RB.useGravity = false;
        RB.isKinematic = false;
        RB.detectCollisions = true;
        RB.velocity = Vector3.zero;
        RB.angularVelocity = Vector3.zero;
        transform.position = HandPos;
        RB.AddForce(power, ForceMode.Impulse);
        //Debug.Log("teamTag: " + tag);
        teamTag = tag;
        gameObject.layer = 10;
        Held = false;
        WhoTossedTheBall = PhotonView.Find(WhoThrew).gameObject;
        Hand = null;
        BH = null;
        HardCol.isTrigger = false;

        //MakeBallReapear();
        //RBS.CmdSetPlayerHolding(null);
    }


    public void SetPass(bool Passing, GameObject Target, float Force, Vector3 HandPos, GameObject WhoThrew)
    {
        //RPC_SetPass(Passing, Target.GetPhotonView().ViewID, Force, HandPos, WhoThrew.GetPhotonView().ViewID);
        PV.RPC("RPC_SetPass", RpcTarget.All, Passing, Target.GetPhotonView().ViewID, Force, HandPos, WhoThrew.GetPhotonView().ViewID);
    }

    [PunRPC]
    public void RPC_SetPass(bool Passing, int Target, float Force, Vector3 HandPos, int WhoThrew)
    {
        transform.SetParent(null);
        Thrown = true;
        CanBeCaughtTimer = 0.15f;
        passedTarget = PhotonView.Find(Target).gameObject;
        //Debug.Log("unParetning Ball shoot. old parent: " + transform.parent);
        //Handle.position = HandPos;
        isInPassing = true;
        RB.useGravity = false;
        RB.isKinematic = false;
        RB.detectCollisions = true;
        transform.position = HandPos;
        //Debug.Log("about to pass");
        Held = false;
        WhoTossedTheBall = PhotonView.Find(WhoThrew).gameObject;
        Hand = null;
        BH = null;
        HardCol.isTrigger = false;
        RB.velocity = Vector3.zero;
        RB.angularVelocity = Vector3.zero;
        transform.LookAt(PhotonView.Find(Target).gameObject.transform);
        RB.AddForce(Force * transform.forward, ForceMode.Impulse);
        teamTag = tag;
        gameObject.layer = 10;
        //RB.AddForce(transform.up * Force, ForceMode.Impulse);
        //Debug.Log("pass force applied");

        //Debug.Log("can not hold pass");
        //MakeBallReapear();
        //RBS.CmdSetPlayerHolding(null);
    }





    public void SetSteal(bool Passing, GameObject Target, float Force, Vector3 HandPos, GameObject WhoThrew)
    {
        //RpcSetPass(Passing, Target, Force, HandPos, WhoThrew);
        PV.RPC("RPC_SetSteal", RpcTarget.All, Passing, Target.GetPhotonView().ViewID, Force, HandPos, WhoThrew.GetPhotonView().ViewID);
    }

    [PunRPC]
    public void RPC_SetSteal(bool Passing, int Target, float Force, Vector3 HandPos, int WhoThrew)
    {
        //MakeBallReapear();
        Thrown = true;
        ResetBall();
        passedTarget = PhotonView.Find(Target).gameObject;
        transform.parent = null;
        //Handle.position = HandPos;
        Handle.parent = null;
        isInPassing = true;
        RB.velocity = Vector3.zero;
        RB.angularVelocity = Vector3.zero;
        float distance = (transform.position - PhotonView.Find(Target).gameObject.transform.position).magnitude;
        transform.LookAt(PhotonView.Find(Target).gameObject.transform);
        RB.AddForce(transform.up * Force, ForceMode.Impulse);
        Held = false;
        WhoTossedTheBall = PhotonView.Find(WhoThrew).gameObject;
        Hand = null;
        BH = null;
        //Debug.Log("can not hold steal");
    }




    public bool GetThrown()
    {
        return Thrown;
    }


    public void TurnOnBall(bool b)
    {
        //RpcTurnOnBall(b);
        PV.RPC("RPC_TurnOnBall", RpcTarget.All, b);
    }

    [PunRPC]
    public void RPC_TurnOnBall(bool b)
    {
        gameObject.SetActive(b);
    }




    private void UpdateHandTransform(GameObject HandParent)
    {
        //Debug.Log("running update hand transform");
        //RPC_UpdateHandTransform(HandParent.GetPhotonView().ViewID);
        PV.RPC("RPC_UpdateHandTransform", RpcTarget.All, HandParent.GetPhotonView().ViewID);
        //Debug.Log("finished running update hand transform");
    }

    [PunRPC]
    private void RPC_UpdateHandTransform(int HandParent)
    {
        BallHandling bh = PhotonView.Find(HandParent).gameObject.GetComponent<BallHandling>();
        Hand = bh.ReturnHand();
    }




    private void SetBallHandling(GameObject bhObject)
    {
        //RPC_SetBallHandling(bhObject.GetPhotonView().ViewID);
        PV.RPC("RPC_SetBallHandling", RpcTarget.All, bhObject.GetPhotonView().ViewID);
    }

    [PunRPC]
    private void RPC_SetBallHandling(int bhObject)
    {
        BH = PhotonView.Find(bhObject).gameObject.GetComponent<BallHandling>();
    }

    public void UpdateBH(BallHandling bh)
    {
        BH = bh;
        PlayerColor pc = BH.GetComponent<PlayerColor>();
        PV.RPC("RPC_UpdateBH", RpcTarget.All, pc.GetCode());
    }

    [PunRPC]
    public void RPC_UpdateBH(string Code)
    {
        PlayerColor[] Players = FindObjectsOfType<PlayerColor>();
        foreach (PlayerColor pc in Players)
        {
            if (pc.GetCode() == Code)
            {
                BH = pc.GetComponent<BallHandling>();
                break;
            }
        }
    }

    public void UpdateHand(BallHandling bh)
    {
        BH = bh;
        PlayerColor pc = BH.GetComponent<PlayerColor>();
        PV.RPC("RPC_UpdateHand", RpcTarget.All, pc.GetCode());
        //RPC_UpdateHand(pc.GetCode());
    }

    [PunRPC]
    public void RPC_UpdateHand(string Code)
    {
        PlayerColor[] Players = FindObjectsOfType<PlayerColor>();
        foreach (PlayerColor pc in Players)
        {
            if (pc.GetCode() == Code)
            {
                Hand = pc.GetComponent<BallHandling>().ReturnHand();
                transform.SetParent(Hand);
                //Debug.Log("the parent is: " + transform.parent);
                transform.localPosition = Vector3.zero;
                break;
            }
        }
    }

    public void UpdateHeld(bool IsHeld)
    {
        PV.RPC("RPC_UpdateHeld", RpcTarget.All, IsHeld);
    }

    [PunRPC]
    public void RPC_UpdateHeld(bool IsHeld)
    {
        Held = IsHeld;
    }

    public void ResetBall()
    {
        PV.RPC("RPC_ResetBall", RpcTarget.All);
    }

    [PunRPC]
    public void RPC_ResetBall()
    {
        print("Ball Reset");
        if (BH != null)
            BH.ReturnHand().DetachChildren();
        Held = false;
        BH = null;
        Hand = null;
        transform.parent = null;
        Debug.Log("unParetning Ball shoot. old parent: " + transform.parent);
        transform.SetParent(null);
    }

    /*
    public void DropBall()
    {
        PV.RPC("RPC_DropBall", RpcTarget.All);
    }

    [PunRPC]
    public void RPC_DropBall()
    {
        transform.SetParent(null);
        CanBeCaughtTimer = 0.15f;
       
        RB.useGravity = true;
        RB.isKinematic = false;
        RB.detectCollisions = true;

        Held = false;
        Hand = null;
        BH = null;
        HardCol.isTrigger = false;
        teamTag = "Ball";
        gameObject.layer = 10;


    }
    */
}
