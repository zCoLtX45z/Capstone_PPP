using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicMenu : MonoBehaviour {

    private AudioSource Src;

    public AudioClip[] music;

    private float musicVolume = 1f;

	// Use this for initialization
	void Start () {
        Src = GetComponent<AudioSource>();
        //if (!Src.playOnAwake)
        //{
        //    Src.clip = music[0];
        //    Src.Play();
        //}
    }
	
	// Update is called once per frame
	void Update () {
        Src.volume = musicVolume;
        //if (!Src.isPlaying)
        //{
        //    Src.clip = music[Random.Range(0, music.Length)];
        //    Src.Play();
        //}
    }

    public void SetVolume(float vol)
    {
        musicVolume = vol;
    }

    //void PlayNextSong()
    //{
    //    Debug.Log("playmusicisCalled");
    //    Src.clip = music[Random.Range(0, music.Length)];
    //    Src.Play();
    //    Invoke("PlayNextSong", Src.clip.length);
    //}
}
