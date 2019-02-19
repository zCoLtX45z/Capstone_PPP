using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour {

    [SerializeField]
    private Mesh ItemMesh;
    [SerializeField]
    private Texture ItemSprite;

    public Mesh GetMesh()
    {
        return ItemMesh;
    }

    public Texture GetSprite()
    {
        return ItemSprite;
    }

    public void Place(Vector3 pos, Vector3 normal)
    {
        transform.position = pos;
        transform.up = normal;
    }
}
