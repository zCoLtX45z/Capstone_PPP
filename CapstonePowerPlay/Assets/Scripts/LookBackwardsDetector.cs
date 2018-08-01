using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LookBackwardsDetector : MonoBehaviour {

    private Cinemachine.CinemachineFreeLook playerVirtualCamera;


    public bool lookingBackwards;

    [SerializeField]
    private GameObject antiCrosshair;

    [SerializeField]
    private GameObject playerObject;

    [SerializeField]
    private GameObject cameraObject;

    // Use this for initialization
    void Start () {
        playerVirtualCamera = transform.GetComponent<Cinemachine.CinemachineFreeLook>();
        //antiCrosshair = GameObject.FindGameObjectWithTag("antiCrosshair");

        antiCrosshair.SetActive(false);
    }

    // Update is called once per frame
    void Update() {

        // Debug.Log("PlayerObj: " + playerObject.transform.localEulerAngles.y);
        //  Debug.Log("CameraObj: " + cameraObject.transform.localEulerAngles.y);


        //if(playerObject.transform.eulerAngles.y)


        //if (playerObject.transform.localEulerAngles.y <= cameraObject.transform.localEulerAngles.y - 90 && playerObject.transform.localEulerAngles.y >= cameraObject.transform.localEulerAngles.y + 90)

        /*
        float playerAngleTemp = 0;
        float cameraAngleTemp = 0;
        bool pBool = false;
        bool cBool = false;

        if (playerObject.transform.localEulerAngles.y > 180)
        {
            playerAngleTemp = playerObject.transform.localEulerAngles.y - 180;
            pBool = true;
        }


        if (cameraObject.transform.localEulerAngles.y > 180)
        {
            cameraAngleTemp = playerObject.transform.localEulerAngles.y - 180;
            cBool = true;
        }


        // both
        if (cBool && pBool)
        {
            Debug.Log("both");
            if ((180 - cameraAngleTemp) - playerAngleTemp <= 90)
            {
                lookingBackwards = true;
            }
        }

        //p
        else if (!cBool && pBool)
        {
            Debug.Log("p");
            if ((180 - cameraObject.transform.localEulerAngles.y) - playerAngleTemp <= 90)
            {
                lookingBackwards = true;
            }
        }
        //c
        else if (cBool && !pBool)
        {
            Debug.Log("c");
            if ((180 - cameraAngleTemp) - playerObject.transform.localEulerAngles.y <= 90)
            {
                lookingBackwards = true;
            }
        }
        // none
        else
        {
            Debug.Log("none");
            if ((180 - cameraObject.transform.localEulerAngles.y) - playerObject.transform.localEulerAngles.y <= 90)
            {
                lookingBackwards = true;
            }
        } 
    */
        Debug.Log(cameraObject.transform.localEulerAngles.y);


        if (cameraObject.transform.localEulerAngles.y >= 90 && cameraObject.transform.localEulerAngles.y <= 270)
        {
            lookingBackwards = true;
        }
        else
        {
            lookingBackwards = false;
        }


            ////////////////////////////////////////////////////////




        if (lookingBackwards)
        {
            antiCrosshair.SetActive(true);
        }

        else
        {
            antiCrosshair.SetActive(false);
        }

    }

}

