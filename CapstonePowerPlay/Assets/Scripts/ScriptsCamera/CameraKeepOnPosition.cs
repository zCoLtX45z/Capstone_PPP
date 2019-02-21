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
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = cameraLookAtPosition.position;

        float angle = Quaternion.Angle(transform.rotation, cameraLookAtPosition.rotation);

        currentRotSpeed = angle * acceleration;

        rotStep = currentRotSpeed * Time.deltaTime;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, cameraLookAtPosition.rotation, rotStep);
    }
}
