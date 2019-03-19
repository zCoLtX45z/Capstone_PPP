using Photon.Pun;
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
    [SerializeField]
    private Collider[] DisableColliders;
    [SerializeField]
    private MeshRenderer[] MeshRenderers;
    [SerializeField]
    private BoxCollider PlacingCollider;
    public Vector3 BoxSize;
    public Vector3 BoxOffset;

    // Item ID
    [SerializeField]
    private string ItemID;

    public int ItemType = -1;

    public bool isPlacing = true;

    private void Update()
    {
        if (!isPlacing)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                Debug.Log("!isPlacing");
                FindObjectOfType<ItemManager>().AddItemToList(ItemType);
            }
            isPlacing = true;
        }
    }

    public string GetITemID()
    {
        return ItemID;
    }

    public BoxCollider GetCollider()
    {
        return PlacingCollider;
    }

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
        foreach (Collider c in DisableColliders)
        {
            c.enabled = false;
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

    public void ChangeMaterials(Material mat)
    {
        foreach (MeshRenderer MR in MeshRenderers)
        {
            MR.material = mat;
        }
    }
}
