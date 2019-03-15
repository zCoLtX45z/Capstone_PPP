using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RuneSpawnQueue : MonoBehaviour {

    [SerializeField]
    private Rune RunePrefab;
    [HideInInspector]
    public Queue<Rune> SpawnedRunes = new Queue<Rune>();

    private GameObject currentSpawnedRune;

    private bool runeDisabled;

    public void SpawnRune()
    {
        currentSpawnedRune = PhotonNetwork.Instantiate(RunePrefab.GetRuneID(), transform.position, transform.rotation);
        Rune temp = currentSpawnedRune.GetComponent<Rune>();
        temp.parentRuneSpawn = this;
        SpawnedRunes.Enqueue(temp);
        runeDisabled = false;
    }

    // not being called
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

    public void CallReActivateRune()
    {
        Invoke("ReActivateSpawnedRune", 7);
    }

   public void ReActivateSpawnedRune()
    {
        currentSpawnedRune.SetActive(true);
    }

}
