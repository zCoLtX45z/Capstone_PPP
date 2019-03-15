using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DietySound : MonoBehaviour {

    public AudioClip[] welcome;



    private AudioSource Src;

    

	// Use this for initialization
	void Awake () {
        Src = GetComponent<AudioSource>();
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void PlayDietyWelcome()
    {
        Debug.Log("playmusicisCalled");
        Src.clip = welcome[Random.Range(0, welcome.Length)];
        Src.Play();
        
    }
}
