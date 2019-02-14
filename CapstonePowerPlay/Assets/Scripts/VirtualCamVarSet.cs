using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VirtualCamVarSet : MonoBehaviour {

    [SerializeField]
    private float positionDampingFreeCam;

    [SerializeField]
    private float rotationDampingFreeCam;


    //


    [SerializeField]
    private float positionDampingBallCam;

    [SerializeField]
    private float rotationDampingBallCam;

    //[SerializeField]
    //private float lookAtSoftDampingHeightFreeCam;

    //[SerializeField]
    //private float lookAtSoftDampingWidthFreeCam;

    //[SerializeField]
    //private float lookAtHardDampingHeightFreeCam;

    //[SerializeField]
    //private float lookAtHardDampingWidthFreeCam;

    [SerializeField]
    private Transform pCameraObject;
    
    [SerializeField]
    private LayerMask lMask;

    [SerializeField]
    private Transform lookAtTransform;

    // rig transposer 0
    CinemachineTransposer transposerVirtualCamera_Rig0;
    // rig transposer 1
    CinemachineTransposer transposerVirtualCamera_Rig1;
    // rig transposer 2
    CinemachineTransposer transposerVirtualCamera_Rig2;

    CinemachineFreeLook freeLookVirtualCamera;



    [SerializeField]
    private float[] heightsFree;

    [SerializeField]
    private float[] radiusFree;


    [SerializeField]
    private float[] heightsBall;

    [SerializeField]
    private float[] radiusBall;


    // Use this for initialization
    void Start () {
        SetFreeCamVars();



        
        freeLookVirtualCamera = transform.GetComponent<CinemachineFreeLook>();


        transposerVirtualCamera_Rig0 = freeLookVirtualCamera.GetRig(0).GetCinemachineComponent<CinemachineTransposer>();

       
       
        transposerVirtualCamera_Rig1 = freeLookVirtualCamera.GetRig(1).GetCinemachineComponent<CinemachineTransposer>();

        
        transposerVirtualCamera_Rig2 = freeLookVirtualCamera.GetRig(2).GetCinemachineComponent<CinemachineTransposer>();

        // un child pCamera
        pCameraObject.parent = null;

        gameObject.AddComponent<CinemachineCollider>().m_CollideAgainst = lMask;
    }


    public void SetFreeCamVars()
    {
        


        



        //// Top Rig
        // position damping
        transposerVirtualCamera_Rig0.m_XDamping = positionDampingFreeCam;
        transposerVirtualCamera_Rig0.m_YDamping = positionDampingFreeCam;
        transposerVirtualCamera_Rig0.m_ZDamping = positionDampingFreeCam;

        //rotation damping
        transposerVirtualCamera_Rig0.m_PitchDamping = rotationDampingFreeCam;
        transposerVirtualCamera_Rig0.m_YawDamping = rotationDampingFreeCam;
        transposerVirtualCamera_Rig0.m_RollDamping = rotationDampingFreeCam;

       




        //// look at damping
        //freeLookVirtualCamera.GetRig(0).GetCinemachineComponent<CinemachineComposer>().m_SoftZoneHeight = lookAtSoftDampingHeight;
        //freeLookVirtualCamera.GetRig(0).GetCinemachineComponent<CinemachineComposer>().m_SoftZoneWidth = lookAtSoftDampingWidth;

        //freeLookVirtualCamera.GetRig(0).GetCinemachineComponent<CinemachineComposer>().m_DeadZoneHeight = lookAtHardDampingHeight;
        //freeLookVirtualCamera.GetRig(0).GetCinemachineComponent<CinemachineComposer>().m_DeadZoneWidth = lookAtHardDampingWidth;

        //// Middle Rig
        //position damping
        transposerVirtualCamera_Rig1.m_XDamping = positionDampingFreeCam;
        transposerVirtualCamera_Rig1.m_YDamping = positionDampingFreeCam;
        transposerVirtualCamera_Rig1.m_ZDamping = positionDampingFreeCam;

        //rotation damping
        transposerVirtualCamera_Rig1.m_PitchDamping = rotationDampingFreeCam;
        transposerVirtualCamera_Rig1.m_YawDamping = rotationDampingFreeCam;
        transposerVirtualCamera_Rig1.m_RollDamping = rotationDampingFreeCam;

        //// look at damping
        //freeLookVirtualCamera.GetRig(1).GetCinemachineComponent<CinemachineComposer>().m_SoftZoneHeight = lookAtSoftDampingHeight;
        //freeLookVirtualCamera.GetRig(1).GetCinemachineComponent<CinemachineComposer>().m_SoftZoneWidth = lookAtSoftDampingWidth;

        //freeLookVirtualCamera.GetRig(1).GetCinemachineComponent<CinemachineComposer>().m_DeadZoneHeight = lookAtHardDampingHeight;
        //freeLookVirtualCamera.GetRig(1).GetCinemachineComponent<CinemachineComposer>().m_DeadZoneWidth = lookAtHardDampingWidth;

        //// Bottom Rig
        // position damping
        transposerVirtualCamera_Rig2.m_XDamping = positionDampingFreeCam;
        transposerVirtualCamera_Rig2.m_YDamping = positionDampingFreeCam;
        transposerVirtualCamera_Rig2.m_ZDamping = positionDampingFreeCam;

        //rotation damping
        transposerVirtualCamera_Rig2.m_PitchDamping = rotationDampingFreeCam;
        transposerVirtualCamera_Rig2.m_YawDamping = rotationDampingFreeCam;
        transposerVirtualCamera_Rig2.m_RollDamping = rotationDampingFreeCam;

        //// look at damping
        //freeLookVirtualCamera.GetRig(2).GetCinemachineComponent<CinemachineComposer>().m_SoftZoneHeight = lookAtSoftDampingHeight;
        //freeLookVirtualCamera.GetRig(2).GetCinemachineComponent<CinemachineComposer>().m_SoftZoneWidth = lookAtSoftDampingWidth;

        //freeLookVirtualCamera.GetRig(2).GetCinemachineComponent<CinemachineComposer>().m_DeadZoneHeight = lookAtHardDampingHeight;
        //freeLookVirtualCamera.GetRig(2).GetCinemachineComponent<CinemachineComposer>().m_DeadZoneWidth = lookAtHardDampingWidth;

        //freeLookVirtualCamera.GetRig(2).GetCinemachineComponent<CinemachineComposer>().m_DeadZoneWidth = 0.1f;


        //// orbit
        //top
        freeLookVirtualCamera.m_Orbits[0].m_Height = heightsFree[0];
        freeLookVirtualCamera.m_Orbits[0].m_Radius = radiusFree[0];
        // middle
        freeLookVirtualCamera.m_Orbits[1].m_Height = heightsFree[1];
        freeLookVirtualCamera.m_Orbits[1].m_Radius = radiusFree[1];
        // bottom
        freeLookVirtualCamera.m_Orbits[2].m_Height = heightsFree[2];
        freeLookVirtualCamera.m_Orbits[2].m_Radius = radiusFree[2];
    }




    public void SetLookAtBallVar()
    {
       



        //// Top Rig
        // position damping
        transposerVirtualCamera_Rig0.m_XDamping = positionDampingBallCam;
        transposerVirtualCamera_Rig0.m_YDamping = positionDampingBallCam;
        transposerVirtualCamera_Rig0.m_ZDamping = positionDampingBallCam;

        //rotation damping
        transposerVirtualCamera_Rig0.m_PitchDamping = rotationDampingBallCam;
        transposerVirtualCamera_Rig0.m_YawDamping = rotationDampingBallCam;
        transposerVirtualCamera_Rig0.m_RollDamping = rotationDampingBallCam;

        //// look at damping
        //freeLookVirtualCamera.GetRig(0).GetCinemachineComponent<CinemachineComposer>().m_SoftZoneHeight = lookAtSoftDampingHeight;
        //freeLookVirtualCamera.GetRig(0).GetCinemachineComponent<CinemachineComposer>().m_SoftZoneWidth = lookAtSoftDampingWidth;

        //freeLookVirtualCamera.GetRig(0).GetCinemachineComponent<CinemachineComposer>().m_DeadZoneHeight = lookAtHardDampingHeight;
        //freeLookVirtualCamera.GetRig(0).GetCinemachineComponent<CinemachineComposer>().m_DeadZoneWidth = lookAtHardDampingWidth;

        //// Middle Rig
        //position damping
        transposerVirtualCamera_Rig1.m_XDamping = positionDampingBallCam;
        transposerVirtualCamera_Rig1.m_YDamping = positionDampingBallCam;
        transposerVirtualCamera_Rig1.m_ZDamping = positionDampingBallCam;

        //rotation damping
        transposerVirtualCamera_Rig1.m_PitchDamping = rotationDampingBallCam;
        transposerVirtualCamera_Rig1.m_YawDamping = rotationDampingBallCam;
        transposerVirtualCamera_Rig1.m_RollDamping = rotationDampingBallCam;

        //// look at damping
        //freeLookVirtualCamera.GetRig(1).GetCinemachineComponent<CinemachineComposer>().m_SoftZoneHeight = lookAtSoftDampingHeight;
        //freeLookVirtualCamera.GetRig(1).GetCinemachineComponent<CinemachineComposer>().m_SoftZoneWidth = lookAtSoftDampingWidth;

        //freeLookVirtualCamera.GetRig(1).GetCinemachineComponent<CinemachineComposer>().m_DeadZoneHeight = lookAtHardDampingHeight;
        //freeLookVirtualCamera.GetRig(1).GetCinemachineComponent<CinemachineComposer>().m_DeadZoneWidth = lookAtHardDampingWidth;

        //// Bottom Rig
        // position damping
        transposerVirtualCamera_Rig2.m_XDamping = positionDampingBallCam;
        transposerVirtualCamera_Rig2.m_YDamping = positionDampingBallCam;
        transposerVirtualCamera_Rig2.m_ZDamping = positionDampingBallCam;

        //rotation damping
        transposerVirtualCamera_Rig2.m_PitchDamping = rotationDampingBallCam;
        transposerVirtualCamera_Rig2.m_YawDamping = rotationDampingBallCam;
        transposerVirtualCamera_Rig2.m_RollDamping = rotationDampingBallCam;

        //// look at damping
        //freeLookVirtualCamera.GetRig(2).GetCinemachineComponent<CinemachineComposer>().m_SoftZoneHeight = lookAtSoftDampingHeight;
        //freeLookVirtualCamera.GetRig(2).GetCinemachineComponent<CinemachineComposer>().m_SoftZoneWidth = lookAtSoftDampingWidth;

        //freeLookVirtualCamera.GetRig(2).GetCinemachineComponent<CinemachineComposer>().m_DeadZoneHeight = lookAtHardDampingHeight;
        //freeLookVirtualCamera.GetRig(2).GetCinemachineComponent<CinemachineComposer>().m_DeadZoneWidth = lookAtHardDampingWidth;

        //freeLookVirtualCamera.GetRig(2).GetCinemachineComponent<CinemachineComposer>().m_DeadZoneWidth = 0.1f;


        //// orbit
        //top
        freeLookVirtualCamera.m_Orbits[0].m_Height = heightsBall[0];
        freeLookVirtualCamera.m_Orbits[0].m_Radius = radiusBall[0];
        // middle
        freeLookVirtualCamera.m_Orbits[1].m_Height = heightsBall[1];
        freeLookVirtualCamera.m_Orbits[1].m_Radius = radiusBall[1];
        // bottom
        freeLookVirtualCamera.m_Orbits[2].m_Height = heightsBall[2];
        freeLookVirtualCamera.m_Orbits[2].m_Radius = radiusBall[2];


    }



}
