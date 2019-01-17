using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPostionFollow : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    private Transform lookatPoint;

    [SerializeField]
    private Transform board;

    [SerializeField]
    private LayerMask layerMask;

	void Start () {
        transform.parent = null;	
	}
	
	// Update is called once per frame
	void Update () {

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(board.position, board.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask))
        {
            //Debug.Log("Hit: " + hit.transform.name);
           // Debug.DrawRay(transform.position, -transform.up, Color.magenta, Mathf.Infinity);
            transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
        }

        transform.position = lookatPoint.position;

        //transform.rotation = Quaternion.FromToRotation(Vector3.up, transform.);



        //transform.rotation = Quaternion.FromToRotation(Vector3.up, lookatPoint.up);




        //transform.rotation = Quaternion.FromToRotation(Vector3.up, lookatPoint.up);

        //transform.up = Vector3.Cross(lookatPoint.forward, lookatPoint.right).normalized;

        //if(transform.up.x == 0 || transform.up.z == 0)
        //{
        //    transform.rotation = Quaternion.Euler(transform.rotation.x, 0, 0);
        //}

        //transform.rotation = Quaternion.AngleAxis(0, lookatPoint.up);
    }
}
