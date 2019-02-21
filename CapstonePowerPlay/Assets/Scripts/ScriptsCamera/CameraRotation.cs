using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour {


    [SerializeField]
    private float rotSpeed;

    [HideInInspector]
    public float yaw;
    [HideInInspector]
    public float pit;

    [SerializeField]
    private Transform objectToCopy;

    private Vector3 rotRef;

    public bool allow = false;

    private float xRot = 0;
    private float yRot = 0;

    // Update is called once per frame
    void Update() {
        if (allow)
        {
            yaw = rotSpeed * Input.GetAxis("Mouse X");
            pit = (rotSpeed * -Input.GetAxis("Mouse Y"));

            rotRef += (new Vector3(pit, yaw, 0));
            rotRef = new Vector3(Mathf.Clamp(rotRef.x, -80, 80), rotRef.y, rotRef.z);

            //Debug.Log("rotRef: " + rotRef);
            transform.localEulerAngles = rotRef + new Vector3(xRot,yRot,0);
        }
    }

    public void GrabRot()
    {
        //Debug.Log("GrabRot");
        yaw = 0;
        pit = 0;
        xRot = transform.localEulerAngles.x;
        yRot = transform.localEulerAngles.y;

        rotRef = Vector3.zero;
    }

}
