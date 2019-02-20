﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlacingItemsTOTC : NetworkBehaviour {

    // Placing Script
    [SerializeField]
    private Placing PlacingScript;
    [SerializeField]
    private Transform PlacingTransform;

    // Player Components
    [SerializeField]
    private hoverBoardScript HBS;
    [SerializeField]
    private BallHandling BH;
    [SerializeField]
    private RunePickups RP;

    // Item Slots
    [SerializeField]
    private ItemSlot[] ItemSlots;

    // Variables
    private bool PlacingItems = false;
    private int CurrentSlot = 0;
    private int MaxSlots = 0;
    private Item ActiveItem;
    [SerializeField]
    private float PickUpTime = 0.2f;
    private float PickUpTimer = 0;
    private bool PickedUpItem = false;

    // Use this for initialization
    void Start () {
        MaxSlots = ItemSlots.Length;
        foreach (ItemSlot IS in ItemSlots)
        {
            IS.SetImage();
        }
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Toggle placing items
            PlacingItems = !PlacingItems;
            // Check if the player HAS items to place
            if (PlacingItems)
            {
                bool HasItems = false;
                foreach (ItemSlot IS in ItemSlots)
                {
                    if (IS.ItemHeld)
                    {
                        HasItems = true;
                        break;
                    }
                }
                PlacingItems = HasItems;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PlacingItems = false;
        }

        if (PlacingItems)
        {
            BH.HasControl = false;
            if (MaxSlots > 0)
            {
                ActiveItem = ItemSlots[CurrentSlot].GetItemRef();
                PlacingScript.SetMesh(ActiveItem.GetMesh());
                PlacingTransform.localScale = ActiveItem.transform.localScale;
            }
            if (Input.GetMouseButtonDown(1))
            {
                // Switch Item
                CurrentSlot++;
                if (CurrentSlot == MaxSlots)
                {
                    CurrentSlot = 0;
                }
                if (ItemSlots[CurrentSlot].ItemHeld == false)
                {
                    while (ItemSlots[CurrentSlot].ItemHeld == false)
                    {
                        CurrentSlot++;
                        if (CurrentSlot == MaxSlots)
                        {
                            CurrentSlot = 0;
                        }
                    }
                }
                if (MaxSlots > 0)
                {
                    ActiveItem = ItemSlots[CurrentSlot].GetItemRef();
                    PlacingScript.SetMesh(ActiveItem.GetMesh());
                    PlacingTransform.localScale = ActiveItem.transform.localScale;
                }
            }
            else if (Input.GetMouseButtonDown(0))
            {
                // place item
                bool canPlace = PlacingScript.UpdatePlacement(ActiveItem.transform.localScale.x);
                if (canPlace)
                {
                    PlacingItems = false;
                    ActiveItem = Instantiate(ActiveItem);
                    ActiveItem.transform.parent = null;
                    ActiveItem.Place(PlacingScript.ObjectPosition, PlacingScript.ObjectNormal);
                    NetworkServer.Spawn(ActiveItem.gameObject);
                    ItemSlots[CurrentSlot].RemoveItem();
                }
            }
            PlacingScript.UpdatePlacement(ActiveItem.transform.localScale.x);
        }
        else
        {
            BH.HasControl = true;
            PlacingTransform.gameObject.SetActive(false);
        }

        if (PickedUpItem)
        {
            PickUpTimer += Time.deltaTime;
            if (PickUpTimer >= PickUpTime)
            {
                PickUpTimer = 0;
                PickedUpItem = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BlueRune" || other.gameObject.tag == "RedRune" || other.gameObject.tag == "GreenRune" || other.gameObject.tag == "Rune")
        {
            if (PickedUpItem == false)
            {
                Rune tempRune = other.GetComponent<Rune>();
                foreach (ItemSlot IS in ItemSlots)
                {
                    if (IS.ItemHeld == false)
                    {
                        PickedUpItem = true;
                        IS.SetItem(tempRune.GetItemPrefab());
                        break;
                    }
                }
                RP.CmdDestroyRune(tempRune.gameObject);
            }
        }
    }
}
