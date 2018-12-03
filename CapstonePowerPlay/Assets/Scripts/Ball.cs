﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private Transform Handle;
    [SerializeField]
    private BallHandling BH;
    [SerializeField]
    private Transform Hand;
    [SerializeField]
    private bool Held = false;
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
    private float constantForce = 15.0f;

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

        if(isInPassing)
        {
            

            Vector3 forwardVector = transform.forward;
            float lengthOfForwardV = forwardVector.magnitude;
            Debug.Log("passTarget: " + passedTarget);
            float angle = Mathf.Acos(Vector3.Dot(transform.forward, (passedTarget.transform.position - transform.position))/(Mathf.Abs(lengthOfForwardV * (passedTarget.transform.position - transform.position).magnitude)));

            angle *= 180 / Mathf.PI;

            angle = Mathf.Abs(angle);
            // float angle = Vector3.Angle(directionFromPlayer, transform.forward);

            if (angle <= maxDegree)
            {
                if(RB.useGravity)
                    RB.useGravity = false;

                Debug.Log("Within angle");
                Vector3 lookPos = passedTarget.transform.position - transform.position;

                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * RotSpeed);
            }
            
            RB.AddForce(transform.forward * constantForce, ForceMode.Force);

            //RB.velocity = (transform.forward * constantForce);
        }
        
        if (RB.useGravity == false)
        { 
            if (!isInPassing)
            {
                RB.useGravity = true;
            }
        }

    }


    private void OnTriggerEnter(Collider c)
    {
        if(c.gameObject.tag == "Player" && !Held)
        {
            HardCol.isTrigger = true;
            RB.isKinematic = true;
            Held = true;
            BH = c.GetComponent<BallHandling>();

            //transform.gameObject.layer = 2;
            

            //if (BH.canHold)
            //{
            Hand = BH.ReturnHand();

                Handle.position = Hand.position;
                Handle.parent = Hand.parent;

                BH.SetBall(this);
            //}
            //else
            //{
            //    BH = null;
            //}
        }
    }

    private void OnTriggerExit(Collider other)
    {
        timer = 1;
        BH = null;
        HardCol.isTrigger = false;
    }
    public void Shoot(Vector3 power, string tag)
    {
        //transform.gameObject.layer = 0;

        Debug.Log("power is " + power);
        RB.isKinematic = false;
        Handle.parent = null;
        RB.AddForce(power, ForceMode.Impulse);
        Debug.Log("teamTag: " + tag);
        teamTag = tag;
    }
    public void SetPass(bool Passing, GameObject Target, float Force)
    {
        if (Target != null)
        {
            passedTarget = Target;
            RB.isKinematic = false;
            Handle.parent = null;
            isInPassing = true;
            float distance = (transform.position - Target.transform.position).magnitude;
            transform.LookAt(Target.transform.position);
            RB.AddForce(transform.forward * Force, ForceMode.Impulse);
        }
    }
}
