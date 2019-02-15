using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Ragdoll : NetworkBehaviour
{
   
    [SerializeField]
    private ParticleSystem PS;
    [SerializeField]
    private ParticleSystem PS1;
   
	void Awake ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {

        if(Input.GetKeyDown(KeyCode.O))
        {
            CmdPlayGetUpEffects();
        }
	}


    [Command]
    public void CmdPlayGetUpEffects()
    {
        PS.Play();
        PS1.Play();
    }
}
