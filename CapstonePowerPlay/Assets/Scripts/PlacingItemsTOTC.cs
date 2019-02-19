using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacingItemsTOTC : MonoBehaviour {

    // Placing Script
    [SerializeField]
    private Placing PlacingScript;

    // Player Components
    [SerializeField]
    private hoverBoardScript HBS;
    [SerializeField]
    private BallHandling BH;

    // Item Slots
    [SerializeField]
    private ItemSlot[] ItemSlots;

    // Variables
    private bool PlacingItems = false;
    private int CurrentSlot = 0;
    private int MaxSlots = 0;
    private Item ActiveItem;

	// Use this for initialization
	void Start () {
        MaxSlots = ItemSlots.Length;
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.G))
        {
            // Toggle placing items
            PlacingItems = !PlacingItems;
            // Check if the player HAS items to place
            if (PlacingItems)
            {
                bool HasItems = false;
                foreach (ItemSlot s in ItemSlots)
                {
                    if (s.ItemHeld)
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
            bool canPlace = PlacingScript.UpdatePlacement();
            BH.HasControl = false;
            if (MaxSlots > 0)
            {
                ActiveItem = ItemSlots[CurrentSlot].GetItemRef();
                PlacingScript.SetMesh(ActiveItem.GetMesh());
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
                }
            }
            else if (Input.GetMouseButtonDown(0))
            {
                // place item
                if (canPlace)
                {
                    PlacingItems = false;
                    ActiveItem = Instantiate(ActiveItem);
                    ActiveItem.transform.parent = null;
                    ActiveItem.Place(PlacingScript.ObjectPosition, PlacingScript.ObjectNormal);
                    ItemSlots[CurrentSlot].RemoveItem();
                }
            }
        }
        else
        {
            BH.HasControl = true;
        }
    }
}
