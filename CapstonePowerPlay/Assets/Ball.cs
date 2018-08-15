using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public void SetPass(bool Passing, GameObject Target)
    {
        
    }
}
