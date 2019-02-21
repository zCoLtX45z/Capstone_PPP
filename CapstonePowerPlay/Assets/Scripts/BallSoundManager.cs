using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSoundManager : MonoBehaviour {

    private AudioSource audioSrc;

    public AudioClip bounce;

    private float minVol = 0.5f;
    private float maxVol = 1f;

	// Use this for initialization
	void Start () {
        audioSrc = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision coll)
    {
        float vol = Random.Range(minVol, maxVol);
        audioSrc.PlayOneShot(bounce, vol);
    }
}
