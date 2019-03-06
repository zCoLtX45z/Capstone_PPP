using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverBoardSounds : MonoBehaviour {

    private AudioSource Src;
    public AudioClip Hover;
    private hoverBoardScript HV;

    private float minPitch = 1f;
    private float maxPitch = 2f;

	// Use this for initialization
	void Start () {
        HV = GetComponent<hoverBoardScript>();
        Src = GetComponent<AudioSource>();
        Src.loop = true;
        Src.playOnAwake = true;
        Src.spatialBlend = 1;
        Src.maxDistance = 20;
	}
	
	// Update is called once per frame
	void Update () {
        if (HV.Speed < 10)
            Src.pitch = minPitch;
        else
            Src.pitch = maxPitch;
	}
}
