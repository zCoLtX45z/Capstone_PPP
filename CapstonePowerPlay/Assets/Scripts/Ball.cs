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

    private float CanBeCaughtTimer = 1;
    private bool Thrown = false;
    private float SlerpRatio = 0;

    [SerializeField]
    private float maxTimePass = 2.0f;

    private float timePassTimer = 0.0f;

    [SerializeField]
    private ResetBallState RBS;


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
                CmdTurnOnBall(false);
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

    public void ShootBall(Vector3 power, string tag)
    {
        CmdShoot(power, tag);
    }

    [Command]
    public void CmdShoot(Vector3 power, string tag)
    {
        //transform.gameObject.layer = 0;
        RpcShoot(power, tag);
    }

    [ClientRpc]
    public void RpcShoot(Vector3 power, string tag)
    {
        //transform.gameObject.layer = 0;
        Thrown = true;
        Handle.position = Hand.position;
        Debug.Log("power is " + power);
        RB.AddForce(power, ForceMode.Impulse);
        Debug.Log("teamTag: " + tag);
        teamTag = tag;
        gameObject.layer = 10;
        Held = false;
        //RBS.CmdSetPlayerHolding(null);
    }

    [Command]
    public void CmdSetPass(bool Passing, GameObject Target, float Force)
    {
        RpcSetPass(Passing, Target, Force);
    }

    [ClientRpc]
    public void RpcSetPass(bool Passing, GameObject Target, float Force)
    {
        Thrown = true;
        passedTarget = Target;
        Handle.position = Hand.position;
        Handle.parent = null;
        isInPassing = true;
        
        float distance = (transform.position - Target.transform.position).magnitude;
        //transform.LookAt(Target);
        //RB.AddForce(transform.forward * Force, ForceMode.Impulse);
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
}
