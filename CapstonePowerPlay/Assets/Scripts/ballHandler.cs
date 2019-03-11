using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ballHandler : MonoBehaviour {


    [SerializeField]
    private GameObject ball;
    [SerializeField]
    private Transform ballSpawn;
    
    // Use this for initialization
    void Start ()
    {

        ballSpawn = GameObject.FindGameObjectWithTag("ballSpawn").GetComponent<Transform>();
        //if (isServer)
        //if(PhotonNetwork.IsMasterClient)
        //{
        //    SpawnBall();
        //}
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        
        if(Input.GetKeyDown(KeyCode.J))
        {
            
            SpawnBall();
            
        }
	}
   
    public void SpawnBall()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            //Debug.Log("spawning ball");
            PhotonNetwork.Instantiate("Prefabs/Ultra Ball", ballSpawn.position, ballSpawn.rotation);
        }
        
    }
}
