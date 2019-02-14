using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SetBallLookAtVcamVar : MonoBehaviour {

    private Transform lookatBall;
    private CinemachineVirtualCamera vCam;

	// Use this for initialization
	void Start () {
        vCam = transform.GetComponent<CinemachineVirtualCamera>();

        lookatBall = GameObject.FindGameObjectWithTag("Ball").transform;

        vCam.m_LookAt = lookatBall;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
