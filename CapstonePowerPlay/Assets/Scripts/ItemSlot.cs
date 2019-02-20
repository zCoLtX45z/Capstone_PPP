using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour {

    [SerializeField]
    private Item HeldItem = null;
    //[HideInInspector]
    public bool ItemHeld = false;


    [SerializeField]
    private Texture NullSprite;
    [SerializeField]
    private RawImage ImageSlot;

    public void SetImage(Texture sprite)
    {
        ImageSlot.texture = sprite;
    }

    public void SetImage()
    {
        ImageSlot.texture = NullSprite;
    }

    public void SetItem(Item item)
    {
        HeldItem = item;
        SetImage(item.GetSprite());
        if (HeldItem != null)
            ItemHeld = true;
    }

    public Item GetItemRef()
    {
        return HeldItem;
    }

    public void RemoveItem()
    {
        SetImage();
        HeldItem = null;
        ItemHeld = false;
    }
}
