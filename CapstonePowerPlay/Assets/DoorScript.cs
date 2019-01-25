using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DoorScript : NetworkBehaviour {

    RunePickups picks;
    Animator anim;
    public bool doorOpen;
    public GameObject myplayer;

	// Use this for initialization
	void Start () {
        doorOpen = false;
        anim = GetComponent<Animator>();
       // picks = GetComponent<RunePickups>();
	}
	
	// Update is called once per frame
	void Update () {
        if(doorOpen)
        {
            CmdOpenDoor();
        }

     //   if (!myplayer)
     //   {
     //       myplayer = GameObject.FindGameObjectWithTag("Team 1");
     //       //picks = GetComponent<RunePickups>();
     //   }
     //   else
     //   {
     //       picks = GetComponent<RunePickups>();
     //   }

	    //if(picks.Opendoor)
     //   {
     //       Debug.Log("DoorOpening");
     //       anim.SetBool("RuneMatch", true);
     //   }
	}

    [Command]
    public void CmdOpenDoor()
    {
        RpcOpenDoor();
    }

    [ClientRpc]
    public void RpcOpenDoor()
    {
        anim.SetBool("RuneMatch", true);
    }

}
