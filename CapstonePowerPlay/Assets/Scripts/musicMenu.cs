using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicMenu : MonoBehaviour {

    private AudioSource Src;

    private float musicVolume = 0.05f;

	// Use this for initialization
	void Start () {
        Src = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        Src.volume = musicVolume;
	}

    public void SetVolume(float vol)
    {
        musicVolume = vol;
    }
}
