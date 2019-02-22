using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PingServer : NetworkBehaviour {
	void Start () {
        if(isLocalPlayer)
            InvokeRepeating("CmdPingFunction", 10, 30);
	}

    [Command]
    private void CmdPingFunction()
    {
        Debug.Log("Called Ping");
    }
}
