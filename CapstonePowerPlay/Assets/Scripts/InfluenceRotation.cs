using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfluenceRotation : MonoBehaviour {

    

    [SerializeField]
    private Transform child; 

	// Use this for initialization
	void Start () {
		if(child == null)
        {
            child = transform.GetChild(0);
        }
	}
	
	// Update is called once per frame
	void Update () {
        
        child.localEulerAngles = new Vector3(child.localEulerAngles.x, -transform.root.eulerAngles.y, child.localEulerAngles.z);
        

    }
}
