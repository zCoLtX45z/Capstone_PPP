using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VirtualCamFOVTransition : MonoBehaviour {

    [SerializeField]
    private CinemachineFreeLook freeLookCamera;

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

	// Update is called once per frame
	void Update () {

        //float tempNumberSet = Mathf.Clamp((((hbs.Speed) - 23) / 100 * 50) + 40, 40, 60);
        float tempNumberSet = Mathf.Clamp((((hbs.Speed) - 23) * boostMultiplication) + minFOV, minFOV, maxFOV);


        if(currentNumber < tempNumberSet - deadZone)
        {
            currentNumber += Time.deltaTime * fovTransiSpeed;
        }

        else if(currentNumber > tempNumberSet + deadZone)
        {
            currentNumber -= Time.deltaTime * fovTransiSpeed;
        }

        freeLookCamera.m_Lens.FieldOfView = Mathf.Clamp(currentNumber, 40, 60);



    }
}
