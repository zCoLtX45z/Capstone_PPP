using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RuneSpawnManager : MonoBehaviour {

    [SerializeField]
    private List<RuneSpawnQueue> RuneSpawns = new List<RuneSpawnQueue>();

	// Use this for initialization
	void Start () {
		if (PhotonNetwork.IsMasterClient)
        {
            foreach(RuneSpawnQueue RSQ in RuneSpawns)
            {
                RSQ.SpawnRune();
            }
        }
	}
}
