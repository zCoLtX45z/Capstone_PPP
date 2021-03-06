﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobberScript : MonoBehaviour
{
    [HideInInspector]
    public bool Sinking = false;

    [SerializeField]
    private float speed = 10;

    private Vector3 InitialPosition;

	// Use this for initialization
	void Start ()
    {
        InitialPosition = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (!Sinking && transform.localPosition.y > -0.1f)
        {
            Debug.Log("floating");
            // Move back to resting position
            transform.Translate(0, -speed * Time.deltaTime, 0);
        }
        //if (transform.localPosition.y < 0)
        //{
        //    Debug.Log("floating");
        //    transform.localPosition = new Vector3(transform.localPosition.x, 0.1f, transform.localPosition.z);
        //}

        // Check if balls go to neverland
        if (transform.localPosition.x != InitialPosition.x || transform.localPosition.z != InitialPosition.z)
        {
            transform.localPosition = new Vector3(InitialPosition.x, transform.localPosition.y, InitialPosition.z);
        }

    }
    public void OnTriggerStay(Collider c)
    {
        if(c.gameObject.tag == "Ground")
        {
            Debug.Log("Bobber Trigger");
            Sinking = true;

            // Move Up
            transform.Translate(0, speed * Time.deltaTime, 0);
        }
    }

    public void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "Ground")
        {
            Sinking = false;
            Debug.Log("bobber exit");
        }
    }
}
