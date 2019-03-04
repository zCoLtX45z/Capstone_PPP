using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KeepNetRotation : MonoBehaviour {

    [SerializeField]
    private Animator AN;
    //[SyncVar(hook = "UpdateRotation")]
    private bool IsRotating = false;

    private NetPlayer[] NetPlayerList;
    private hoverBoardScript[] HoverboardList;
    private PhotonView PV;
    private void Start()
    {
        PV = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update () {
        if (!IsRotating)
        {
            NetPlayerList = FindObjectsOfType<NetPlayer>();
            HoverboardList = FindObjectsOfType<hoverBoardScript>();
            if (NetPlayerList.Length > 0)
            {
                if (HoverboardList.Length > 0)
                {
                    if (NetPlayerList.Length == HoverboardList.Length)
                    {
                        if (PhotonNetwork.IsMasterClient)
                            //CmdUpdateRotation(true);
                            PV.RPC("RPC_UpdateRotation", RpcTarget.AllBuffered, IsRotating);
                    }
                }
            }
        }
        else
        {
            if (!AN.GetBool("Rotate"))
            {
                AN.SetBool("Rotate", IsRotating);
            }
        }
	}

    [PunRPC]
    private void RPC_UpdateRotation(bool rotating)
    {
        IsRotating = rotating;
    }

    private void UpdateRotation(bool rotating)
    {
        IsRotating = rotating;
    }
}
