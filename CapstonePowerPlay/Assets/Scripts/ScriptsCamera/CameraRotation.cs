using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour {


    [SerializeField]
    private float rotSpeed;

    private float yaw;

    private float pit;

    [SerializeField]
    private Transform objectToCopy;

    private Vector3 rotRef;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {



        
        yaw = rotSpeed * -Input.GetAxis("Mouse X");
        pit = rotSpeed * -Input.GetAxis("Mouse Y");

        

        rotRef += (new Vector3(pit, yaw, 0));
        rotRef = new Vector3(Mathf.Clamp(rotRef.x, -80, 80), rotRef.y, rotRef.z);

        transform.localEulerAngles = rotRef;






        //transform.eulerAngles = rotRef;

    }
}
