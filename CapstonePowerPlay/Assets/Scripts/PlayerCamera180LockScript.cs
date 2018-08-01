using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerCamera180LockScript : MonoBehaviour
{


    private Cinemachine.CinemachineFreeLook playerVirtualCamera;

  

    // Use this for initialization
    void Start()
    {
        playerVirtualCamera = transform.GetComponent<Cinemachine.CinemachineFreeLook>();



    }




    // Update is called once per frame
    void Update()
    {
        //if ((player))

        if (playerVirtualCamera.m_XAxis.Value >= 90 && playerVirtualCamera.m_XAxis.Value <= 270)
        {
            if (playerVirtualCamera.m_XAxis.Value >= 90 && playerVirtualCamera.m_XAxis.Value < 181)
            {

                //90
                playerVirtualCamera.m_XAxis.Value = 90.0f;
                
            }


            else if (playerVirtualCamera.m_XAxis.Value <= 270 && playerVirtualCamera.m_XAxis.Value > 179)
            {
                // 270
                playerVirtualCamera.m_XAxis.Value = 270.0f;
                // Debug.Log("Entered2");
            }
        }


        ////////////////////////////////////////////////////////


    }
}
