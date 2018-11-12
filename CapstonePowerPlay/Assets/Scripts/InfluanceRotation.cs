using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfluanceRotation : MonoBehaviour {

    //[SerializeField]
    //private Transform referenceXZ;
    //[SerializeField]
    //private Transform referenceY;

    [SerializeField]
    private Transform child; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //transform.eulerAngles = new Vector3(referenceXZ.eulerAngles.x, 0, referenceXZ.eulerAngles.z);
        //referenceY.eulerAngles = new Vector3(0, referenceXZ.eulerAngles.y, 0);
        child.localEulerAngles = new Vector3(child.localEulerAngles.x, -transform.root.eulerAngles.y, child.localEulerAngles.z);
        

    }
}
