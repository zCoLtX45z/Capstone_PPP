using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VirtualCamVarSet : MonoBehaviour {

    [SerializeField]
    private float positionDamping;

    [SerializeField]
    private float rotationDamping;

    [SerializeField]
    private float lookAtSoftDampingHeight;

    [SerializeField]
    private float lookAtSoftDampingWidth;

    [SerializeField]
    private float lookAtHardDampingHeight;

    [SerializeField]
    private float lookAtHardDampingWidth;



    [SerializeField]
    private Transform pCameraObject;
    
    [SerializeField]
    private LayerMask lMask;

    [SerializeField]
    private Transform lookAtTransform;

   

	// Use this for initialization
	void Start () {

        // set freeLookVirtualCamera
        CinemachineFreeLook freeLookVirtualCamera;
        freeLookVirtualCamera = transform.GetComponent<CinemachineFreeLook>();


        // rig transposer 0
        CinemachineTransposer transposerVirtualCamera_Rig0;
        transposerVirtualCamera_Rig0 = freeLookVirtualCamera.GetRig(0).GetCinemachineComponent<CinemachineTransposer>();

        // rig transposer 1
        CinemachineTransposer transposerVirtualCamera_Rig1;
        transposerVirtualCamera_Rig1 = freeLookVirtualCamera.GetRig(1).GetCinemachineComponent<CinemachineTransposer>();

        // rig transposer 2
        CinemachineTransposer transposerVirtualCamera_Rig2;
        transposerVirtualCamera_Rig2 = freeLookVirtualCamera.GetRig(2).GetCinemachineComponent<CinemachineTransposer>();

        // un child pCamera
        pCameraObject.parent = null;



        //// Top Rig
        // position damping
        transposerVirtualCamera_Rig0.m_XDamping = positionDamping;
        transposerVirtualCamera_Rig0.m_YDamping = positionDamping;
        transposerVirtualCamera_Rig0.m_ZDamping = positionDamping;

        //rotation damping
        transposerVirtualCamera_Rig0.m_PitchDamping = rotationDamping;
        transposerVirtualCamera_Rig0.m_YawDamping = rotationDamping;
        transposerVirtualCamera_Rig0.m_RollDamping = rotationDamping;

        // look at damping
        freeLookVirtualCamera.GetRig(0).GetCinemachineComponent<CinemachineComposer>().m_SoftZoneHeight = lookAtSoftDampingHeight;
        freeLookVirtualCamera.GetRig(0).GetCinemachineComponent<CinemachineComposer>().m_SoftZoneWidth = lookAtSoftDampingWidth;

        freeLookVirtualCamera.GetRig(0).GetCinemachineComponent<CinemachineComposer>().m_DeadZoneHeight = lookAtHardDampingHeight;
        freeLookVirtualCamera.GetRig(0).GetCinemachineComponent<CinemachineComposer>().m_DeadZoneWidth = lookAtHardDampingWidth;

        //// Middle Rig
        //position damping
        transposerVirtualCamera_Rig1.m_XDamping = positionDamping;
        transposerVirtualCamera_Rig1.m_YDamping = positionDamping;
        transposerVirtualCamera_Rig1.m_ZDamping = positionDamping;

        //rotation damping
        transposerVirtualCamera_Rig1.m_PitchDamping = rotationDamping;
        transposerVirtualCamera_Rig1.m_YawDamping = rotationDamping;
        transposerVirtualCamera_Rig1.m_RollDamping = rotationDamping;

        // look at damping
        freeLookVirtualCamera.GetRig(1).GetCinemachineComponent<CinemachineComposer>().m_SoftZoneHeight = lookAtSoftDampingHeight;
        freeLookVirtualCamera.GetRig(1).GetCinemachineComponent<CinemachineComposer>().m_SoftZoneWidth = lookAtSoftDampingWidth;

        freeLookVirtualCamera.GetRig(1).GetCinemachineComponent<CinemachineComposer>().m_DeadZoneHeight = lookAtHardDampingHeight;
        freeLookVirtualCamera.GetRig(1).GetCinemachineComponent<CinemachineComposer>().m_DeadZoneWidth = lookAtHardDampingWidth;

        //// Bottom Rig
        // position damping
        transposerVirtualCamera_Rig2.m_XDamping = positionDamping;
        transposerVirtualCamera_Rig2.m_YDamping = positionDamping;
        transposerVirtualCamera_Rig2.m_ZDamping = positionDamping;

        //rotation damping
        transposerVirtualCamera_Rig2.m_PitchDamping = rotationDamping;
        transposerVirtualCamera_Rig2.m_YawDamping = rotationDamping;
        transposerVirtualCamera_Rig2.m_RollDamping = rotationDamping;

        // look at damping
        freeLookVirtualCamera.GetRig(2).GetCinemachineComponent<CinemachineComposer>().m_SoftZoneHeight = lookAtSoftDampingHeight;
        freeLookVirtualCamera.GetRig(2).GetCinemachineComponent<CinemachineComposer>().m_SoftZoneWidth = lookAtSoftDampingWidth;

        freeLookVirtualCamera.GetRig(2).GetCinemachineComponent<CinemachineComposer>().m_DeadZoneHeight = lookAtHardDampingHeight;
        freeLookVirtualCamera.GetRig(2).GetCinemachineComponent<CinemachineComposer>().m_DeadZoneWidth = lookAtHardDampingWidth;

        //freeLookVirtualCamera.GetRig(2).GetCinemachineComponent<CinemachineComposer>().m_DeadZoneWidth = 0.1f;

        //// collisions
        gameObject.AddComponent<CinemachineCollider>().m_CollideAgainst = lMask;
        
    }


}
