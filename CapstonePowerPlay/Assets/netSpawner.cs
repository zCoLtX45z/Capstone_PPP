﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class netSpawner : NetworkBehaviour
{
    [SerializeField]
    private Transform netSpawn;
    [SerializeField]
    private GameObject netPrefab;
	// Use this for initialization
	void Start ()
    {
        CmdSpawnNet();

    }
	
    [Command]
    public void CmdSpawnNet()
    {
        GameObject TempNet;
        TempNet = Instantiate(netPrefab, netSpawn.position, netSpawn.rotation);
        NetworkServer.Spawn(TempNet);
    }
}