using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DoorSpawner : NetworkBehaviour {
    [SerializeField]
    private Transform doorSpawn;
    [SerializeField]
    private GameObject doorPrefab;
    // Use this for initialization
    void Start()
    {
        CmdSpawnNet();

    }

    [Command]
    public void CmdSpawnNet()
    {
        GameObject TempNet;
        TempNet = Instantiate(doorPrefab, doorSpawn.position, doorSpawn.rotation);
        NetworkServer.Spawn(TempNet);
    }
}
