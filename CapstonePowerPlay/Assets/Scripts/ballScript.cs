﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballScript : MonoBehaviour {
    
    private Transform playHand;
    private PlayerScript player;
    Rigidbody RB;
    private bool usingGravity = true;

    public int spawnPlace;

    // Use this for initialization
    void Start ()
    {
        RB = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    public void OnTriggerEnter(Collider c)
    {
        if(c.gameObject.tag == "Player")
        {
            player = FindObjectOfType<PlayerScript>();
            playHand = player.HandReturn();
            if (!player.getHand())
            {
                spawnPlace = -1;
                toggleRB();
                this.transform.parent = playHand.transform;
                this.transform.position = playHand.transform.position;
                this.transform.rotation = playHand.transform.rotation;
                player.SetHand(true);
                player.setBall(this);
            }
           
        }
    }

    public void throwBall(Vector3 direction)
    {
        toggleRB();
        this.transform.parent = null;
        
        RB.AddForce(direction * 500);
        RB.AddForce(transform.up * 50);
    }

    public void toggleRB()
    {
        RB.isKinematic = usingGravity;
        usingGravity = !usingGravity;
        RB.useGravity = usingGravity;
    }
}
