using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraKeepOnPosition : MonoBehaviour {

    [SerializeField]
    private Transform cameraLookAtPosition;

    [SerializeField]
    private float maxRotSpeed = 1;


    private float currentRotSpeed;

    private float rotStep;

    [SerializeField]
    private float acceleration;

    [SerializeField]
    private CameraRotation camRot;

	//// Use this for initialization
	//void Start () {
       
 //   }
	
    public void UnParent()
    {
        //cameraLookAtPosition = transform.parent;
        transform.parent = null;
        Debug.Log("UnParent 2: cameraKeepOnPosition");
        camRot.GrabRot();
    }


	// Update is called once per frame
	void Update () {
        if (transform.parent == null)
        {
            transform.position = cameraLookAtPosition.position;

            float angle = Quaternion.Angle(transform.rotation, cameraLookAtPosition.rotation);

            currentRotSpeed = angle * acceleration;

            rotStep = currentRotSpeed * Time.deltaTime;

            transform.rotation = Quaternion.RotateTowards(transform.rotation, cameraLookAtPosition.rotation, rotStep);
        }
    }
}
