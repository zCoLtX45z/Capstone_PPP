using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RuneSpawnQueue : MonoBehaviour {

    [SerializeField]
    private Rune[] RunePrefabs;
    [HideInInspector]
    public List<Rune> SpawnedRunes = new List<Rune>();

    private GameObject currentSpawnedRune;

    private bool runeDisabled;

    [SerializeField]
    private PhotonView PV;

    public void SpawnRune()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            for (int i = 0; i < RunePrefabs.Length; i++)
            {
                SpawnedRunes.Add(PhotonNetwork.Instantiate(RunePrefabs[i].GetRuneID(), transform.position, transform.rotation).GetComponent<Rune>());
                PV.RPC("RPC_AddToRune", RpcTarget.Others, SpawnedRunes[i].GetComponent<PhotonView>().ViewID, i);
                SpawnedRunes[i].GetComponent<Rune>().parentRuneSpawn = this;
                SpawnedRunes[i].gameObject.SetActive(false);
              
            }

            AcivateRandomRune();
        }

       
    }

    [PunRPC]
    private void RPC_AddToRune(int id, int i)
    {
        SpawnedRunes.Add(PhotonView.Find(id).GetComponent<Rune>());
        SpawnedRunes[i].GetComponent<Rune>().parentRuneSpawn = this;
        SpawnedRunes[i].gameObject.SetActive(false);
    }

        public void CallAcivateRandomRune()
    {
        if(PhotonNetwork.IsMasterClient)
            Invoke("AcivateRandomRune", 7);
    }


    private void AcivateRandomRune()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            int rng = Random.Range(0, SpawnedRunes.Count);
            PV.RPC("RPC_ActivateRandomRune", RpcTarget.All, SpawnedRunes[rng].PV.ViewID);
        }
    }

    [PunRPC]
    private void RPC_ActivateRandomRune(int id)
    {
        // PhotonView.Find(id).GetComponent<RuneSpawnQueue>().SpawnedRunes[number].gameObject.SetActive(true);
        PhotonView.Find(id).gameObject.SetActive(true);
    }

    
}
