using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour {

    public float minDistance = 1.0f;
    public float maxDistance = 4.0f;
    public float smoothTransitionSpeed = 10.0f;

    [SerializeField]
    private float distanceMultiplier;

    Vector3 dollyDir;
    public Vector3 dollyDirAdjusted;
    public float distance;


    [SerializeField]
    private LayerMask layerMask;

    

	// Use this for initialization
	void Start () {
        dollyDir = transform.localPosition.normalized;
        distance = transform.localPosition.magnitude;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 desiredCameraPos = transform.parent.TransformPoint(dollyDir * maxDistance);

        RaycastHit hit;

        if(Physics.Linecast(transform.parent.position, desiredCameraPos, out hit, layerMask))
        {
            if (hit.transform.tag != "Shield" && hit.transform.tag != "Team 1" && hit.transform.tag != "Team 2")
            {
                distance = Mathf.Clamp((hit.distance * distanceMultiplier), minDistance, maxDistance);
            }
        }
        else
        {
            distance = maxDistance;
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDir * distance, Time.deltaTime * smoothTransitionSpeed);
	}
}
