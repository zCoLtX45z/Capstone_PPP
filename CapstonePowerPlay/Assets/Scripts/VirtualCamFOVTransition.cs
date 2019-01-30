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

	// Update is called once per frame
	void Update () {

        //float tempNumberSet = Mathf.Clamp((((hbs.Speed) - 23) / 100 * 50) + 40, 40, 60);
        float tempNumberSet = Mathf.Clamp((((hbs.Speed) - 23) * 2) + 40, 40, 60);


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
