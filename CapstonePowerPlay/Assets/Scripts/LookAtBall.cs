using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtBall : MonoBehaviour {

    private Transform lookatBall;

    public bool allow = false;

    private bool hardLock;

    [SerializeField]
    private float maxLookAtSpeed;

    private float lookAtSpeed;

    [SerializeField]
    private float accelerationBoost = 1;

	// Use this for initialization
	void Start () {
        lookatBall = GameObject.FindGameObjectWithTag("Ball").transform;
        hardLock = false;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (allow)
        {
           

            if(lookAtSpeed < maxLookAtSpeed)
            {
                lookAtSpeed += Time.time * accelerationBoost;
            }
            else
            {
                lookAtSpeed = maxLookAtSpeed;
            }

            if (!hardLock)
            {
                Debug.Log("Alert");
                Vector3 direction = lookatBall.position - transform.position;

                //Vector3 newDir = Vector3.RotateTowards(transform.forward, direction, lookAtSpeed * Time.time, 0.0f);

                //transform.rotation = Quaternion.LookRotation(newDir);


                Quaternion rot = Quaternion.LookRotation(direction);
                // slerp to the desired rotation over time
                transform.rotation = Quaternion.Slerp(transform.rotation, rot, lookAtSpeed * Time.deltaTime);


                if (Vector3.Angle(direction, transform.forward) ==0)
                {
                    Debug.Log("activate hard lock");
                    hardLock = true;
                }
               

            }
            if (hardLock)
            {
                transform.LookAt(lookatBall, transform.up);
            }

        }
        else
        {
            if(lookAtSpeed >= maxLookAtSpeed)
            {
                lookAtSpeed = 0;
            }
            if(hardLock)
            {
                hardLock = false;
            }
        }
    }
}
