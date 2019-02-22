using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PingServer : NetworkBehaviour {
	void Start () {
        InvokeRepeating("RpcPingFunction", 10, 10);
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
