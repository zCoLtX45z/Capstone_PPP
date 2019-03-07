using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RuneSpawnQueue : MonoBehaviour {

    [SerializeField]
    private Rune RunePrefab;
    [HideInInspector]
    public Queue<Rune> SpawnedRunes;

    public void SpawnRune()
    {
        GameObject GO = PhotonNetwork.Instantiate(RunePrefab.GetRuneID(), transform.position, transform.rotation);
        Rune temp = GO.GetComponent<Rune>();
        SpawnedRunes.Enqueue(temp);
    }

    public void ResetQueue()
    {
        Rune temp = SpawnedRunes.Dequeue();
        if (!temp.gameObject.activeSelf)
        {
            temp.gameObject.SetActive(true);
            SpawnedRunes.Enqueue(temp);
        }
        else
        {
            SpawnedRunes.Enqueue(temp);
        }
    }
}
