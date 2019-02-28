using Photon.Pun;
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
    //particle system
    [SerializeField]
    private ParticleSystem.EmissionModule BallTrail;

    //PHOTON VARIABLES
    private PhotonView PV;

    // Use this for initialization
    void Start ()
    {
        PV = GetComponent<PhotonView>();
        Handle = GetComponent<Transform>();
        RB = GetComponent<Rigidbody>();
        BallTrail = GetComponentInChildren<ParticleSystem.EmissionModule>();

    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            Held = false;
        }

        if (Thrown)
        {
            CanBeCaughtTimer -= Time.deltaTime;
            if (CanBeCaughtTimer <= 0)
            {
                CmdPlayTrail();
                Thrown = false;
                CanBeCaughtTimer = 0.1f;
            }
        }

        if(isInPassing)
        {
            if (RB.useGravity)
                RB.useGravity = false;


            timePassTimer += Time.deltaTime;

            if(timePassTimer >= maxTimePass)
            {
                timePassTimer = 0;
                isInPassing = false;

                RB.useGravity = true;
            }

            Vector3 forwardVector = transform.forward;
            float lengthOfForwardV = forwardVector.magnitude;
            Debug.Log("passTarget: " + passedTarget);

            //
            Vector3 posOffset = (new Vector3(passedTarget.transform.up.x, passedTarget.transform.up.y, passedTarget.transform.up.z) / 4) * 3;
            //

            float angle = Mathf.Acos(Vector3.Dot(transform.forward, ((passedTarget.transform.position + posOffset) - transform.position))/(Mathf.Abs(lengthOfForwardV * ((passedTarget.transform.position + posOffset) - transform.position).magnitude)));

            angle *= 180 / Mathf.PI;

            angle = Mathf.Abs(angle);
            Debug.Log("Within angle");
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
                RB.velocity = Vector3.zero;
                RB.AddForce(direction * KonstantForce, ForceMode.Acceleration);

            }
            else
            {
                //transform.rotation = rotation;
                //transform.LookAt(passedTarget);
                RB.velocity = Vector3.zero;
                RB.AddForce(direction * KonstantForce / 2, ForceMode.Acceleration);
            }
            //RB.velocity = (transform.forward * constantForce);
        }
        
        if (RB.useGravity == false)
        { 
            if (!isInPassing)
            {
                gameObject.layer = 10;
                RB.useGravity = true;
            }
        }

        if (Held)
        {
            if (Hand != null)
                UiCanvas.position = Hand.transform.position;
        }
        else if (UiCanvas.localPosition != Vector3.zero)
        {
            UiCanvas.localPosition = Vector3.zero;
        }


        if(stolenInProgress)
        {
            Debug.Log("stolenInProgress");
            if((thiefTransform.position - transform.position).magnitude <= thiefCatchDistance)
            {
                Debug.Log("witin range");
                CatchThief();
            }

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
            BH.TurnOnFakeBall(true);
            MakeBallDisapear();
        }
        else
        {
            BH = null;
        }
       // Debug.Log("End of catch thief");
        stolenInProgress = false;
        thiefTransform = null;
    }




    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "Default")
        {
            isInPassing = false;
            RB.useGravity = true;
        }
    }
    private void OnTriggerEnter(Collider c)
    {
        //Debug.Log("triged: " + c.name + " tag: " + c.tag);
        if((c.tag == "Team 1" || c.tag == "Team 2") && !Held && !Thrown)
        {
            CmdStopTrail();
            //Debug.Log("PLAYER HAS ENTERED THE AREA!!!1");
            gameObject.layer = 2;
            HardCol.isTrigger = true;
            Held = true;

            // Set who has the the ball
            BH = c.GetComponent<BallHandling>();
            SetBallHandling(BH.gameObject);
            //transform.gameObject.layer = 2;


            if (BH.canHold )
            {
                MakeBallDisapear();
                Hand = BH.ReturnHand();
                UpdateHandTransform(BH.gameObject);
                //Handle.position = Hand.position;
                //Handle.parent = Hand.parent;

                BH.SetBall(gameObject);
                //RBS.CmdSetPlayerHolding(BH.gameObject);
                BH.TurnOnFakeBall(true);
                //CmdTurnOnBall(false);
            }
            else
            { 
                BH = null;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!stolenInProgress)
        {
            timer = 1;
            BH = null;
            HardCol.isTrigger = false;
        }
    }

    public void ShootBall(Vector3 power, string tag, Vector3 HandPos, GameObject WhoThrew)
    {
        Shoot(power, tag, HandPos, WhoThrew);
    }

    
    public void Shoot(Vector3 power, string tag, Vector3 HandPos, GameObject WhoThrew)
    {
        //transform.gameObject.layer = 0;
        //RpcShoot(power, tag, HandPos, WhoThrew);
        PV.RPC("RPC_Shoot", RpcTarget.All, power, tag, HandPos, WhoThrew);
    }

    [PunRPC]
    public void RPC_Shoot(Vector3 power, string tag, Vector3 HandPos, GameObject WhoThrew)
    {
        //transform.gameObject.layer = 0;
        Thrown = true;
        CanBeCaughtTimer = 0.15f;
        Handle.position = HandPos;
        //Debug.Log("power is " + power);
        RB.useGravity = false;
        RB.isKinematic = false;
        RB.velocity = Vector3.zero;
        RB.angularVelocity = Vector3.zero;
        RB.AddForce(power, ForceMode.Impulse);
        //Debug.Log("teamTag: " + tag);
        teamTag = tag;
        gameObject.layer = 10;
        Held = false;
        WhoTossedTheBall = WhoThrew;
        Hand = null;
        BH = null;
        MakeBallReapear();
        //RBS.CmdSetPlayerHolding(null);
    }

    
    public void SetPass(bool Passing, GameObject Target, float Force, Vector3 HandPos, GameObject WhoThrew)
    {
        //RpcSetPass(Passing, Target, Force, HandPos, WhoThrew);
        PV.RPC("RPC_SetPass", RpcTarget.All, Passing, Target, Force, HandPos, WhoThrew);
    }

    [PunRPC]
    public void RPC_SetPass(bool Passing, GameObject Target, float Force, Vector3 HandPos, GameObject WhoThrew)
    {
        Thrown = true;

        CanBeCaughtTimer = 0.15f;
        passedTarget = Target;
        Handle.position = HandPos;
        Handle.parent = null;
        isInPassing = true;
        RB.velocity = Vector3.zero;
        RB.angularVelocity = Vector3.zero;
        float distance = (transform.position - Target.transform.position).magnitude;
        transform.LookAt(Target.transform);
        RB.AddForce(transform.up * Force, ForceMode.Impulse);
        Held = false;
        WhoTossedTheBall = WhoThrew;
        Hand = null;
        BH = null;
        MakeBallReapear();
        //RBS.CmdSetPlayerHolding(null);
    }




    
    public void SetSteal(bool Passing, GameObject Target, float Force, Vector3 HandPos, GameObject WhoThrew)
    {
        //RpcSetPass(Passing, Target, Force, HandPos, WhoThrew);
        PV.RPC("RPC_SetSteal", RpcTarget.All, Passing, Target, Force, HandPos, WhoThrew);
    }

    [PunRPC]
    public void RPC_SetSteal(bool Passing, GameObject Target, float Force, Vector3 HandPos, GameObject WhoThrew)
    {
        MakeBallReapear();
        Thrown = true;

        passedTarget = Target;
        Handle.position = HandPos;
        Handle.parent = null;
        isInPassing = true;
        RB.velocity = Vector3.zero;
        RB.angularVelocity = Vector3.zero;
        float distance = (transform.position - Target.transform.position).magnitude;
        transform.LookAt(Target.transform);
        RB.AddForce(transform.up * Force, ForceMode.Impulse);
        Held = false;
        WhoTossedTheBall = WhoThrew;
        Hand = null;
        BH = null;
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

    
    public void MakeBallDisapear()
    {
        //RpcMakeBallDisapear();
        PV.RPC("RPC_MakeBallDisapear", RpcTarget.All);
    }

    [PunRPC]
    public void RPC_MakeBallDisapear()
    {
        ChildObject.SetActive(false);
        HardCol.enabled = false;
        SoftCol.enabled = false;
        RB.useGravity = false;
        RB.isKinematic = true;
    }

    
    public void MakeBallReapear()
    {
        //RpcMakeBallReapear();
        PV.RPC("RPC_MakeBallReapear", RpcTarget.All);
    }

    [PunRPC]
    public void RPC_MakeBallReapear()
    {
        HardCol.enabled = true;
        SoftCol.enabled = true;
        RB.useGravity = true;
        RB.isKinematic = false;
        ChildObject.SetActive(true);
    }

    
    private void UpdateHandTransform(GameObject HandParent)
    {
        //RpcUpdateHandTransform(HandParent);
        PV.RPC("RPC_UpdateHandTransform", RpcTarget.All, HandParent);
    }

    [PunRPC]
    private void RPC_UpdateHandTransform(GameObject HandParent)
    {
        BallHandling bh = HandParent.GetComponent<BallHandling>();
        Hand = bh.ReturnHand();
    }



    
    private void SetBallHandling(GameObject bhObject)
    {
        //RpcSetBallHandling(bhObject);
        PV.RPC("RPC_SetBallHandling", RpcTarget.All, bhObject);
    }

    [PunRPC]
    private void RPC_SetBallHandling(GameObject bhObject)
    {
        BH = bhObject.GetComponent<BallHandling>();
    }
    [Command]
    private void CmdPlayTrail()
    {
        RpcPlayTrail();
    }
    [ClientRpc]
    public void RpcPlayTrail()
    {
        BallTrail.enabled = true;
    }
    [Command]
    private void CmdStopTrail()
    {
        RpcStopTrail();
    }
    [ClientRpc]
    private void RpcStopTrail()
    {
        BallTrail.enabled = false;
    }
}
