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
    private Camera Cam;

    // Hiddenn variables
    [HideInInspector]
    public bool IsActive = true;
    private bool PlayerSetActive = true;
	
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

        }
	}
}
