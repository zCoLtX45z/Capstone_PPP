using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PingServer : NetworkBehaviour {
	void Start () {
        if(isLocalPlayer)
            InvokeRepeating("RpcPingFunction", 10, 30);
	}

    [ClientRpc]
    private void RpcPingFunction()
    {
        CmdPingFunction();
    }

    [Command]
    private void CmdPingFunction()
    {
        Debug.Log("Called Ping");
    }
}
