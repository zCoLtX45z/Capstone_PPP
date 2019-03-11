using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInGameMusic : MonoBehaviour {

    private PlayerSoundSettings PS;
    private AudioSource Src;

    private float MusicVol;


	// Use this for initialization
	void Start () {

        Src = GetComponent<AudioSource>();
        PS = FindObjectOfType<PlayerSoundSettings>();
        MusicVol = PS.musicVol;
	}
	
	// Update is called once per frame
	void Update () {
        Src.volume = MusicVol;
	}
}
