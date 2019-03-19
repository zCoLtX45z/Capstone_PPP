using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlacingItemsTOTC : MonoBehaviour
{

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
    [SerializeField]
    private PlayerColor PC;
    private PlayerVoiceLine PVL;

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
    //photon variables
    private PhotonView PV;

    private ItemManager IM;

    // Use this for initialization
    void Start ()
    {
        PVL = FindObjectOfType<PlayerVoiceLine>();
        PV = GetComponent<PhotonView>();
        MaxSlots = ItemSlots.Length;
        foreach (ItemSlot IS in ItemSlots)
        {
            IS.SetImage();
        }
        IM = FindObjectOfType<ItemManager>();
    }
	
	// Update is called once per frame
	void Update () {

        if (PC.LocalPlayer != PC.ParentPlayer)
        {
            return;
        }

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
                if (ActiveItem != null)
                {
                    PlacingScript.UpdatePlacement(ActiveItem, ActiveItem.transform.localScale.x);
                    if (ItemSlots[CurrentSlot].ItemHeld)
                    {
                        //PlacingScript.SetMesh(ActiveItem.GetMesh());
                        //PlacingTransform.localScale = ActiveItem.transform.localScale;
                    }
                    else
                    {
                        int initialInt = CurrentSlot;
                        while (ItemSlots[CurrentSlot].ItemHeld == false)
                        {
                            CurrentSlot++;
                            if (CurrentSlot == MaxSlots)
                            {
                                CurrentSlot = 0;
                            }

                            if (initialInt == CurrentSlot)
                            {
                                break;
                            }
                        }
                        ActiveItem = ItemSlots[CurrentSlot].GetItemRef();
                        if (ItemSlots[CurrentSlot].ItemHeld)
                        {
                            //PlacingScript.SetMesh(ActiveItem.GetMesh());
                            //PlacingTransform.localScale = ActiveItem.transform.localScale;
                        }
                    }
                    if (!ItemSlots[CurrentSlot].ItemHeld)
                    {
                        PlacingItems = false;
                    }
                }
                else
                {
                    int initialInt = CurrentSlot;
                    while (ItemSlots[CurrentSlot].ItemHeld == false)
                    {
                        CurrentSlot++;
                        if (CurrentSlot == MaxSlots)
                        {
                            CurrentSlot = 0;
                        }

                        if (initialInt == CurrentSlot)
                        {
                            break;
                        }
                    }
                    ActiveItem = ItemSlots[CurrentSlot].GetItemRef();
                }
            }
            //if (Input.GetKeyDown(KeyCode.E))
            //{
            //    // Switch Item
            //    CurrentSlot++;
            //    if (CurrentSlot == MaxSlots)
            //    {
            //        CurrentSlot = 0;
            //    }
            //    if (ItemSlots[CurrentSlot].ItemHeld == false)
            //    {
            //        int initialInt = CurrentSlot;
            //        while (ItemSlots[CurrentSlot].ItemHeld == false)
            //        {
            //            CurrentSlot++;
            //            if (CurrentSlot == MaxSlots)
            //            {
            //                CurrentSlot = 0;
            //            }
            //            if (initialInt == CurrentSlot)
            //            {
            //                break;
            //            }
            //        }
            //    }
            //    if (MaxSlots > 0)
            //    {
            //        ActiveItem = ItemSlots[CurrentSlot].GetItemRef();
            //        PlacingScript.SetMesh(ActiveItem.GetMesh());
            //        PlacingTransform.localScale = ActiveItem.transform.localScale;
            //    }
            //}
            if (Input.GetKeyDown(KeyCode.Q))
            {
                // place item
                bool canPlace = PlacingScript.UpdatePlacement(ActiveItem, ActiveItem.transform.localScale.x);
                if (canPlace)
                {
                    PVL.PlayAbility();
                    PlacingItems = false;
                    PlacingScript.PlaceItem();
                    //ActiveItem.transform.parent = null;
                    //ActiveItem.transform.position = PlacingScript.ObjectPosition;
                    //ActiveItem.transform.up = PlacingScript.ObjectNormal;
                    //ActiveItem.transform.forward = PlacingScript.OffsetDirection;
                    //CmdSpawnItem(ActiveItem.GetITemID(), PlacingScript.ItemWorldPosition, PlacingScript.ItemWorldRotation);
                    PV.RPC("RPC_SpawnItem", RpcTarget.All, ActiveItem.GetITemID(), PlacingScript.ItemWorldPosition, PlacingScript.ItemWorldRotation);
                    ItemSlots[CurrentSlot].RemoveItem();
                }
            }
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

    [PunRPC]
    private void RPC_SpawnItem(string itemID, Vector3 Pos, Quaternion Rotation)
    {
        //GameObject temp = Instantiate(item);
        //temp.transform.position = Pos;
        //temp.transform.rotation = Rotation;

        //if (temp != null)
        //NetworkServer.Spawn(item);
        GameObject temp = (PhotonNetwork.Instantiate(itemID, Pos, Rotation, 0, null));

        temp.GetComponent<Item>().isPlacing = false;
        //IM.AddItemToList(.GetComponent<Item>().ItemType);
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
                tempRune.TurnOffRune();
            }
        }
    }
}
