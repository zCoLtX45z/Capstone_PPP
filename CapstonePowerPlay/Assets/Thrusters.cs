﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrusters : MonoBehaviour
{
    [SerializeField]
    private hoverBoardScript HBS;
    [SerializeField]
    private float thisSpeed;
    [SerializeField]
    private ParticleSystem[] lowThrust;
    [SerializeField]
    private ParticleSystem[] midThrust;
    [SerializeField]
    private ParticleSystem[] highThrust;
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        thisSpeed = HBS.Speed;
        if(thisSpeed < 20)
        {
            foreach(ParticleSystem PS in lowThrust)
            {
                PS.Play();
            }
        }
        if(thisSpeed < 20 && thisSpeed < 30)
        {
            foreach(ParticleSystem PS in midThrust)
            {
                PS.Play();
            }
        }
        if(thisSpeed < 30)
        {
            foreach(ParticleSystem PS in highThrust)
            {
                PS.Play();
            }
        }
	}
}
