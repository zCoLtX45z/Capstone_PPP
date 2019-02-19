using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtBall : MonoBehaviour {

    private Transform lookatBall;

    public bool allow = false;

	// Use this for initialization
	void Start () {
        lookatBall = GameObject.FindGameObjectWithTag("Ball").transform;
    }
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(lookatBall);
	}
}
