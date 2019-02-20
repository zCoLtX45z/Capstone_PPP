using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class KeepAlive : NetworkBehaviour
{

	// Update is called once per frame
	void Update ()
    {
        InvokeRepeating("CmdKeepAlive", 5.0f, 10.0f);
	}
    [Command]
    public void CmdKeepAlive()
    {
        RpcKeepAlive();
    }
    [ClientRpc]
    public void RpcKeepAlive()
    {

    }
}
