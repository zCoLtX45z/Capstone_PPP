using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraModeMedium : MonoBehaviour {

    [SerializeField]
    private LookAtBall lookAtBall = null;
    [SerializeField]
    private CameraRotation cameraRotation = null;

    private bool isFreeCam = false;

	// Use this for initialization
	void Start () {
        lookAtBall = transform.GetComponent<LookAtBall>();
        cameraRotation = transform.GetComponent<CameraRotation>();
        isFreeCam = true;
        lookAtBall.allow = false;
        cameraRotation.allow = true;
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.C))
        {
            ChangeCameraMode();
        }
	}

    public void ChangeCameraMode()
    {
        if (isFreeCam == false)
        {
            cameraRotation.GrabRot();
            isFreeCam = true;
            lookAtBall.allow = false;
            cameraRotation.allow = true;
            lookAtBall.hardLock = false;
        }
        else
        {
            isFreeCam = false;
            lookAtBall.allow = true;
            cameraRotation.allow = false;
        }
    }
}
