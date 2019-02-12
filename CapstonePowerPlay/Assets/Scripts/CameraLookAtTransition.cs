using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraLookAtTransition : MonoBehaviour {


    private Transform playerLookAtPoint;


    private Transform ballLookAtPoint;


    private bool lookAtBall;


    private CinemachineFreeLook vFreeCam;


    private CinemachineCollider collCineMachine;


    private void Start()
    {
        lookAtBall = false;
        vFreeCam = transform.GetComponent<CinemachineFreeLook>();
        collCineMachine = transform.GetComponent<CinemachineCollider>();
        ballLookAtPoint = GameObject.FindGameObjectWithTag("Ball").transform;
        playerLookAtPoint = vFreeCam.LookAt;
    }


    // Update is called once per frame
    void Update () {

        if(Input.GetKeyDown(KeyCode.C))
        {
            Switch(lookAtBall);
        }

        if(lookAtBall)
        {
            if (collCineMachine.m_Strategy != CinemachineCollider.ResolutionStrategy.PreserveCameraDistance)
            {
                vFreeCam.m_XAxis.m_InputAxisName = "";
                vFreeCam.m_YAxis.m_InputAxisName = "";
                collCineMachine.m_Strategy = CinemachineCollider.ResolutionStrategy.PreserveCameraDistance;
            }



            //Vector3 inverseTPoint = transform.InverseTransformPoint(transform.position + hit.normal);
            //float angleX = Mathf.Atan2(inverseTPoint.z, inverseTPoint.y) * Mathf.Rad2Deg;
            //transform.Rotate(angleX, 0, 0);

            //inverseTPoint = transform.InverseTransformPoint(transform.position + hit.normal);

            //float angleZ = -Mathf.Atan2(inverseTPoint.x, inverseTPoint.y) * Mathf.Rad2Deg;
            //transform.Rotate(0, 0, angleZ);






            Vector3 directionFromPlayer = ((ballLookAtPoint.position - playerLookAtPoint.position));

            // this works if player stays on the ground
            //directionFromPlayer = new Vector3((directionFromPlayer.x / directionFromPlayer.y), 0, directionFromPlayer.z / directionFromPlayer.y);

            float dX = directionFromPlayer.x * playerLookAtPoint.up.x;
            float dY = directionFromPlayer.y * playerLookAtPoint.up.y;
            float dZ = directionFromPlayer.z * playerLookAtPoint.up.z;

           // Vector3 crossVec =  Vector3.Cross(directionFromPlayer, playerLookAtPoint.up);

            //if (dX < 0)
            //{
            //    dX = -dX;
            //}

            //if (dY < 0)
            //{
            //    dY = -dY;
            //}

            //if (dZ < 0)
            //{
            //    dZ = -dZ;
            //}


            //directionFromPlayer = new Vector3((directionFromPlayer.x / dY / dZ), (directionFromPlayer.y / dX / dZ), (directionFromPlayer.z / dX / dY));

            directionFromPlayer = new Vector3((directionFromPlayer.x - dX), (directionFromPlayer.y - dY), (directionFromPlayer.z - dZ));

            //Debug.Log("lookAt: forward: " + playerLookAtPoint.forward);

            float angle = Vector3.Angle(directionFromPlayer, playerLookAtPoint.forward);
            //Debug.Log("Angle: " + angle);

            Vector3 cross = Vector3.Cross(directionFromPlayer.normalized, playerLookAtPoint.forward);



            if (cross.y > 0)
                angle = -angle;

            vFreeCam.m_XAxis.Value = angle;

            //float dotProd = Vector3.Dot(playerLookAtPoint.right, directionFromPlayer);

            //if (dotProd < 0)
            //{
            //    vFreeCam.m_XAxis.Value = -angle;
            //}
            //else
            //{
            //    vFreeCam.m_XAxis.Value = angle;
            //}
        }
        else
        {
            if (collCineMachine.m_Strategy != CinemachineCollider.ResolutionStrategy.PreserveCameraHeight)
            {
                vFreeCam.m_XAxis.m_InputAxisName = "Mouse X";
                vFreeCam.m_YAxis.m_InputAxisName = "Mouse Y";
                collCineMachine.m_Strategy = CinemachineCollider.ResolutionStrategy.PreserveCameraHeight;
            }
        }
    }

    public void Switch(bool lookAtBool)
    {
        lookAtBall = !lookAtBool;
        //if (lookAtBall)
        //    vFreeCam.LookAt = ballLookAtPoint;
        //else
        //    vFreeCam.LookAt = playerLookAtPoint;
    }
}
