using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameSoundSettings : MonoBehaviour {


    public float musicVol;
    public float SoundFXVol;
    public float VoiceVol;

    public Slider musicSlider;
    public Slider SoundFXSlider;
    public Slider voiceSlider;

    private PlayerSoundSettings PS;
    private PlayerInGameMusic PGM;
    private HoverBoardSounds HV;
    private BallSoundManager BS;



    // Use this for initialization
    void Start()
    {
        PS = FindObjectOfType<PlayerSoundSettings>();
        PGM = FindObjectOfType<PlayerInGameMusic>();
        HV = FindObjectOfType<HoverBoardSounds>();
        BS = FindObjectOfType<BallSoundManager>();
        musicVol = PS.musicVol;
        musicSlider.value = musicVol;
        SoundFXVol = PS.SoundFXVol;
        SoundFXSlider.value = SoundFXVol;
        VoiceVol = PS.VoiceVol;
        voiceSlider.value = VoiceVol;
    }

    // Update is called once per frame
    void Update()
    {

        if (musicSlider != null)
        {
            musicVol = musicSlider.value;
            PGM.MusicVol = musicVol;
        }
        if (SoundFXSlider != null)
        {
            SoundFXVol = SoundFXSlider.value;
            HV.HovVol = SoundFXVol;
            BS.bounceVol = SoundFXVol;
        }
        if (voiceSlider != null)
        {
            VoiceVol = voiceSlider.value;

        }

    }
}
