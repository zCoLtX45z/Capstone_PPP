using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DietySound : MonoBehaviour {

    public AudioClip welcome;

    private AudioSource source;

	// Use this for initialization
	void Awake () {
        source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
