using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour {

    // Joints
    [SerializeField]
    private GameObject JointOne;
    [SerializeField]
    private GameObject JointTwo;

    // Custumizables
    [SerializeField]
    private float VerticeLength = 1;
    [SerializeField]
    private float MaxLength = 1;
    [SerializeField]
    private float SpringDamper = 0.7f;
    [SerializeField]
    private float SpringForceModulus = 1;
    [SerializeField]
    private bool AttachedToPlayer = false;

    // Active Variables
    private Vector3 displacement;
    private float distance;
    private float force;

    // gameobject attributes
    private Rigidbody RB1;
    private Rigidbody RB2;

    // Use this for initialization
    void Start () {
        displacement = JointOne.transform.position - JointTwo.transform.position;
        distance = displacement.magnitude;
        RB1 = JointOne.GetComponent<Rigidbody>();
        RB2 = JointTwo.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        displacement = JointOne.transform.position - JointTwo.transform.position;
        distance = displacement.magnitude;
        if (distance > VerticeLength + MaxLength)
            distance = VerticeLength + MaxLength;
        force = SpringForceModulus * (distance - VerticeLength) * SpringDamper;

        if (AttachedToPlayer)
        {
            RB1.AddForce(force * Vector3.up, ForceMode.Force);
        }

        RB2.AddForce(-force * Vector3.up, ForceMode.Force);
        //Debug.Log("Force to Player " + -force);
    }
}
