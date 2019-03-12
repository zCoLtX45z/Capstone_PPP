using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotation : MonoBehaviour {

    // Adjustables
    [SerializeField]
    private float RotationSpeed = 10;
    [SerializeField]
    private LayerMask GroundLayers;

    // Components
    [SerializeField]
    private Transform CamParent;
    [SerializeField]
    private Transform Cam;
    [SerializeField]
    private Transform HelperPlayer;
    [SerializeField]
    private Transform HelperSpace;
    [SerializeField]
    private Transform HelperSpaceCam;
    [SerializeField]
    private Transform CameraLookAtPos;

    // Hiddenn variables
    [HideInInspector]
    public bool IsActive = true;
    private bool PlayerSetActive = true;
    private Vector3 DirectionHelper;
    private Vector3 DirectionCam;
	
	// Update is called once per frame
	void Update () {
		
        // Set if the player wants autorotation on
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlayerSetActive = !PlayerSetActive;
        }

        // If the players autorotation is active, rotatte
        if (IsActive && PlayerSetActive)
        {
            //HelperSpaceCam.position = camer
        }
	}
}
