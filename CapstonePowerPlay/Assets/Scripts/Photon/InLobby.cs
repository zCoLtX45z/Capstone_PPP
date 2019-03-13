using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class InLobby : MonoBehaviour {

    [SerializeField]
    private GameObject NextCanvas;
    private bool Search = false;

	// Update is called once per frame
	void Update () {
		if (Search && !NextCanvas.activeSelf)
        {
            if (PhotonNetwork.IsConnectedAndReady && PhotonNetwork.InLobby)
            {
                NextCanvas.SetActive(true);
                Search = false;
            }
        }
	}

    public void GetReady()
    {
        Search = true;
    }
}
