using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    [SerializeField]
    private Rigidbody[] RBs;
    [SerializeField]
    private ParticleSystem PS;
    [SerializeField]
    private ParticleSystem PS1;
	// Use this for initialization
	void Awake ()
    {
		RBs = gameObject.GetComponentsInChildren<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.P))
        {
            EnableRagdoll();
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            DisableRagdoll();
        }
	}

    public void EnableRagdoll()
    {
        foreach(Rigidbody rb in RBs)
        {
            rb.isKinematic = false;
        }
        gameObject.GetComponentInChildren<Animator>().enabled = false;
    }

    public void DisableRagdoll()
    {
        //foreach (Rigidbody rb in RBs)
        //{
        //    rb.isKinematic = true;
        //}
        gameObject.GetComponentInChildren<Animator>().enabled = true;
        PlayEffects();
    }

    public void PlayEffects()
    {
        PS.Play();
        PS1.Play();
    }
}
