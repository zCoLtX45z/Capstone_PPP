using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ComponentsToDisable : MonoBehaviour {

    public Behaviour[] DisabledComponents;
    //[HideInInspector]
    public NetPlayer LocalPlayer;
    public NetPlayer ParentPlayer;
    [SerializeField]
    private PhotonView PV;
    // Use this for initialization
    private void Start()
    {
        //Debug.Log("disbaling behaviors");
        ForcedStart1();
    }
    public void ForcedStart1()
    {
        if (PhotonNetwork.InRoom)
            PV.RPC("RPC_ForcedStart", RpcTarget.AllBuffered);
    }

    [PunRPC]
	public void RPC_ForcedStart () {
		if(!PV.IsMine)
        {
            for(int i = 0; i < DisabledComponents.Length; i++)
            {
                DisabledComponents[i].enabled = false;
            }
        }


        // added by andre.
        else
        {
            gameObject.layer = 2;
        }
	}

    public void ForcedStart2()
    {
        if (LocalPlayer.PlayerCode != ParentPlayer.PlayerCode)
        {
            for (int i = 0; i < DisabledComponents.Length; i++)
            {
                DisabledComponents[i].enabled = false;
            }
        }


        // added by andre.
        else
        {
            gameObject.layer = 2;
        }
    }
}
