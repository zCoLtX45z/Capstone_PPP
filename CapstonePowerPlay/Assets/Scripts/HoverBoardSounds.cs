using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverBoardSounds : MonoBehaviour {

    private AudioSource Src;
    public AudioClip Hover;
    private hoverBoardScript HV;
    private PlayerSoundSettings PS;

    public float HovVol = 0.3f;
    private float minPitch = 1f;
    private float maxPitch = 2f;

	// Use this for initialization
	void Start () {
        HV = FindObjectOfType<hoverBoardScript>();
        Src = GetComponent<AudioSource>();
        PS = FindObjectOfType<PlayerSoundSettings>();
        if (PS)
            HovVol = PS.SoundFXVol;
        else
            HovVol = 0.1f;
        
	}
	
	// Update is called once per frame
	void Update () {
        Src.volume = HovVol;

        if (HV.Speed < 10)
            Src.pitch = minPitch;
        else
            Src.pitch = maxPitch;
	}

    public void SetVol(float vol)
    {
        HovVol = vol;
    }
}
