using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {

    [SerializeField]
    private int MaxWallItemCount = 1;
    [SerializeField]
    private int MaxSpeedStripItemCount = 1;

    [SerializeField]
    private float MaxSpeedStripTimeOut;
    private float SpeedStripTimeOut;
    [SerializeField]
    private float MaxWallTimeOut;
    private float WallTimeOut;

    // item type int = 1
    private Queue<GameObject> SpeedStripItemQueue = new Queue<GameObject>();
    // item type int = 2
    private Queue<GameObject> WallItemQueue = new Queue<GameObject>();


    // item type int = 1
    private Queue<GameObject> SpeedStripItemDestroyQueue = new Queue<GameObject>();
    // item type int = 2
    private Queue<GameObject> WallItemDestroyQueue = new Queue<GameObject>();

    [SerializeField]
    private PhotonView PV;

    // Update is called once per frame
    void Update () {
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("count of dSpeed: " + SpeedStripItemDestroyQueue.Count);
            Debug.Log("count of dWall: " + WallItemDestroyQueue.Count);
            if (SpeedStripItemDestroyQueue.Count != 0)
            {
                if (SpeedStripTimeOut < MaxSpeedStripTimeOut)
                {
                    SpeedStripTimeOut += Time.deltaTime;
                }
                else
                {
                    SpeedStripTimeOut = 0;
                    GameObject TempPV = SpeedStripItemDestroyQueue.Dequeue();
                    //PhotonView.Destroy(TempPV.gameObject);
                    Debug.Log("TempPV = " + TempPV);
                    PV.RPC("RPC_Destroy", RpcTarget.All, TempPV.GetComponent<PhotonView>().ViewID);
                    if (TempPV != null)
                    {
                        SpeedStripItemDestroyQueue.Enqueue(TempPV);
                    }
                }
            }

            if (WallItemDestroyQueue.Count != 0)
            {
                if (WallTimeOut < MaxWallTimeOut)
                {
                    WallTimeOut += Time.deltaTime;
                }
                else
                {
                    WallTimeOut = 0;
                    GameObject TempPV = WallItemDestroyQueue.Dequeue();
                    //PhotonView.Destroy(TempPV.gameObject);
                    Debug.Log("TempPV = " + TempPV);
                    PV.RPC("RPC_Destroy", RpcTarget.All, TempPV.GetComponent<PhotonView>().ViewID);
                    if (TempPV != null)
                    {
                        WallItemDestroyQueue.Enqueue(TempPV);
                    }
                }
            }
        }
    }

    public void AddItemToList(int item)
    {
        PV.RPC("RPC_AddItemToList", RpcTarget.MasterClient, item);
    }

    [PunRPC]
    private void RPC_AddItemToList(int ItemID)
    {
        Debug.Log("RPC_AddItem");
        // find items 
        Item[] TempArray = FindObjectsOfType<Item>();
        List<Item> DesiredItem = new List<Item>();

        foreach (Item item in TempArray)
        {
            if(item.ItemType == ItemID)
            {
                DesiredItem.Add(item);
            }
        }

        // filter items we want
        foreach (Item item in DesiredItem)
        {
            // speed
            if(item.ItemType == 1)
            {
                if(!SpeedStripItemQueue.Contains(item.gameObject))
                {
                    SpeedStripItemQueue.Enqueue(item.gameObject);
                }
            }
            //wall
            else if (item.ItemType == 2)
            {
                if (!WallItemQueue.Contains(item.gameObject))
                {
                    WallItemQueue.Enqueue(item.gameObject);
                }
            }
        }

        if (ItemID == 1)
        {
            if (SpeedStripItemQueue.Count > MaxSpeedStripItemCount)
            {
                Debug.Log("Destroy speed queue added");
                SpeedStripItemDestroyQueue.Enqueue(SpeedStripItemQueue.Dequeue());
            }
        }
        else if (ItemID == 2)
        {
            if (WallItemQueue.Count > MaxWallItemCount)
            {
                Debug.Log("Destroy wall queue added");
                WallItemDestroyQueue.Enqueue(WallItemQueue.Dequeue());
            }
        }
    }


    [PunRPC]
    private void RPC_Destroy(int ItemID)
    {
        Debug.Log("Destroyed Entered");
        Destroy(PhotonView.Find(ItemID).gameObject);
    }
      
}
