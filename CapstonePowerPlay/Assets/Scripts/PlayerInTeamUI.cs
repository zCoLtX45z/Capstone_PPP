using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInTeamUI : MonoBehaviour {

    [SerializeField]
    private NetPlayer N_Player;

    [SerializeField]
    private Text[] PlayerNameList;
    private NetPlayer[] PlayerList = null;
    private List<NetPlayer> PlayersOnTeam = new List<NetPlayer>();

    [SerializeField]
    private int TeamNum = 0;
    [SerializeField]
    private int MaxPlayers = 4;

    [SerializeField]
    private GameObject IsFullImage;

    // Use this for initialization
    void Start () {
		if (MaxPlayers > 8)
        {
            Debug.LogWarning("Can't have more then 8 players on a team");
            MaxPlayers = 8;
        }
	}
	
	// Update is called once per frame
	void Update () {
        N_Player.SetPlayerList();
        PlayerList = N_Player.PlayerList;
        FindPlayersOnTeam();
        UpdateListOfPlayers();
    }

    private void FindPlayersOnTeam()
    {
        if (PlayerList != null)
        {
            foreach (NetPlayer p in PlayerList)
            {
                if (p.TeamNum == TeamNum && !PlayersOnTeam.Contains(p))
                {
                    if (PlayersOnTeam.Count < MaxPlayers)
                    {
                        PlayersOnTeam.Add(p);
                    }
                    else
                    {
                        p.TeamNum = 0;
                    }
                }
                if (p.TeamNum != TeamNum && PlayersOnTeam.Contains(p))
                {
                    PlayersOnTeam.Remove(p);
                }
            }
        }
    }

    private void UpdateListOfPlayers()
    {
        foreach(Text t in PlayerNameList)
        {
            t.text = "";
        }

        for(int i = 0; i < PlayersOnTeam.Count; i++)
        {
            PlayerNameList[i].text = PlayersOnTeam[i].name;
        }

        if (IsFullImage != null)
        {
            if (PlayersOnTeam.Count >= MaxPlayers)
            {
                IsFullImage.SetActive(true);
            }
            else
            {
                IsFullImage.SetActive(false);
            }
        }
    }
}
