using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraLookAtTransition : MonoBehaviour {

    //[SerializeField]
    private Transform playerLookAtPoint;

    //[SerializeField]
    private Transform ballLookAtPoint;

    //[SerializeField]
    private bool lookAtBall;

    //[SerializeField]
    private CinemachineFreeLook vFreeCam;
    
    //[SerializeField]
    private CinemachineCollider collCineMachine;

    [SerializeField]
    private float transoitionAngleSpeed;


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


            Vector3 directionFromPlayer = ((ballLookAtPoint.position - playerLookAtPoint.position));

            vFreeCam.m_XAxis.Value = AngleXValueSet(directionFromPlayer);

            vFreeCam.m_YAxis.Value = AngleYValueSet(directionFromPlayer);

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

    private float AngleXValueSet(Vector3 directionFromPlayer)
    {
        //X
        float upX = playerLookAtPoint.up.x;
        float upY = playerLookAtPoint.up.y;
        float upZ = playerLookAtPoint.up.z;

        if (upX < 0)
            upX = -upX;
        if (upY < 0)
            upY = -upY;
        if (upZ < 0)
            upZ = -upZ;


        float dX = directionFromPlayer.x * upX;
        float dY = directionFromPlayer.y * upY;
        float dZ = directionFromPlayer.z * upZ;



        directionFromPlayer = new Vector3((directionFromPlayer.x - dX), (directionFromPlayer.y - dY), (directionFromPlayer.z - dZ));


        float angleX = Vector3.Angle(directionFromPlayer, playerLookAtPoint.forward);




        Vector3 nVector3 = new Vector3(playerLookAtPoint.forward.x, playerLookAtPoint.forward.y, playerLookAtPoint.forward.z);

        if (nVector3.x < 0)
            nVector3 = new Vector3(-playerLookAtPoint.forward.x, playerLookAtPoint.forward.y, playerLookAtPoint.forward.z);
        if (nVector3.y < 0)
            nVector3 = new Vector3(playerLookAtPoint.forward.x, -playerLookAtPoint.forward.y, playerLookAtPoint.forward.z);
        if (nVector3.z < 0)
            nVector3 = new Vector3(playerLookAtPoint.forward.x, playerLookAtPoint.forward.y, -playerLookAtPoint.forward.z);



        Vector3 cross = Vector3.Cross(directionFromPlayer.normalized, nVector3);


        // crossX

        

        if (cross.x > 0 && playerLookAtPoint.up.x > 0)
        {
            Debug.Log("x > 0");
            angleX = -angleX;
        }
        else if (cross.x < 0 && playerLookAtPoint.up.x < 0)
        {
            Debug.Log("x < 0");
            angleX = -angleX;
        }
        //crossY
        else if (cross.y > 0 && playerLookAtPoint.up.y > 0)
        {
            Debug.Log("y > 0");
            angleX = -angleX;
        }
        else if (cross.y < 0 && playerLookAtPoint.up.y < 0)
        {
            Debug.Log("y < 0");
            angleX = -angleX;
        }
        // crossZ
        else if (cross.z > 0 && playerLookAtPoint.up.z > 0)
        {
            Debug.Log("z > 0");
            angleX = -angleX;
        }
        else if (cross.z < 0 && playerLookAtPoint.up.z < 0)
        {
            Debug.Log("z < 0");
            angleX = -angleX;
        }
 

        return angleX;
    }


    private float AngleYValueSet(Vector3 directionFromPlayer)
    {
        // max: 1 +
        // min: 0

        /*
                                                  /|
                                                 / |
                                                /  |
                                   /\          /   | 
                                  /  \        /    |
                                 /    \      /     |
                                /      \    /      |
                               /        \  /       |
                              /          \/        |
                             <---------------------|
                              \          /\        |
                               \        /  \       |
                                \      /    \      |
                                 \    /      \     |
                                  \  /        \    |
                                   \/          \   |
                                                \  |
                                                 \ |
                                                  \| 
         */


        float rightX = playerLookAtPoint.right.x;
        float rightY = playerLookAtPoint.right.y;
        float rightZ = playerLookAtPoint.right.z;

        if (rightX < 0)
            rightX = -rightX;
        if (rightY < 0)
            rightY = -rightY;
        if (rightZ < 0)
            rightZ = -rightZ;


        float dX = directionFromPlayer.x * rightX;
        float dY = directionFromPlayer.y * rightY;
        float dZ = directionFromPlayer.z * rightZ;



        directionFromPlayer = new Vector3((directionFromPlayer.x - dX), (directionFromPlayer.y - dY), (directionFromPlayer.z - dZ));


        float angleY = Vector3.Angle(directionFromPlayer, playerLookAtPoint.up);


        float yValue = (angleY / 180);


        Debug.Log("yValue: " + yValue);

        return yValue;
    }


}
