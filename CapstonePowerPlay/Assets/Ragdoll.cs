using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Ragdoll : MonoBehaviour
{
    //[SerializeField]
    //private Rigidbody[] RBs;
    [SerializeField]
    private ParticleSystem PS;
    [SerializeField]
    private ParticleSystem PS1;
    //[SerializeField]
    //private Rigidbody BaseRb;
    //[SerializeField]
    //private Animator Anim;
	// Use this for initialization
	void Awake ()
    {
  //      Anim = gameObject.GetComponentInChildren<Animator>();
  //      BaseRb = gameObject.GetComponent<Rigidbody>();
		//RBs = gameObject.GetComponentsInChildren<Rigidbody>();
        //foreach (Rigidbody rb in RBs)
        //{
        //    if(rb.gameObject.tag != "Shield")
        //    {
        //        rb.isKinematic = true;
        //    }

        //}
        //foreach (Rigidbody rb in RBs)
        //{
        //    if (rb.gameObject.tag != "Shield" || rb.gameObject.tag != "Team 1" || rb.gameObject.tag != "Team 2")
        //    {
        //        rb.mass = 0.01f;
        //    }

        //}
        //BaseRb.isKinematic = false;
        //BaseRb.mass = 100;
    }
	
	// Update is called once per frame
	void Update ()
    {
		//if(Input.GetKeyDown(KeyCode.P))
  //      {
  //          EnableRagdoll();
  //      }
        if(Input.GetKeyDown(KeyCode.L))
        {
            //CmdGetUp();
        }
	}

    //public void EnableRagdoll()
    //{

    //    //Death animation forward, copy for death anim backwards
    //    //foreach (Rigidbody rb in RBs)
    //    //{
    //    //    if (rb.gameObject.tag != "Shield" || rb.gameObject.tag != "Team 1" || rb.gameObject.tag != "Team 2")
    //    //    {
    //    //        //rb.isKinematic = false;
    //    //        rb.mass = 2.0f;
    //    //    }
    //    //}
    //    //BaseRb.isKinematic = true;
    //    //Anim.enabled = false;
    //}
    //[Command]
    //public void CmdGetUp()
    //{
    //    Debug.Log("getting up");
    //    PS.Play();
    //    PS1.Play();
    //    //foreach (Rigidbody rb in RBs)
    //    //{
    //    //    if (rb.gameObject.tag != "Shield" || rb.gameObject.tag != "Team 1" || rb.gameObject.tag != "Team 2")
    //    //    {
    //    //        //rb.isKinematic = true;
    //    //        rb.mass = 0.01f;
    //    //        rb.velocity = Vector3.zero;
    //    //    }
    //    //}
    //    //BaseRb.isKinematic = false;
    //    //Anim.enabled = true;

    //}
   
    //public void CmdPlayGetUpEffects()
    //{
    //    PS.Play();
    //    PS1.Play();
    //}
}
