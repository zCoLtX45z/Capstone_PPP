using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ballHandler : NetworkBehaviour {


    [SerializeField]
    private GameObject ball;
    [SerializeField]
    private Transform ballSpawn;
    
    // Use this for initialization
    void Awake ()
    {
        //ballSpawn = GameObject.FindGameObjectWithTag("ballSpawn").GetComponent<Transform>();

        ballSpawn = GameObject.FindGameObjectWithTag("ballSpawn").GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        
        if(Input.GetKeyDown(KeyCode.J))
        {
            
            CmdSpawnBall();
            
        }
	}
    [Command]
    public void CmdSpawnBall()
    {
        Debug.Log("spawning ball");
        GameObject ball1 = Instantiate(ball, ballSpawn.position, ballSpawn.rotation);

        NetworkServer.Spawn(ball1);
    }
}
