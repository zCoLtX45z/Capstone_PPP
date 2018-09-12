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
    private float Y_Angle;
    [SerializeField]
    private BobberScript RBS;
    [SerializeField]
    private BobberScript LBS;
    private Vector3 DistanceRtoL;
    private float RLStartDistance;
    private float X_Angle;

    // Use this for initialization
    void Start ()
    {
        DistanceFtoB = FBS.transform.position - BBS.transform.position;
        FBStartDistance = DistanceFtoB.magnitude;
        DistanceRtoL = RBS.transform.position - LBS.transform.position;
        RLStartDistance = DistanceRtoL.magnitude;
        Debug.Log("adjacents (RL: " + RLStartDistance + ", FB: " + FBStartDistance + ")");
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Get the distance from A to B
        DistanceFtoB = FBS.transform.position - BBS.transform.position;
        DistanceRtoL = RBS.transform.position - LBS.transform.position;
        Debug.Log("hypotenuse (RL: " + DistanceRtoL.magnitude + ", FB: " + DistanceFtoB.magnitude + ")");
        // Find Angles
        Y_Angle = Mathf.Acos(Mathf.Abs(FBStartDistance / DistanceFtoB.magnitude)) * 180 / Mathf.PI;
        X_Angle = Mathf.Acos(Mathf.Abs(RLStartDistance / DistanceRtoL.magnitude)) * 180 / Mathf.PI;
        Debug.Log("Angles (X: " + X_Angle + ", Y: " + Y_Angle + ")");

        // Set thew rotation with this new Angle
        if (FBS.transform.position.y > BBS.transform.position.y)
        {
            if (RBS.transform.position.y > LBS.transform.position.y)
            {
                Debug.Log("tilting backwards and left");
                Quaternion Rot = Quaternion.Euler(-Y_Angle, transform.localRotation.eulerAngles.y, X_Angle);
                transform.rotation = Quaternion.Lerp(transform.localRotation, Rot, Time.deltaTime * 5);
            }
            else if (RBS.transform.position.y < LBS.transform.position.y)
            {
                Debug.Log("tilting backwards and right");
                Quaternion Rot = Quaternion.Euler(-Y_Angle, transform.localRotation.eulerAngles.y, -X_Angle);
                transform.rotation = Quaternion.Lerp(transform.localRotation, Rot, Time.deltaTime * 5);
            }
        }
        if (FBS.transform.position.y < BBS.transform.position.y)
        {
            if (RBS.transform.position.y > LBS.transform.position.y)
            {
                Debug.Log("tilting forwards and left");
                Quaternion Rot = Quaternion.Euler(Y_Angle, transform.localRotation.eulerAngles.y, X_Angle);
                transform.rotation = Quaternion.Lerp(transform.localRotation, Rot, Time.deltaTime * 5);
            }
            else if (RBS.transform.position.y < LBS.transform.position.y)
            {
                Debug.Log("tilting forwards and right");
                Quaternion Rot = Quaternion.Euler(Y_Angle, transform.localRotation.eulerAngles.y, -X_Angle);
                transform.rotation = Quaternion.Lerp(transform.localRotation, Rot, Time.deltaTime * 5);
            }
        }

    }
}
