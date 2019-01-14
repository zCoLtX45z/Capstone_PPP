using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfluenceRotation : MonoBehaviour {

    
    // restricts rotation of child y axis
    private Transform child;

	// Use this for initialization
	void Start () {
		if(child == null)
        {
            child = transform.GetChild(0);
        }
	}

    RaycastHit hit;
    
	// Update is called once per frame
	void Update () {
        child.up = Vector3.Cross(transform.forward, transform.right);
    }
}
