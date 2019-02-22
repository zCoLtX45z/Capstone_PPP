using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PingServer : NetworkBehaviour {
	void Update () {
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
    }
}
