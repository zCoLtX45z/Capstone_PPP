using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]
public class PlayerControlPhysics : NetworkBehaviour
{

    // Adjustables
    [SerializeField]
    private float JumpForce = 50;
    [SerializeField]
    private float MoveForce = 150;
    [SerializeField]
    private float TurnTorque = 150;
    [SerializeField]
    private float PlayerMass = 80;
    [SerializeField]
    private float FloatDistance = 0.2f;
    [SerializeField]
    private float MaxSpeed = 20f;
    //[SerializeField]
    //private float MaxSpin = 80f;
    [SerializeField]
    private Spring CharacterSpring;

    // Body Parts
    [SerializeField]
    private LayerMask GroundLayer;
    [SerializeField]
    private Transform Feet;
    private bool onGround = true;

    // Recieved attributes
    private float MoveAxis = 0;
    private float TurnAxis = 0;
    private float JumpAxis = 0;

    // Marcs Stuff
    public GameObject camGurl;
    public GameObject camGurl2;

    // Bobbers
    [SerializeField]
    private GameObject Bobbers;

    // Rigidbody
    private Rigidbody RB;
    //[SerializeField]
    //private Rigidbody MeshRB;
    private Transform Trans;

    // Use this for initialization
    void Start () {
        //MaxSpin *= Mathf.PI / 180;
        Physics.gravity = new Vector3(0, -20, 0);
        Cursor.lockState = CursorLockMode.Locked;
        RB = GetComponent<Rigidbody>();
        RB.mass = PlayerMass;
        Trans = transform;

        //local player camera
        if (isLocalPlayer)
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
	void FixedUpdate () {

        //local player control
        if (isLocalPlayer)
        {
            
            // Get Axis of Movement
            MoveAxis = Input.GetAxis("Move");
            TurnAxis = Input.GetAxis("Turn");
            //Debug.Log("Move Axis: " + MoveAxis);
            //Debug.Log("Turn Axis: " + TurnAxis);

            // Moving
            if (Mathf.Abs(MoveAxis) < 0.1f)
            {
                MoveAxis = 0;
            }
            //RB.AddForce(MoveAxis * MoveForce * Feet.forward, ForceMode.Force);
            Trans.Translate(0, 0, MoveAxis * MoveForce *Time.fixedDeltaTime);
            //Vector3 temp = MoveAxis * MoveForce * Vector3.forward;
            //Debug.Log("Vector: " + temp);
            //Debug.Log("Move Force: " + MoveAxis * MoveForce * Vector3.forward);
            //Debug.Log("Move Force Quantity: " + MoveForce);

            // Turning
            if (Mathf.Abs(TurnAxis) < 0.1f)
            {
                TurnAxis = 0;
            }
            //RB.AddTorque(TurnAxis * Trans.up * TurnTorque, ForceMode.Force);
            Trans.Rotate(TurnAxis * Trans.up * TurnTorque * Time.deltaTime);
            Bobbers.transform.Rotate(TurnAxis * Trans.up * TurnTorque * Time.deltaTime);
            //MeshRB.AddTorue(TurnAxis * Feet.up * TurnTorque, ForceMode.Force);
            //Debug.Log("Turn Force: " + TurnAxis * Trans.up * TurnTorque);
            //Debug.Log("Turn Torque Quantity: " + TurnTorque);

            // Jump
            JumpAxis = Input.GetAxis("Jump");
            onGround = Physics.Raycast(Feet.position, -Feet.up, FloatDistance + 0.3f, GroundLayer);
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
        if (RB.velocity.magnitude > MaxSpeed)
        {
            RB.velocity = RB.velocity / (RB.velocity.magnitude - MaxSpeed + 1);
        }
        //if (Mathf.Abs(RB.angularVelocity.y) > MaxSpin)
        //{
        //    RB.angularVelocity = RB.angularVelocity / (Mathf.Abs(RB.angularVelocity.y) - MaxSpeed + 1);
        //}
    }
}
