using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DietySound : MonoBehaviour {

    public AudioClip[] welcome;

    public AudioClip[] NetSpawn;

    private AudioSource Src;

    public float dietyVol;

    private PlayerSoundSettings PS;

    

	// Use this for initialization
	void Awake () {
        Src = GetComponent<AudioSource>();
        PS = FindObjectOfType<PlayerSoundSettings>();

        if (PS)
            dietyVol = PS.VoiceVol;
        else
            dietyVol = 0.1f;

    }
	
	// Update is called once per frame
	void Update () {
        Src.volume = dietyVol;
	}

    public void PlayDietyWelcome()
    {
        Debug.Log("playmusicisCalled");
        Src.clip = welcome[Random.Range(0, welcome.Length)];
        Src.Play();
        
    }

    public void PlayDietyNetSpawn()
    {
        Debug.Log("playmusicisCalled");
        Src.clip = NetSpawn[Random.Range(0, NetSpawn.Length)];
        Src.Play();

    }
}
