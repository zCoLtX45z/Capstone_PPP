using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfluenceRotation : MonoBehaviour {

    
    // restricts rotation of child y axis
    private Transform lookatPoint;


	// Use this for initialization
	void Start () {
		if(lookatPoint == null)
        {
            lookatPoint = transform.GetChild(0);
        }
    }

    // Update is called once per frame
    void Update () {

        //Vector3 dir =child.position - (child.position + child.up);
        //child.up = Vector3.Cross(dir, child.up);

        //if (lookatPoint != null)
        //lookatPoint.up = Vector3.Normalize(Vector3.Cross(transform.forward, transform.right));


        //float x = ((transform.forward.y * transform.right.z) - (transform.forward.z * transform.right.y));
        //float y = ((transform.forward.z * transform.right.x) - (transform.forward.x * transform.right.z));
        //float z = ((transform.forward.x * transform.right.y) - (transform.forward.y * transform.right.x));

        //lookatPoint.up = new Vector3(Mathf.Clamp(x, -1, 1), Mathf.Clamp(y, -1, 1), Mathf.Clamp(z, -1, 1)).normalized;
        //Debug.Log("lookatPoint.up: " + lookatPoint.up);


        //if (lookatPoint.up.y <= -0.8f)
        //    lookatPoint.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, transform.rotation.eulerAngles.z);

        lookatPoint.up = Vector3.Cross(transform.forward, transform.right);

        //lookatPoint.localEulerAngles = new Vector3(lookatPoint.localEulerAngles.x, Vector3.Dot(transform.forward, transform.right), lookatPoint.localEulerAngles.z);


        //lookatPoint.up = new Vector3(Mathf.Clamp(lookatPoint.up.x, -1, 1), Mathf.Clamp(lookatPoint.up.y, -1, 1), Mathf.Clamp(lookatPoint.up.z, -1, 1));
        //lookatPoint.right = new Vector3(Mathf.Clamp(lookatPoint.right.x, -1, 1), Mathf.Clamp(lookatPoint.right.y, -1, 1), Mathf.Clamp(lookatPoint.right.z, -1, 1));
        //lookatPoint.forward = new Vector3(Mathf.Clamp(lookatPoint.forward.x, -1, 1), Mathf.Clamp(lookatPoint.forward.y, -1, 1), Mathf.Clamp(lookatPoint.forward.z, -1, 1));
        //lookatPoint.localRotation = Quaternion.Euler(lookatPoint.localulerAngles)

        //lookatPoint.rotation = Quaternion.LookRotation(transform.forward);

        //lookatPoint.up = Vector3.ProjectOnPlane(transform.forward, transform.right).normalized;

    }
}
