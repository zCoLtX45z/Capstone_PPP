using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ComponentsToDisable : NetworkBehaviour {

   public Behaviour[] DisabledComponents;

	// Use this for initialization
	void Start () {
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
}
