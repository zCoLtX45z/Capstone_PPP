using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetup : MonoBehaviour {

    public static GameSetup GS;

    public GameObject[] playerSpawns;
    // Use this for initialization
    void Start()
    {
        playerSpawns = GameObject.FindGameObjectsWithTag("playerSpawn");
    }


    private void OnEnable()
    {
        if (GameSetup.GS == null)
        {
            GameSetup.GS = this;
        }
    }
}
