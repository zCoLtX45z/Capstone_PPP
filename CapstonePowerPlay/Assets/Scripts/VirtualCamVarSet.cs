using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VirtualCamVarSet : MonoBehaviour {

    private CinemachineFreeLook freeLookVirtualCamera;

    

    [SerializeField]
    private float damping;

    
    [SerializeField]
    private Transform pCameraObject;
    
    [SerializeField]
    private LayerMask lMask;

    [SerializeField]
    private Transform lookAtTransform;

	// Use this for initialization
	void Start () {


       // freeLookVirtualCamera.m_Orbits.SyncRoot

        freeLookVirtualCamera = transform.GetComponent<CinemachineFreeLook>();

        pCameraObject.parent = null;

       
        damping = 0;

        // Top Rig
        freeLookVirtualCamera.GetRig(0).GetCinemachineComponent<CinemachineTransposer>().m_XDamping = damping;
        freeLookVirtualCamera.GetRig(0).GetCinemachineComponent<CinemachineTransposer>().m_YDamping = damping;
        freeLookVirtualCamera.GetRig(0).GetCinemachineComponent<CinemachineTransposer>().m_ZDamping = damping;

        freeLookVirtualCamera.GetRig(0).GetCinemachineComponent<CinemachineComposer>().m_SoftZoneHeight = 0;
        freeLookVirtualCamera.GetRig(0).GetCinemachineComponent<CinemachineComposer>().m_SoftZoneWidth = 0;
        // Middle Rig
        freeLookVirtualCamera.GetRig(1).GetCinemachineComponent<CinemachineTransposer>().m_XDamping = damping;
        freeLookVirtualCamera.GetRig(1).GetCinemachineComponent<CinemachineTransposer>().m_YDamping = damping;
        freeLookVirtualCamera.GetRig(1).GetCinemachineComponent<CinemachineTransposer>().m_ZDamping = damping;

        freeLookVirtualCamera.GetRig(1).GetCinemachineComponent<CinemachineComposer>().m_SoftZoneHeight = 0;
        freeLookVirtualCamera.GetRig(1).GetCinemachineComponent<CinemachineComposer>().m_SoftZoneWidth = 0;
        // Bottom Rig
        freeLookVirtualCamera.GetRig(2).GetCinemachineComponent<CinemachineTransposer>().m_XDamping = damping;
        freeLookVirtualCamera.GetRig(2).GetCinemachineComponent<CinemachineTransposer>().m_YDamping = damping;
        freeLookVirtualCamera.GetRig(2).GetCinemachineComponent<CinemachineTransposer>().m_ZDamping = damping;

        freeLookVirtualCamera.GetRig(2).GetCinemachineComponent<CinemachineComposer>().m_SoftZoneHeight = 0;
        freeLookVirtualCamera.GetRig(2).GetCinemachineComponent<CinemachineComposer>().m_SoftZoneWidth = 0;

        // collisions
        gameObject.AddComponent<CinemachineCollider>().m_CollideAgainst = lMask;
        
    }


}
