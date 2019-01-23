using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RunePickups : NetworkBehaviour {

    public int red;
    public int blue;
    public int green;
    public bool insureOneType = true;
    float pickupTimer = 0.5f;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {

        pickupTimer -= Time.deltaTime;

        if (red >= 3 || blue >= 3 ||green >= 3)
        {
            if(gameObject.tag == "Team 1")
            {
                //opendoor team 1
            }
            if(gameObject.tag == "Team 2")
            {
                //opendoor team 2
            }
        }
	}

    private void OnTriggerEnter(Collider r)
    {
        if (pickupTimer <= 0.0f)
        {
            if (r.gameObject.tag == "RedRune")
            {
                
                red++;
                if (insureOneType)
                {
                    blue = 0;
                    green = 0;
                }
                pickupTimer = 0.5f;
                CmdDestroyRune(r.gameObject);
                //Destroy(r.gameObject);
            }
            if (r.gameObject.tag == "BlueRune")
            {
                blue++;
                if (insureOneType)
                {
                    red = 0;
                    green = 0;
                }
                pickupTimer = 0.5f;
                CmdDestroyRune(r.gameObject);
            }
            if (r.gameObject.tag == "GreenRune")
            {
                green++;
                if (insureOneType)
                {
                    red = 0;
                    blue = 0;
                }
                pickupTimer = 0.5f;
                CmdDestroyRune(r.gameObject);
            }
        }
    }

    
    [Command]
    void CmdDestroyRune(GameObject rune)
    {
        Destroy(rune);
    }

    [ClientRpc]
    void RpcDestroyRune(GameObject rune)
    {
        Destroy(rune);
    }

}
