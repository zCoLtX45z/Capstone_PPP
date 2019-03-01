using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerSettings : MonoBehaviour {

    public static MultiplayerSettings MPS;

    public bool DelayStart;
    public int MaxPlayers;
    public int MenuScene;
    public int MultiplayerScene;


    private void Awake()
    {
        if (MultiplayerSettings.MPS == null)
        {
            MultiplayerSettings.MPS = this;
        }
        else
        {
            if (MultiplayerSettings.MPS != this)
            {
                Destroy(this.gameObject);
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
