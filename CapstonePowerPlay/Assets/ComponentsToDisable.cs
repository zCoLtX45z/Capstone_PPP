using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ComponentsToDisable : NetworkBehaviour {

    public Behaviour[] DisabledComponents;
    //[HideInInspector]
    public NetPlayer LocalPlayer;

    // Use this for initialization
    [Command]
    public void CmdForcedStart()
    {
        RpcForcedStart();
    }

    [ClientRpc]
	public void RpcForcedStart () {
		if(!isLocalPlayer)
        {
            for(int i = 0; i < DisabledComponents.Length; i++)
            {
                DisabledComponents[i].enabled = false;
            }
        }


        // added by andre.
        else
        {
            gameObject.layer = 2;
        }
	}

    public void ForcedStart()
    {
        if (LocalPlayer.PlayerCode != gameObject.name)
        {
            for (int i = 0; i < DisabledComponents.Length; i++)
            {
                DisabledComponents[i].enabled = false;
            }
        }


        // added by andre.
        else
        {
            gameObject.layer = 2;
        }
    }
}
