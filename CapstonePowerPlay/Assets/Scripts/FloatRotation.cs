using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatRotation : MonoBehaviour
{
    [SerializeField]
    private BobberScript FBS;
    [SerializeField]
    private BobberScript BBS;
    private Vector3 DistanceFtoB;
    private float FBStartDistance;
    private float X_Angle;
    private float LerpPosition = 0;
    [SerializeField]
    private BobberScript RBS;
    [SerializeField]
    private BobberScript LBS;
    private Vector3 DistanceRtoL;
    private float RLStartDistance;
    private float Z_Angle;

    [SerializeField]
    private float RotateSpeed = 50f; // Rotation Speed per Second
    private float TickRotation; // Rotation Speed per tick


    // Timers
    // Back front rotation timers
    private float BackTimer = 0;
    private float FrontTimer = 0;

    // Front rotation timers
    private float LeftTimer = 0;
    private float RightTimer = 0;

    // Use this for initialization
    void Start ()
    {
        DistanceFtoB = FBS.transform.position - BBS.transform.position;
        FBStartDistance = DistanceFtoB.magnitude;
        DistanceRtoL = RBS.transform.position - LBS.transform.position;
        RLStartDistance = DistanceRtoL.magnitude;
        Debug.Log("adjacents (RL: " + RLStartDistance + ", FB: " + FBStartDistance + ")");
        TickRotation = RotateSpeed * 0.02f;
    }

    // Update is called once per frame
    void Update()
    {
        // Get the distance from A to B
        DistanceFtoB = FBS.transform.position - BBS.transform.position;
        DistanceRtoL = RBS.transform.position - LBS.transform.position;
        //Debug.Log("hypotenuse (RL: " + DistanceRtoL.magnitude + ", FB: " + DistanceFtoB.magnitude + ")");
        // Find Angles
        X_Angle = Mathf.Acos(Mathf.Abs(FBStartDistance / DistanceFtoB.magnitude)) * 180 / Mathf.PI;
        Z_Angle = Mathf.Acos(Mathf.Abs(RLStartDistance / DistanceRtoL.magnitude)) * 180 / Mathf.PI;
        //Debug.Log("Angles (Z: " + Z_Angle + ", X: " + X_Angle + ")");
    }

    private void FixedUpdate()
    {
        // Set thew rotation with this new Angle
        if (FBS.transform.localPosition.y > BBS.transform.localPosition.y)
        {
            // Rotate Backwards -- X Angle
            if (X_Angle < TickRotation)
            {
                transform.Rotate(-X_Angle, 0, 0);
            }
            else if (X_Angle >= TickRotation)
            {
                transform.Rotate(-TickRotation, 0, 0);
            }
        }
        else if (FBS.transform.localPosition.y < BBS.transform.localPosition.y)
        {
            // Rotate Forwards -- X Angle
            if (X_Angle < TickRotation)
            {
                transform.Rotate(X_Angle, 0, 0);
            }
            else if (X_Angle >= TickRotation)
            {
                transform.Rotate(TickRotation, 0, 0);
            }
        }

        if (RBS.transform.localPosition.y > LBS.transform.localPosition.y)
        {
            // Rotate Left
            if (Z_Angle < TickRotation)
            {
                transform.Rotate(0, 0, -Z_Angle);
            }
            else if (Z_Angle >= TickRotation)
            {
                transform.Rotate(0, 0, -TickRotation);
            }
        }
        else if (RBS.transform.localPosition.y < LBS.transform.localPosition.y)
        {
            // Rotate Right
            if (Z_Angle < TickRotation)
            {
                transform.Rotate(0, 0, Z_Angle);
            }
            else if (Z_Angle >= TickRotation)
            {
                transform.Rotate(0, 0, TickRotation);
            }
        }
    }
}
