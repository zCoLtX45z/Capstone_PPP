using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    [SerializeField]
    private Rigidbody Rb;
    [SerializeField]
    private GameObject SpeedStrip;
    [SerializeField]
    private float CurrentEnergy;
    private float SpeedStripCost = 33;
    private float MaxEnergy = 100;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Rb.velocity.z > 1 && CurrentEnergy < MaxEnergy)
        {
            CurrentEnergy += Time.deltaTime * 2;
        }
        if(CurrentEnergy > MaxEnergy)
        {
            CurrentEnergy = MaxEnergy;
        }

        if(Input.GetKeyDown(KeyCode.C) && CurrentEnergy >= SpeedStripCost)
        {

        }
       


	}
}
