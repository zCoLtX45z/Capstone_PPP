﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToMenuInteraction : MonoBehaviour {

    private Camera menuCamera;

    private bool allowInteraction = false;

	// Use this for initialization
	void Start () {
        menuCamera = transform.GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        if (allowInteraction)
        {
            RaycastHit hit;

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Ray ray = menuCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    Transform objectHit = hit.transform;

                    hit.transform.GetComponent<ButtonCallToDollyManager>().CallEffect();
                    allowInteraction = false;
                }
            }
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "MenuTrigger")
        {
            allowInteraction = true;
        }
    }


}