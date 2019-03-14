using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSoundManager : MonoBehaviour {

    private AudioSource audioSrc;

    public AudioClip bounce;

    public float bounceVol;

    private PlayerSoundSettings PS;

    //private float minVol = 0.2f;
    //private float maxVol = 1f;

	// Use this for initialization
	void Start () {
        PS = FindObjectOfType<PlayerSoundSettings>();
        audioSrc = GetComponent<AudioSource>();

        if (PS)
            bounceVol = PS.SoundFXVol;
        else
            audioSrc.volume = 0.1f;

    }
	
	// Update is called once per frame
	void Update () {
        audioSrc.volume = bounceVol;
	}

    private void OnCollisionEnter(Collision coll)
    {
        //float vol = Random.Range(minVol, maxVol);
        audioSrc.PlayOneShot(bounce, bounceVol);
        
    }
}
