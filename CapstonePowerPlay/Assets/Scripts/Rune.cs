using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rune : MonoBehaviour {

    [SerializeField]
    private Item ItemPrefab;

    public Item GetItemPrefab()
    {
        return ItemPrefab;
    }
}
