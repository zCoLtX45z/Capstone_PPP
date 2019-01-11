using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Ball : NetworkBehaviour
{
    [SerializeField]
    private Transform Handle;
    [SerializeField]
    public BallHandling BH;
    [SerializeField]
    private Transform Hand;
    [SyncVar]
    public bool Held = false;
    private Rigidbody RB;

    public SphereCollider HardCol;

    private float timer = 0;

    [SerializeField]
    private bool isInPassing = false;


    private GameObject passedTarget;

    [SerializeField]
    private float RotSpeed = 0.5f;

    [SerializeField]
    private float maxDegree = 50;

    // Who threw the ball (based on tag string)
    public string teamTag;

    [SerializeField]
    private float constantForce = 900.0f;

    [SyncVar]
    private float CanBeCaughtTimer = 1;
    private bool Thrown = false;
    private float SlerpRatio = 0;

    [SerializeField]
    private float maxTimePass = 2.0f;

    private float timePassTimer = 0.0f;

    [SerializeField]
    private GameObject ChildObject;
    [SerializeField]
    private SphereCollider SoftCol;

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
            float angle = Mathf.Acos(Vector3.Dot(transform.forward, (passedTarget.transform.position - transform.position))/(Mathf.Abs(lengthOfForwardV * (passedTarget.transform.position - transform.position).magnitude)));

            angle *= 180 / Mathf.PI;

            angle = Mathf.Abs(angle);
            Debug.Log("Within angle");
            //
            Vector3 lookPos = passedTarget.transform.position - transform.position;
            Vector3 direction = lookPos.normalized;

            var rotation = Quaternion.LookRotation(passedTarget.transform.position);
            SlerpRatio = Time.deltaTime * RotSpeed;
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
                RB.AddForce(direction * constantForce, ForceMode.Force);

            }
            else
            {
                //transform.rotation = rotation;
                //transform.LookAt(passedTarget);
                RB.AddForce(direction * constantForce / 2, ForceMode.Force);
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
        if(c.gameObject.tag == "Player" && !Held && !Thrown)
        {
            gameObject.layer = 2;
            HardCol.isTrigger = true;
            Held = true;
            BH = c.GetComponent<BallHandling>();

            //transform.gameObject.layer = 2;
            

            if (BH.canHold )
            {
                Hand = BH.ReturnHand();
                //Handle.position = Hand.position;
                //Handle.parent = Hand.parent;

                BH.CmdSetBall(gameObject);
                //RBS.CmdSetPlayerHolding(BH.gameObject);
                BH.CmdTurnOnFakeBall(true);
                //CmdTurnOnBall(false);
                CmdMakeBallDisapear();
            }
            else
            { 
                BH = null;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        timer = 1;
        BH = null;
        HardCol.isTrigger = false;
    }

    public void ShootBall(Vector3 power, string tag, Vector3 HandPos)
    {
        CmdShoot(power, tag, HandPos);
    }

    [Command]
    public void CmdShoot(Vector3 power, string tag, Vector3 HandPos)
    {
        //transform.gameObject.layer = 0;
        RpcShoot(power, tag, HandPos);
    }

    [ClientRpc]
    public void RpcShoot(Vector3 power, string tag, Vector3 HandPos)
    {
        CmdMakeBallReapear();
        //transform.gameObject.layer = 0;
        Thrown = true;
        CanBeCaughtTimer = 0.1f;
        Handle.position = HandPos;
        Debug.Log("power is " + power);
        RB.velocity = Vector3.zero;
        RB.angularVelocity = Vector3.zero;
        RB.AddForce(power, ForceMode.Impulse);
        Debug.Log("teamTag: " + tag);
        teamTag = tag;
        gameObject.layer = 10;
        Held = false;
        //RBS.CmdSetPlayerHolding(null);
    }

    [Command]
    public void CmdSetPass(bool Passing, GameObject Target, float Force, Vector3 HandPos)
    {
        RpcSetPass(Passing, Target, Force, HandPos);
    }

    [ClientRpc]
    public void RpcSetPass(bool Passing, GameObject Target, float Force, Vector3 HandPos)
    {
        CmdMakeBallReapear();
        Thrown = true;
        CanBeCaughtTimer = 0.1f;
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
        //RBS.CmdSetPlayerHolding(null);
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
        HardCol.enabled = false;
        SoftCol.enabled = false;
        RB.useGravity = false;
        RB.isKinematic = true;
        ChildObject.SetActive(false);
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
}
