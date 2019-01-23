using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RuneSpawn : NetworkBehaviour {

    [SerializeField]
    private GameObject[] Runes;

    GameObject myInstantiatedObject;
    

    // Use this for initialization
    void Start () {
        CmdSpawnRune();
	}

    Vector3 RandomCircle(Vector3 center, float radius)
    {
        float ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }

    [Command]
    void CmdSpawnRune()
    {
        Vector3 center = transform.position;
        Vector3 pos = RandomCircle(center, 5.0f);
        Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center - pos);
        myInstantiatedObject = Instantiate(Runes[Random.Range(0, 3)], pos, rot);
        NetworkServer.Spawn(myInstantiatedObject);
        //RpcSpawner();
    }

    [ClientRpc]
    void RpcSpawner()
    {
        
    }
        
}
