using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FollowPlayer : NetworkBehaviour {

    [SerializeField]
    private float MaxYOffset = 2;

    [SerializeField]
    private ThirdPersonController player;
    [SerializeField]
    private Transform playerFeet;
    private Vector2 displacement;
    private float distance;

	// Use this for initialization
	void Start () {
        transform.position = playerFeet.position;
        transform.rotation = playerFeet.rotation;
        transform.Translate(0, -MaxYOffset, 0);
	}
	
	// Update is called once per frame
	void Update () {

        distance = playerFeet.position.y - transform.position.y;
        displacement = new Vector2(playerFeet.position.x - transform.position.x, playerFeet.position.x - transform.position.x);
        //Debug.Log("Distance: " + distance + ", Displacement: " + displacement);
        if (distance > MaxYOffset)
            distance = MaxYOffset;
        if (displacement.magnitude > 0.2)
        {
            transform.position = playerFeet.position;
            transform.Translate(0, -distance, 0);
        }
	}

    public void SetPlayer(ThirdPersonController Player)
    {
        player = Player;
    }

    public void SetFeet(Transform Feet)
    {
        playerFeet = Feet;
    }
}
