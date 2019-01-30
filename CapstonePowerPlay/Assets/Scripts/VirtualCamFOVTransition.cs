using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VirtualCamFOVTransition : MonoBehaviour {

    [SerializeField]
    private CinemachineFreeLook freeLookCamera;

    [SerializeField]
    private hoverBoardScript hbs;

	// Update is called once per frame
	void Update () {

        float tempNumberSet = Mathf.Clamp((((hbs.Speed) - 23) / 100 * 50) + 40, 40, 60);

        freeLookCamera.m_Lens.FieldOfView = Mathf.Clamp(tempNumberSet, 40, 60);



    }
}
