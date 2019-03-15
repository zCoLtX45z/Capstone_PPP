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

  

    public void SpawnRune()
    {
        /*
        currentSpawnedRune = PhotonNetwork.Instantiate(RunePrefab.GetRuneID(), transform.position, transform.rotation);
        Rune temp = currentSpawnedRune.GetComponent<Rune>();
        temp.parentRuneSpawn = this;
        SpawnedRunes.Enqueue(temp);
        runeDisabled = false;
        */
        for (int i = 0; i < RunePrefabs.Length; i++)
        {
            SpawnedRunes.Add(PhotonNetwork.Instantiate(RunePrefabs[i].GetRuneID(), transform.position, transform.rotation).GetComponent<Rune>());
            SpawnedRunes[i].GetComponent<Rune>().parentRuneSpawn = this;
            SpawnedRunes[i].gameObject.SetActive(false);
        }
        AcivateRandomRune();
    }

    

    /*
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
    */

    public void CallAcivateRandomRune()
    {
        Invoke("AcivateRandomRune", 7);
    }


    public void AcivateRandomRune()
    {
        //Debug.Log("activate");
        int rng = Random.Range(0, SpawnedRunes.Count);
        //Debug.Log("rng: " + rng);
        SpawnedRunes[rng].gameObject.SetActive(true);
    }
}
