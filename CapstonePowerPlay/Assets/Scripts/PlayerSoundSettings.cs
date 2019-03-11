using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSoundSettings : MonoBehaviour {


    public float musicVol;
    public float SoundFXVol;


    public Slider musicSlider;
    public Slider SoundFXSlider;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        musicVol = musicSlider.value;
        SoundFXVol = SoundFXSlider.value;
	}
}
