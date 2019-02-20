using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraKeepOnPosition : MonoBehaviour {

    //[SerializeField]
    private Transform cameraLookAtPosition;

    [SerializeField]
    private float maxRotSpeed = 1;


    private float currentRotSpeed;

    private float rotStep;

    [SerializeField]
    private float acceleration;

	// Use this for initialization
	void Start () {
        cameraLookAtPosition = transform.parent;
        transform.parent = null;
        //currentRotSpeed = maxRotSpeed;
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = cameraLookAtPosition.position;


        float angle = Quaternion.Angle(transform.rotation, cameraLookAtPosition.rotation);

        //currentRotSpeed = Mathf.Clamp((angle * acceleration), 0, maxRotSpeed);

        currentRotSpeed = angle * acceleration;

        rotStep = currentRotSpeed * Time.deltaTime;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, cameraLookAtPosition.rotation, rotStep);

        //transform.rotation = Quaternion.FromToRotation(transform.forward, cameraLookAtPosition.forward);
        // transform.rotation = cameraLookAtPosition.rotation;

        //transform.rotation = Quaternion.Lerp(transform.rotation, cameraLookAtPosition.rotation, rotSpeed);

        //transform.eulerAngles = new Vector3(transform.eulerAngles.x,
        //    cameraLookAtPosition.eulerAngles.y, transform.eulerAngles.z);
    }
}
