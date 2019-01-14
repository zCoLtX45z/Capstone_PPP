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

        Debug.Log("up: " + transform.up);

        

       // float yRotationCounter = (child.up.x + child.up.y + child.up.z);

        //child.eulerAngles = new Vector3(child.eulerAngles.x, 0, child.eulerAngles.z);

       // child.rotation = Quaternion.Euler(transform.root.rotation.x, transform.root.rotation.y, transform.root.rotation.z);

        //// the smae rot
        //float x = ((transform.up.x * child.up.x) + (transform.right.x * child.right.x) + (transform.forward.x * child.forward.x) *
        //    (transform.up.x * child.up.y) + (transform.right.x * child.right.y) + (transform.forward.x * child.forward.y) *
        //    (transform.up.x * child.up.z) + (transform.right.x * child.right.z) + (transform.forward.x * child.forward.z));

        //float y = ((transform.up.y * child.up.x) + (transform.right.y * child.right.x) + (transform.forward.y * child.forward.x) *
        //    (transform.up.y * child.up.y) + (transform.right.y * child.right.y) + (transform.forward.y * child.forward.y) *
        //    (transform.up.y* child.up.z) + (transform.right.y * child.right.z) + (transform.forward.y * child.forward.z));

        //float z = ((transform.up.z * child.up.x) + (transform.right.z * child.right.x) + (transform.forward.z * child.forward.x) *
        //   (transform.up.z * child.up.y) + (transform.right.z * child.right.y) + (transform.forward.z * child.forward.y) *
        //   (transform.up.z * child.up.z) + (transform.right.z * child.right.z) + (transform.forward.z * child.forward.z));
        ////
        

        //child.localEulerAngles = new Vector3(transform.up.x, /**/(-transform.eulerAngles.y *child.up.y) + (child.localEulerAngles.z * child.up.y) + (child.localEulerAngles.x * child.up.y)/**/, transform.up.z);
        // child.up.y
        //child.localEulerAngles = new Vector3(child.localEulerAngles.x, -transform.root.eulerAngles.y, child.localEulerAngles.z);

        //child.localEulerAngles = new Vector3(child.localEulerAngles.x * transform.root.eulerAngles.x, -transform.root.eulerAngles.y * child.up.y, child.localEulerAngles.z * -transform.root.eulerAngles.z);
    }
}
