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
    [SerializeField]
    private Transform ForwardLook;
   
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
        Debug.DrawRay(transform.position, LeftWheel.forward * LeftHit.distance, Color.blue);
        Physics.Raycast(transform.position, RightWheel.forward, out RightHit, 15);
        Debug.DrawRay(transform.position, RightWheel.forward * RightHit.distance, Color.blue);
        Debug.DrawRay(transform.position, -transform.up * 0.7f, Color.yellow);

        float RotationY = transform.rotation.eulerAngles.y;
        // Auto correct
        if (!Physics.Raycast(transform.position,-transform.up, 0.7f))
        {
            if (Mathf.Abs(transform.rotation.eulerAngles.x) > 0)
            {
                if (transform.rotation.eulerAngles.x > 0)
                {
                    transform.Rotate(transform.right * RotationSpeed * Time.fixedDeltaTime * Mathf.Abs(transform.rotation.eulerAngles.x) * Mathf.PI / 180);
                    Debug.Log("Air 1 - Tilt Forwards");
                }
                if (transform.rotation.eulerAngles.x < 0)
                {
                    Debug.Log("Air 2 - Tilt Backwards");
                    transform.Rotate(-transform.right * RotationSpeed * Time.fixedDeltaTime * Mathf.Abs(transform.rotation.eulerAngles.x) * Mathf.PI / 180);
                }
            }
            if (Mathf.Abs(transform.rotation.eulerAngles.z) > 0)
            {
                if (transform.rotation.eulerAngles.z > 0)
                {
                    transform.Rotate(-transform.forward * RotationSpeed * Time.fixedDeltaTime * Mathf.Abs(transform.rotation.eulerAngles.z) * Mathf.PI / 180);
                    Debug.Log("Air 3 - Tile Right");
                }
                if (transform.rotation.eulerAngles.z < 0)
                {
                    transform.Rotate(transform.forward * RotationSpeed * Time.fixedDeltaTime * Mathf.Abs(transform.rotation.eulerAngles.z) * Mathf.PI / 180);
                    Debug.Log("Air 4 - Tilt Left");
                }
            }

        }
        else
        {
            // Forward Backwards
            if (Mathf.Round(FrontHit.distance * 180) / 10 - Mathf.Round(BackHit.distance * 180) / 10 > 0)
            {
                Debug.Log("Ground 1 - Tilt Forwards");
                // Rotate Forwards
                transform.Rotate(transform.right * RotationSpeed * Time.fixedDeltaTime * Mathf.Abs(FrontHit.distance - BackHit.distance) * 2 / (FrontHit.distance + BackHit.distance));
            }
            else if (Mathf.Round(FrontHit.distance * 180) / 10 - Mathf.Round(BackHit.distance * 180) / 10 < 0)
            {
                Debug.Log("Ground 2 - Tilt Backwards");
                // Rotate Backwards
                transform.Rotate(-transform.right * RotationSpeed * Time.fixedDeltaTime * Mathf.Abs(FrontHit.distance - BackHit.distance) * 2 / (FrontHit.distance + BackHit.distance));

            }

            // Side to Side
            if (Mathf.Round(RightHit.distance * 180) / 10 - Mathf.Round(LeftHit.distance * 180) / 10 > 0)
            {
                Debug.Log("Ground 3 - Tilt Right");
                // Rotate Right
                transform.Rotate(-transform.forward * RotationSpeed * Time.fixedDeltaTime * Mathf.Abs(RightHit.distance - LeftHit.distance) * 2 / (RightHit.distance + LeftHit.distance));
            }
            else if (Mathf.Round(RightHit.distance * 180) / 10 - Mathf.Round(LeftHit.distance * 180) / 10 < 0)
            {
                Debug.Log("Ground 4 - Tilt Left");
                // Rotate Left
                transform.Rotate(transform.forward * RotationSpeed * Time.fixedDeltaTime * Mathf.Abs(RightHit.distance - LeftHit.distance) * 2 / (RightHit.distance + LeftHit.distance));

            }
        }

        transform.rotation.eulerAngles.Set(transform.rotation.eulerAngles.x, RotationY, transform.rotation.eulerAngles.z);
    }
    
}
