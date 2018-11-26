using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VirtualCamVarSet : MonoBehaviour {

    private CinemachineFreeLook freeLookVirtualCamera;

    private float damping;

    private CinemachineTransposer topRigTransposer = null;
    private CinemachineComposer topRigComposer = null;

    private CinemachineTransposer middleRigTransfposer = null;
    private CinemachineComposer middleRigComposer = null;

    private CinemachineTransposer bottomRigTransfposer = null;
    private CinemachineComposer bottomRigComposer = null;



    [SerializeField]
    private LayerMask lMask;

	// Use this for initialization
	void Start () {

        damping = 0;




        freeLookVirtualCamera = GetComponent<CinemachineFreeLook>();


        topRigTransposer = freeLookVirtualCamera.GetRig(0).GetCinemachineComponent<CinemachineTransposer>();
        topRigComposer = freeLookVirtualCamera.GetRig(0).GetCinemachineComponent<CinemachineComposer>();

        middleRigTransfposer = freeLookVirtualCamera.GetRig(1).GetCinemachineComponent<CinemachineTransposer>();
        middleRigComposer = freeLookVirtualCamera.GetRig(1).GetCinemachineComponent<CinemachineComposer>();

        bottomRigTransfposer = freeLookVirtualCamera.GetRig(2).GetCinemachineComponent<CinemachineTransposer>();
        bottomRigComposer = freeLookVirtualCamera.GetRig(0).GetCinemachineComponent<CinemachineComposer>();









        

        topRigTransposer.m_XDamping = damping;
        topRigTransposer.m_YDamping = damping;
        topRigTransposer.m_ZDamping = damping;


        topRigComposer.m_SoftZoneHeight = 0;
        topRigComposer.m_SoftZoneWidth = 0;




        middleRigTransfposer.m_XDamping = damping;
        middleRigTransfposer.m_YDamping = damping;
        middleRigTransfposer.m_ZDamping = damping;


        middleRigComposer.m_SoftZoneHeight = 0;
        middleRigComposer.m_SoftZoneWidth = 0;



        bottomRigTransfposer.m_XDamping = damping;
        bottomRigTransfposer.m_YDamping = damping;
        bottomRigTransfposer.m_ZDamping = damping;


        bottomRigComposer.m_SoftZoneHeight = 0;
        bottomRigComposer.m_SoftZoneWidth = 0;



        //// Top Rig
        //freeLookVirtualCamera.GetRig(0).GetCinemachineComponent<CinemachineTransposer>().m_XDamping = damping;
        //freeLookVirtualCamera.GetRig(0).GetCinemachineComponent<CinemachineTransposer>().m_YDamping = damping;
        //freeLookVirtualCamera.GetRig(0).GetCinemachineComponent<CinemachineTransposer>().m_ZDamping = damping;

        //freeLookVirtualCamera.GetRig(0).GetCinemachineComponent<CinemachineComposer>().m_SoftZoneHeight = 0;
        //freeLookVirtualCamera.GetRig(0).GetCinemachineComponent<CinemachineComposer>().m_SoftZoneWidth = 0;
        //// Middle Rig
        //freeLookVirtualCamera.GetRig(1).GetCinemachineComponent<CinemachineTransposer>().m_XDamping = damping;
        //freeLookVirtualCamera.GetRig(1).GetCinemachineComponent<CinemachineTransposer>().m_YDamping = damping;
        //freeLookVirtualCamera.GetRig(1).GetCinemachineComponent<CinemachineTransposer>().m_ZDamping = damping;

        //freeLookVirtualCamera.GetRig(1).GetCinemachineComponent<CinemachineComposer>().m_SoftZoneHeight = 0;
        //freeLookVirtualCamera.GetRig(1).GetCinemachineComponent<CinemachineComposer>().m_SoftZoneWidth = 0;
        //// Bottom Rig
        //freeLookVirtualCamera.GetRig(2).GetCinemachineComponent<CinemachineTransposer>().m_XDamping = damping;
        //freeLookVirtualCamera.GetRig(2).GetCinemachineComponent<CinemachineTransposer>().m_YDamping = damping;
        //freeLookVirtualCamera.GetRig(2).GetCinemachineComponent<CinemachineTransposer>().m_ZDamping = damping;

        //freeLookVirtualCamera.GetRig(2).GetCinemachineComponent<CinemachineComposer>().m_SoftZoneHeight = 0;
        //freeLookVirtualCamera.GetRig(2).GetCinemachineComponent<CinemachineComposer>().m_SoftZoneWidth = 0;

        // collisions
        gameObject.AddComponent<CinemachineCollider>().m_CollideAgainst = lMask;
        
    }


}
