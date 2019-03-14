using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerRoomUI : MonoBehaviourPun
{

    [SerializeField]
    private Text NameText;
    [SerializeField]
    private InputField DisplayNameIF;

    private string DisplayName = "";
    private string DisplayNameInitial = "";
    bool SetDisplayNameBool = false;

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

    private void Start()
    {
        DisplayName = (string)PhotonNetwork.LocalPlayer.CustomProperties["DisplayName"];
        DisplayNameInitial = DisplayName;
    }

    private void Update()
    {
        if (DisplayNameIF.isFocused)
        {
            DisplayNameIF.image.color = Color.black;
        }
        else
        {
            //if (SetDisplayNameBool && DisplayNameInitial != DisplayName)
            //{
            //    SetDisplayNameBool = false;
            //    Debug.Log("Set Name " + DisplayName);
            //    ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
            //    hash = PhotonNetwork.LocalPlayer.CustomProperties;
            //    hash.Remove("DisplayName");
            //    hash.Add("DisplayName", DisplayName);
            //    PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
            //}
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
        DisplayName = name;
        Debug.Log("Set DisplayName " + DisplayName);
        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        hash = PhotonNetwork.LocalPlayer.CustomProperties;
        hash.Remove("DisplayName");
        hash.Add("DisplayName", DisplayName);
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        Debug.Log("Set DisplayName (Hash)" + (string)PhotonNetwork.LocalPlayer.CustomProperties["DisplayName"]);
        //SetDisplayNameBool = true;
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
