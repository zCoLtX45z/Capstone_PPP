using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSoundFXs : MonoBehaviour {

    private AudioSource Src;
    
    public AudioClip interact;
    public AudioClip start;

    public float menuFXVol = 1f;

	// Use this for initialization
	void Start () {
        Src = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        Src.volume = menuFXVol;
	}



    public void PlayInt()
    {
        Src.clip = interact;
        Src.Play();
    }

    public void PlayStart()
    {
        Src.clip = start;
        Src.Play();
    }


    public void SetVolume(float vol)
    {
        menuFXVol = vol;
    }
}
