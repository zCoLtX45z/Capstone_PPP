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
    [SerializeField]
    private Behaviour[] DisableBehaviours;
    [SerializeField]
    private GameObject[] DisableObjects;

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

    public void Disable()
    {
        foreach (Behaviour b in DisableBehaviours)
        {
            b.enabled = false;
        }
        foreach (GameObject g in DisableObjects)
        {
            g.SetActive(false);
        }
    }

    public void Enable()
    {
        foreach (Behaviour b in DisableBehaviours)
        {
            b.enabled = true;
        }
        foreach (GameObject g in DisableObjects)
        {
            g.SetActive(true);
        }
    }
}
