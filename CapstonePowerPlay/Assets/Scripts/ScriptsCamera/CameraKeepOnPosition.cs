using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraKeepOnPosition : MonoBehaviour {

    [SerializeField]
    private Transform cameraLookAtPosition;

    [SerializeField]
    private float maxRotSpeed = 1;

    [SerializeField]
    private Transform player;

    private float currentRotSpeed;

    private float rotStep;

    [SerializeField]
    private float acceleration;

    [SerializeField]
    private CameraRotation camRot;
	
    public void UnParent()
    {
        transform.parent = null;
        if (player.GetComponent<PlayerColor>().TeamNum == 2)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }


	// Update is called once per frame
	void Update () {
        if (transform.parent == null)
        {
            transform.position = cameraLookAtPosition.position;

            float angle = Quaternion.Angle(transform.rotation, cameraLookAtPosition.rotation);

            currentRotSpeed = angle * acceleration;

            rotStep = currentRotSpeed * Time.deltaTime;

            transform.rotation = Quaternion.RotateTowards(transform.rotation, cameraLookAtPosition.rotation, rotStep);
        }
    }
}
