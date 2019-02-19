using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour {

    private Item HeldItem = null;
    [HideInInspector]
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
        if (HeldItem != null)
            ItemHeld = true;
    }

    public Item GetItemRef()
    {
        return HeldItem;
    }

    public void RemoveItem()
    {
        ImageSlot.texture = NullSprite;
        HeldItem = null;
        ItemHeld = false;
    }
}
