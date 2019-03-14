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

    public void ActivateIF()
    {
        DisplayNameIF.gameObject.SetActive(true);
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
        PhotonRoom temp = FindObjectOfType<PhotonRoom>();
        temp.SetUpdateDisplayName();
    }

    public string GetDisplayName()
    {
        return DisplayName;
    }

    public void MakeNameBlank()
    {
        NameText.text = "";
    }
}
