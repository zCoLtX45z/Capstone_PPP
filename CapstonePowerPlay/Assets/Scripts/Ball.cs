using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Ball : NetworkBehaviour
{
    [SerializeField]
    private Transform Handle;
    [SerializeField]
    public BallHandling BH;
    [SyncVar]
    public GameObject WhoTossedTheBall = null;
    [SerializeField]
    public Transform Hand;
    [SyncVar]
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

    [SyncVar]
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

    // Use this for initialization
    void Start ()
    {
        Handle = GetComponent<Transform>();
        RB = GetComponent<Rigidbody>();
        
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
        CmdSetBallHandling(BH.gameObject);

        if (BH.canHold)
        {
            Hand = BH.ReturnHand();
            CmdUpdateHandTransform(BH.gameObject);
            BH.CmdSetBall(gameObject);
            BH.CmdTurnOnFakeBall(true);
            CmdMakeBallDisapear();
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
            //Debug.Log("PLAYER HAS ENTERED THE AREA!!!1");
            gameObject.layer = 2;
            HardCol.isTrigger = true;
            Held = true;

            // Set who has the the ball
            BH = c.GetComponent<BallHandling>();
            CmdSetBallHandling(BH.gameObject);
            //transform.gameObject.layer = 2;


            if (BH.canHold )
            {
                CmdMakeBallDisapear();
                Hand = BH.ReturnHand();
                CmdUpdateHandTransform(BH.gameObject);
                //Handle.position = Hand.position;
                //Handle.parent = Hand.parent;

                BH.CmdSetBall(gameObject);
                //RBS.CmdSetPlayerHolding(BH.gameObject);
                BH.CmdTurnOnFakeBall(true);
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
        CmdShoot(power, tag, HandPos, WhoThrew);
    }

    [Command]
    public void CmdShoot(Vector3 power, string tag, Vector3 HandPos, GameObject WhoThrew)
    {
        //transform.gameObject.layer = 0;
        RpcShoot(power, tag, HandPos, WhoThrew);
    }

    [ClientRpc]
    public void RpcShoot(Vector3 power, string tag, Vector3 HandPos, GameObject WhoThrew)
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
        CmdMakeBallReapear();
        //RBS.CmdSetPlayerHolding(null);
    }

    [Command]
    public void CmdSetPass(bool Passing, GameObject Target, float Force, Vector3 HandPos, GameObject WhoThrew)
    {
        RpcSetPass(Passing, Target, Force, HandPos, WhoThrew);
    }

    [ClientRpc]
    public void RpcSetPass(bool Passing, GameObject Target, float Force, Vector3 HandPos, GameObject WhoThrew)
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
        CmdMakeBallReapear();
        //RBS.CmdSetPlayerHolding(null);
    }




    [Command]
    public void CmdSetSteal(bool Passing, GameObject Target, float Force, Vector3 HandPos, GameObject WhoThrew)
    {
        RpcSetPass(Passing, Target, Force, HandPos, WhoThrew);
    }

    [ClientRpc]
    public void RpcSetSteal(bool Passing, GameObject Target, float Force, Vector3 HandPos, GameObject WhoThrew)
    {
        CmdMakeBallReapear();
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

    [Command]
    public void CmdTurnOnBall(bool b)
    {
        RpcTurnOnBall(b);
    }

    [ClientRpc]
    public void RpcTurnOnBall(bool b)
    {
        gameObject.SetActive(b);
    }

    [Command]
    public void CmdMakeBallDisapear()
    {
        RpcMakeBallDisapear();
    }

    [ClientRpc]
    public void RpcMakeBallDisapear()
    {
        ChildObject.SetActive(false);
        HardCol.enabled = false;
        SoftCol.enabled = false;
        RB.useGravity = false;
        RB.isKinematic = true;
    }

    [Command]
    public void CmdMakeBallReapear()
    {
        RpcMakeBallReapear();
    }

    [ClientRpc]
    public void RpcMakeBallReapear()
    {
        HardCol.enabled = true;
        SoftCol.enabled = true;
        RB.useGravity = true;
        RB.isKinematic = false;
        ChildObject.SetActive(true);
    }

    [Command]
    private void CmdUpdateHandTransform(GameObject HandParent)
    {
        RpcUpdateHandTransform(HandParent);
    }

    [ClientRpc]
    private void RpcUpdateHandTransform(GameObject HandParent)
    {
        BallHandling bh = HandParent.GetComponent<BallHandling>();
        Hand = bh.ReturnHand();
    }



    [Command]
    private void CmdSetBallHandling(GameObject bhObject)
    {
        RpcSetBallHandling(bhObject);
    }

    [ClientRpc]
    private void RpcSetBallHandling(GameObject bhObject)
    {
        BH = bhObject.GetComponent<BallHandling>();
    }

}
