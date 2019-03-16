using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVoiceLine : MonoBehaviour {

    private PlayerSoundSettings PS;
    private AudioSource Src;
    public AudioClip[] ballPickup;
    public AudioClip[] ability;
    public AudioClip[] win;
    public AudioClip[] dietyloose;
    public AudioClip Ball;
    public float voiceVol;

	// Use this for initialization
	void Start () {
        Src = GetComponent<AudioSource>();
        PS = FindObjectOfType<PlayerSoundSettings>();

        if (PS)
            voiceVol = PS.VoiceVol;
        else
            voiceVol = 0.1f;
    }
	
	// Update is called once per frame
	void Update () {
        Src.volume = voiceVol;
	}

    public void PlaydietyLoose()
    {
        Src.clip = dietyloose[Random.Range(0, dietyloose.Length)];
        Src.Play();
    }
    public void PlayBallPickup()
    {
        Src.clip = ballPickup[Random.Range(0, ballPickup.Length)];
        Src.Play();
    }
    public void PlayAbility()
    {
        Src.clip = ability[Random.Range(0, ability.Length)];
        Src.Play();
    }
    public void PlayWin()
    {
        Src.clip = win[Random.Range(0, win.Length)];
        Src.Play();
    }

    public void PlayShoot()
    {
        Src.clip = Ball;
        Src.Play();
    }
}
