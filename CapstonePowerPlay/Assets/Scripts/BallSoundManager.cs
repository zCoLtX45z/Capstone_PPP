using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSoundManager : MonoBehaviour {

    private AudioSource audioSrc;

    public AudioClip bounce;

	// Use this for initialization
	void Start () {
        audioSrc = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision coll)
    {
        audioSrc.PlayOneShot(bounce, 1f);
    }
}
