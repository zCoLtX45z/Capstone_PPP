using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Rune : MonoBehaviour {

    [SerializeField]
    private Item ItemPrefab;
    [SerializeField]
    private PhotonView PV;
    [SerializeField]
    private string RuneID;

    public RuneSpawnQueue parentRuneSpawn;

    public string GetRuneID()
    {
        return RuneID;
    }

    public Item GetItemPrefab()
    {
        return ItemPrefab;
    }

    public void DestroyRune()
    {
        PhotonNetwork.Destroy(PV);
    }

    public void TurnOffRune()
    {
        PV.RPC("RPC_TurnOffRune", RpcTarget.All);
    }

    [PunRPC]
    void RPC_TurnOffRune()
    {
        gameObject.SetActive(false);
        parentRuneSpawn.CallAcivateRandomRune();
    }
}
