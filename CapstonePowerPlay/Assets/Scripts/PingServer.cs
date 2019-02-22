using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PingServer : NetworkBehaviour {

    [SerializeField]
    private float maxTime;

    private float currentTime;

	// Use this for initialization
	void Start () {
        currentTime = maxTime;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		if(currentTime > 0)
        {
            currentTime -= Time.time; 
        }
        else
        {
            currentTime = maxTime;

        }


	}

    [ClientRpc]
    private void RpcPingFunction()
    {
        CmdPingFunction();
    }

    [Command]
    private void CmdPingFunction()
    {

    }


}
