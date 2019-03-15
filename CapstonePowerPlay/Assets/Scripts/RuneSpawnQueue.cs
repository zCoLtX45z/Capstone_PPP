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
        for (int i = 0; i < RunePrefabs.Length; i++)
        {
            SpawnedRunes.Add(PhotonNetwork.Instantiate(RunePrefabs[i].GetRuneID(), transform.position, transform.rotation).GetComponent<Rune>());
            SpawnedRunes[i].GetComponent<Rune>().parentRuneSpawn = this;
            SpawnedRunes[i].gameObject.SetActive(false);
        }
        AcivateRandomRune();
    }

    public void CallAcivateRandomRune()
    {
        Invoke("AcivateRandomRune", 7);
    }


    public void AcivateRandomRune()
    {
        int rng = Random.Range(0, SpawnedRunes.Count);
        SpawnedRunes[rng].gameObject.SetActive(true);
    }
}
