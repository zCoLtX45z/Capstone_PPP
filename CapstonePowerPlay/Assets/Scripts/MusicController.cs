using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {

    private AudioSource audioSrc;

    private float musicVolume = 0.05f;

    //private float minVol = 0.2f;
    //private float maxVol = 1f;

	// Use this for initialization
	void Start () {
        audioSrc = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update () {
        audioSrc.volume = musicVolume;
	}

    public void SetVolume(float vol)
    {
        //float vol = Random.Range(minVol, maxVol);
        audioSrc.PlayOneShot(bounce, audioSrc.volume);

    }
}
