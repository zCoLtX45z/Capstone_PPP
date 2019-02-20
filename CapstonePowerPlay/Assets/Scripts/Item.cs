using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour {

    private Mesh ItemMesh;
    [SerializeField]
    private MeshFilter ItemMeshFilter;
    [SerializeField]
    private Texture ItemSprite;

    public Mesh GetMesh()
    {
        ItemMesh = ItemMeshFilter.sharedMesh;
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
