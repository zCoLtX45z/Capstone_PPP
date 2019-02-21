using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetStart : MonoBehaviour
{
    //new spawning mechanics
    [SerializeField]
    private Transform FinalSpot;
    // Use this for initialization
    void Start ()
    {
        FinalSpot = FindObjectOfType<netSpawner>().GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        Debug.Log(FinalSpot.transform.position.y);
        if (gameObject.transform.position.y < FinalSpot.transform.position.y)
        {
            Debug.Log("moving on up");
            transform.position += Vector3.up * 1 * Time.deltaTime;
        }
    }
}
