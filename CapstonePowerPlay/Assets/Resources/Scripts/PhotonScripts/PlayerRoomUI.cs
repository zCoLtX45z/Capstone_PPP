using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerRoomUI : MonoBehaviour {

    [SerializeField]
    private Text NameText;
    [SerializeField]
    private InputField DisplayNameIF;

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

    private void Update()
    {
        if (DisplayNameIF.isFocused)
        {
            DisplayNameIF.image.color = Color.black;
        }
        else
        {
            DisplayNameIF.image.color = Color.clear;
        }
    }

    public void ActivateIF()
    {
        DisplayNameIF.gameObject.SetActive(true);
    }

    public void SetName(string Name)
    {
         NameText.text = Name;
    }

    public void MakeIFBlank()
    {
        DisplayNameIF.text = "";
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
        DisplayNameIF.image.color = Color.clear;
        DisplayName = name;
        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        hash = PhotonNetwork.LocalPlayer.CustomProperties;
        hash.Remove("DisplayName");
        hash.Add("DisplayName", DisplayName);
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        PhotonRoom temp = FindObjectOfType<PhotonRoom>();
        temp.SetUpdateDisplayName();
    }

    public string GetDisplayName()
    {
        return DisplayName;
    }

    public void MakeNameBlank()
    {
        DisplayNameIF.image.color = Color.black;
    }
}
