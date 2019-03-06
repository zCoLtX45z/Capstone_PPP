﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Rune : MonoBehaviour {

    [SerializeField]
    private Item ItemPrefab;
    [SerializeField]
    private PhotonView PV;

    public Item GetItemPrefab()
    {
        return ItemPrefab;
    }

    public void DestroyRune()
    {
        PhotonNetwork.Destroy(PV);
        //PV.RPC("RPC_DestroyRune", RpcTarget.All);
    }

    //[PunRPC]
    //void RPC_DestroyRune()
    //{
    //    Destroy(this.gameObject);
    //}

    public void TurnOffRune()
    {
        PV.RPC("RPC_TurnOffRune", RpcTarget.All);
    }

    [PunRPC]
    void RPC_TurnOffRune()
    {
        gameObject.SetActive(false);
    }
}
