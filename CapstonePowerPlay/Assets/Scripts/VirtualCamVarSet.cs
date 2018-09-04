using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VirtualCamVarSet : MonoBehaviour {

    private CinemachineFreeLook freeLookVirtualCamera;

    private float damping;

	// Use this for initialization
	void Start () {
        freeLookVirtualCamera = GetComponent<CinemachineFreeLook>();
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
    }


}
