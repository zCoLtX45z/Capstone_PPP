using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotation : MonoBehaviour {

    // Adjustables
    [SerializeField]
    private float RotationForce = 50;

    // Components
    [SerializeField]
    private hoverBoardScript HBS;
    [SerializeField]
    private Transform Cam;
    [SerializeField]
    private Transform HelperSpaceCam;

    // Hiddenn variables
    [HideInInspector]
    public bool IsActive = true;
    private bool PlayerSetActive = true;
    private Vector3 DirectionHelper;
    private Vector3 DirectionCam;

    private string Rotate = "No";
    private string LastRotate = "Null";

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
            HelperSpaceCam.position = Cam.position;
            HelperSpaceCam.localPosition -= new Vector3(0, HelperSpaceCam.localPosition.y, 0);

            LastRotate = Rotate;
            // Camera is left of the player
            if (HelperSpaceCam.localPosition.x < 0)
            {
                Rotate = "Right";
            }
            // Camera is right of the player
            else if (HelperSpaceCam.localPosition.x > 0)
            {
                Rotate = "Left";
            }

            if (LastRotate == "Left" && Rotate == "Right")
                Rotate = "No";
            else if (LastRotate == "Right" && Rotate == "Left")
                Rotate = "No";

            LastRotate = Rotate;

            if (Rotate == "Right")
            {
                HBS.RotateHoverBoard(RotationForce);
            }
            else if (Rotate == "Left")
            {
                HBS.RotateHoverBoard(-RotationForce);
            }
        }
	}
}
