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

    DoorScript Door;

    
    public bool Opendoor = false;

	// Use this for initialization
	void Start () {
        Door = FindObjectOfType<DoorScript>();
	}

    // Update is called once per frame
    void Update() {

        pickupTimer -= Time.deltaTime;

        if (red >= 3 || blue >= 3 ||green >= 3)
        {
            if(gameObject.tag == "Team 1")
            {
                Debug.Log("team1door");
                Opendoor = true;
                Door.CmdOpenDoor();
            }
            if(gameObject.tag == "Team 2")
            {
                Debug.Log("team2door");
                Opendoor = true;
                Door.CmdOpenDoor();
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
            }
        }
    }

    
    [Command]
    public void CmdDestroyRune(GameObject rune)
    {
        Destroy(rune);
    }

    [ClientRpc]
    void RpcDestroyRune(GameObject rune)
    {
        Destroy(rune);
    }

}
