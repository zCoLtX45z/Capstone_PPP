using System.Collections;
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

    private bool isInPassing = false;


    private GameObject passedTarget;

    [SerializeField]
    private float RotSpeed = 0.5f;

    [SerializeField]
    private float maxDegree = 50;
   
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

            float angle = Mathf.Acos(Vector3.Dot(transform.forward, (passedTarget.transform.position - transform.position))/(Mathf.Abs(lengthOfForwardV * (passedTarget.transform.position - transform.position).magnitude)));

            angle *= 180 / Mathf.PI;

            angle = Mathf.Abs(angle);
            // float angle = Vector3.Angle(directionFromPlayer, transform.forward);

            if (angle <= maxDegree)
            {
                Vector3 lookPos = passedTarget.transform.position - transform.position;

                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * RotSpeed);
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
    public void Shoot(Vector3 power)
    {
        Debug.Log("power is " + power);
        RB.isKinematic = false;
        Handle.parent = null;
        RB.AddForce(power, ForceMode.Impulse);
    }
    {
    }
}
