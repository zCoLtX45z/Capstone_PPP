using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ballSpawner : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform ballSpawn;
    public float timer;
    private bool spawned = false;
	// Use this for initialization
	void Start ()
    {
        timer = 5;
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer -= Time.deltaTime;
        if(timer < 0 && !spawned)
        {
            SpawnBall();
            spawned = true;
        }
	}

    public void SpawnBall()
    {
        GameObject tempBall = Instantiate(ballPrefab, ballSpawn.position, ballSpawn.rotation);
        tempBall.transform.parent = null;
        //NetworkServer.Spawn(tempBall);
    }
}
