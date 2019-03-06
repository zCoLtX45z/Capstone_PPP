using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VirtualCamFOVTransition : MonoBehaviour {


    [SerializeField]
    private Camera cameraObject;

    [SerializeField]
    private hoverBoardScript hbs;


    private float currentNumber = 0;

    [SerializeField]
    private float deadZone;

    [SerializeField]
    private float fovTransiSpeed;


    [SerializeField]
    private float minFOV = 40;

    [SerializeField]
    private float maxFOV = 60;

    [SerializeField]
    private float boostMultiplication = 2;

    private void Start()
    {
        if (cameraObject == null)
        {
            cameraObject = transform.GetComponent<Camera>();
        }


        currentNumber = minFOV;


    }

    // Update is called once per frame
    void Update()
    {

        //float tempNumberSet = Mathf.Clamp((((hbs.Speed) - 23) / 100 * 50) + 40, 40, 60);
        float tempNumberSet = Mathf.Clamp((((hbs.Speed) - 23) * boostMultiplication) + minFOV, minFOV, maxFOV);

       // Debug.Log("tempNumberSet: " + tempNumberSet);

        //Debug.Log("tempNumberSet: " + tempNumberSet + " greater: " + (tempNumberSet - deadZone));

        if (currentNumber < tempNumberSet - deadZone)
        {
            currentNumber += Time.deltaTime * fovTransiSpeed;
        }

        else if (currentNumber > tempNumberSet + deadZone)
        {
            currentNumber -= Time.deltaTime * fovTransiSpeed;
        }

        Debug.Log("current number: " + currentNumber);

        if (cameraObject != null)
            cameraObject.fieldOfView = Mathf.Clamp(currentNumber, 40, 60);



    }
}
