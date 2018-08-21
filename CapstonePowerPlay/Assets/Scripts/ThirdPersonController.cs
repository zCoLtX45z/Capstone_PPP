using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]
//[RequireComponent(typeof(ConfigurableJoint))]
public class ThirdPersonController : NetworkBehaviour {

    // Serielized
    // Movement
    [SerializeField]
    private float AccerationForce = 100000;
    [SerializeField]
    private float MaxVelocity = 20;
    [SerializeField]
    private float TurningForce = 10000;
    [SerializeField]
    private float MaxAngularVelocity = 180;
    [SerializeField]
    private float JumpForce = 250;
    [SerializeField]
    private float SpeedSlow = 80;
    [SerializeField]
    private float TurnSlow = 200;

    // Character Attributes
    [SerializeField]
    private float PlayerMass = 80;
    [SerializeField]
    private float FloatDistance = 0.2f;
    [SerializeField]
    private LayerMask GroundLayer;
    [SerializeField]
    private Transform Feet;
    [SerializeField]
    private Spring CharacterSpring;

    // Not Serielized
    // Player components
    private Rigidbody RB;
    private Transform Trans;
    //private ConfigurableJoint CJ;

    // Active attributes
    private float Speed = 0;
    private float AngularSpeed = 0;
    private float Acceleration = 0;
    private float TurningAcceleration = 0;
    private bool onGround = true;


    // Recieved attributes
    private float MoveAxis = 0;
    private float TurnAxis = 0;
    private float JumpAxis = 0;

    public GameObject camGurl;
    public GameObject camGurl2;
    

    void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        RB = GetComponent<Rigidbody>();
        RB.mass = PlayerMass;
        //CJ = GetComponent<ConfigurableJoint>();
        //CJ.connectedAnchor = new Vector3(0, FloatDistance, 0);
        Trans = transform;


        //local player camera
        if(isLocalPlayer)
        {
            camGurl.SetActive(true);
            camGurl2.SetActive(true);
        }
        else
        {
            camGurl.SetActive(false);
            camGurl2.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {


        //local player control
        if (isLocalPlayer)
        {

            // Get Axis of Movement
            MoveAxis = Input.GetAxis("Move");
            TurnAxis = Input.GetAxis("Turn");

            //Debug.Log("Move Axis: " + MoveAxis);
            //Debug.Log("Turn Axis: " + TurnAxis);
            // Accelerate/ Deccelerate
            // Speed
            if (MoveAxis > 0.1f || MoveAxis < -0.1)
            {
                if (Mathf.Abs(Speed) <= MaxVelocity)
                {
                    Acceleration = MoveAxis * AccerationForce * Time.deltaTime / PlayerMass;
                }
                else
                {
                    Acceleration = 0;
                }
            }
            else
            {
                Acceleration = 0;
                if (Mathf.Abs(Speed) > 5f)
                {
                    Speed -= Speed * SpeedSlow * Time.deltaTime / 100;
                }
                else if (Mathf.Abs(Speed) > 1f)
                {
                    Speed -= Speed * 95 * Time.deltaTime / 100;
                }
                else if (Mathf.Abs(Speed) > 0.1f)
                {
                    Speed -= Speed * 99 * Time.deltaTime / 100;
                }
                else
                    Speed = 0;
            }

            // Turning
            if (TurnAxis > 0.1f || TurnAxis < -0.1)
            {
                if (Mathf.Abs(AngularSpeed) <= MaxAngularVelocity)
                {
                    TurningAcceleration = TurnAxis * TurningForce * Time.deltaTime * 180 / PlayerMass;
                }
                else
                {
                    TurningAcceleration = 0;
                }
            }
            else
            {
                TurningAcceleration = 0;
                if (Mathf.Abs(AngularSpeed) > 10)
                {
                    AngularSpeed -= AngularSpeed * TurnSlow * Time.deltaTime / 100;
                }
                else
                {
                    AngularSpeed = 0;
                }
            }

            // Move Player
            Speed += Acceleration * Time.deltaTime;
            if (Speed > MaxVelocity)
            {
                Speed = MaxVelocity;
            }
            else if (Speed < -MaxVelocity)
            {
                Speed = -MaxVelocity;
            }
            //Debug.Log("Speed: " + Speed + " Acceleration: " + Acceleration);
            Trans.position += Trans.forward * Speed * Time.deltaTime;

            // Turn Player
            AngularSpeed += TurningAcceleration;
            if (AngularSpeed > MaxAngularVelocity)
            {
                AngularSpeed = MaxAngularVelocity;
            }
            else if (AngularSpeed < -MaxAngularVelocity)
            {
                AngularSpeed = -MaxAngularVelocity;
            }
            //Debug.Log("Angular Speed: " + AngularSpeed + " Angular Acceleration: " + TurningAcceleration);
            Trans.Rotate(0, AngularSpeed * Time.deltaTime, 0);

            // Jump
            JumpAxis = Input.GetAxis("Jump");
            onGround = Physics.Raycast(Feet.position, Vector3.down, FloatDistance + 0.3f, GroundLayer);
            //Debug.Log("Jump Axis: " + JumpAxis);
            if (onGround)
            {
                CharacterSpring.enabled = true;
                //Debug.Log("ON GROUND");
            }
            else
            {
                CharacterSpring.enabled = false;
                //Debug.Log("NOT ON GROUND");
            }

            if (JumpAxis > 0.1f && onGround)
            {
                RB.AddForce(Trans.up * JumpForce, ForceMode.Impulse);
            }
        }
    }
}
