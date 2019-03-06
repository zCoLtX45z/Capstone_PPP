using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerNetwork : MonoBehaviour {

    public static PlayerNetwork Instance;
    public string PlayerName;
	// Use this for initialization
	void Awake () {
        Instance = this;
        PlayerName = "Player-" + PhotonNetwork.LocalPlayer.ActorNumber + "#" + Random.Range(1000, 99999);
    }
}
