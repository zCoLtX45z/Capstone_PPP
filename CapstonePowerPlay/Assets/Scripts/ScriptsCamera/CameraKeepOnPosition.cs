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
	
    public void UnParent(int teamNumber)
    {
        //cameraLookAtPosition = transform.parent;
        transform.parent = null;
        Debug.Log("team Num: " + teamNumber);
        if (teamNumber == 2)
        {
            Debug.Log("Alert");
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        Debug.Log("UnParent 2: cameraKeepOnPosition");
        Debug.Log(transform.GetChild(0).eulerAngles);
        //camRot.GrabRot();
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
