using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    private SphereCollider SC;

    private RaycastHit FrontHit;
    private RaycastHit BackHit;
    private RaycastHit LeftHit;
    private RaycastHit RightHit;
    private Rigidbody RB;
    private Transform _transform;
    [SerializeField]
    private float RotationSpeed = 10;
    [SerializeField]
    private Transform WheelForward;
    [SerializeField]
    private Transform WheelBack;
    [SerializeField]
    private Transform LeftWheel;
    [SerializeField]
    private Transform RightWheel;


    // Use this for initialization
    void Start ()
    {
        SC = GetComponent<SphereCollider>();
        RB = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void FixedUpdate()
    {
        RotatePlayer();
    }
    private void RotatePlayer()
    {
        Physics.Raycast(transform.position, WheelForward.forward, out FrontHit, 15);
        Debug.DrawRay(transform.position, WheelForward.forward * FrontHit.distance, Color.red);
        Physics.Raycast(transform.position, WheelBack.forward, out BackHit, 15);
        Debug.DrawRay(transform.position, WheelBack.forward * BackHit.distance, Color.red);


        Physics.Raycast(transform.position, LeftWheel.forward, out LeftHit, 15);
        Debug.DrawRay(transform.position, LeftWheel.forward * LeftHit.distance, Color.red);
        Physics.Raycast(transform.position, RightWheel.forward, out RightHit, 15);
        Debug.DrawRay(transform.position, RightWheel.forward * RightHit.distance, Color.red);

        // Auto correct
        if (!Physics.Raycast(transform.position,-transform.up, 0.3f))
        {
            Vector3 Direction = transform.rotation.eulerAngles;
            Vector3.Slerp(Direction, Vector3.up, RotationSpeed * Time.fixedDeltaTime);
            //transform.rotation.eulerAngles.Set(Direction.x, Direction.y, Direction.z);
        }
        else
        {
            // Forward Backwards
            if (Mathf.Round(FrontHit.distance * 30) / 10 > Mathf.Round(BackHit.distance * 30) / 10)
            {
                // Rotate Forwards
                transform.Rotate(transform.right * RotationSpeed * Time.fixedDeltaTime);
            }
            else if (Mathf.Round(FrontHit.distance * 30) / 10 < Mathf.Round(BackHit.distance * 30) / 10)
            {
                // Rotate Backwards
                transform.Rotate(-transform.right * RotationSpeed * Time.fixedDeltaTime);

            }

            // Side to Side
            if (Mathf.Round(RightHit.distance * 30) / 10 > Mathf.Round(LeftHit.distance * 30) / 10)
            {
                // Rotate Right
                transform.Rotate(-transform.forward * RotationSpeed * Time.fixedDeltaTime);
            }
            else if (Mathf.Round(RightHit.distance * 30) / 10 < Mathf.Round(LeftHit.distance * 30) / 10)
            {
                // Rotate Left
                transform.Rotate(transform.forward * RotationSpeed * Time.fixedDeltaTime);

            }
        }
    }
    
}
