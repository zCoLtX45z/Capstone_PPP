using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPostionFollow : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    private Transform lookatPoint;

    [SerializeField]
    private Transform player;

    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private float speedTransation;

	void Start () {
        transform.parent = null;	
	}
	
	// Update is called once per frame
	void Update () {

        float distance = (lookatPoint.position - transform.position).magnitude;


        transform.position = lookatPoint.position;

        //transform.position = Vector3.MoveTowards(transform.position, lookatPoint.position, speedTransation * distance);

        RaycastHit hit;

        if (Physics.Raycast(player.position, player.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask))
        {
            Vector3 inverseTPoint = transform.InverseTransformPoint(transform.position + hit.normal);
            float angleX = Mathf.Atan2(inverseTPoint.z, inverseTPoint.y) * Mathf.Rad2Deg;
            transform.Rotate(angleX, 0, 0);

            inverseTPoint = transform.InverseTransformPoint(transform.position + hit.normal);

            float angleZ = -Mathf.Atan2(inverseTPoint.x, inverseTPoint.y) * Mathf.Rad2Deg;
            transform.Rotate(0, 0, angleZ);

        }
    }
}
