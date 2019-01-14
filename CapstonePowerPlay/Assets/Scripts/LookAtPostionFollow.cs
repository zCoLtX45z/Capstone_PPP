﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPostionFollow : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    private Transform lookatPoint;


	void Start () {
        transform.parent = null;	
	}
	
	// Update is called once per frame
	void Update () {


        transform.position = lookatPoint.position;

        transform.up = Vector3.Cross(lookatPoint.forward, lookatPoint.right);
    }
}
