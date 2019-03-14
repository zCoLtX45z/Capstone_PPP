﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerRoomUI : MonoBehaviour {

    [SerializeField]
    private Text NameText;
    [SerializeField]
    private GameObject DisplayNameIF;

    private string DisplayName = "";

    private string PlayerName;
    public string PlayerIdentifier
    {
        get
        {
            return PlayerName;
        }
        set
        {
            PlayerName = value;
        }
    }

    public void ActivateIF()
    {
        DisplayNameIF.SetActive(true);
    }

    public void SetName(string Name)
    {
         NameText.text = Name;
    }

    public void SetName()
    {
        if (DisplayName == "")
            NameText.text = PlayerName;
        else
            NameText.text = DisplayName;
    }

    public void SetDisplayName(string name)
    {
        DisplayName = name;
        PhotonNetwork.LocalPlayer.CustomProperties["DisplayName"] = DisplayName;
    }

    public string GetDisplayName()
    {
        return DisplayName;
    }
}
