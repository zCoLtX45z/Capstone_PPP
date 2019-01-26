using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Chat : NetworkBehaviour
{ 
    // Colors
    [SerializeField]
    private Color AllChatColor;
    [SerializeField]
    private Color TeamChatColor;
    [SerializeField]
    private Color ConsoleColor;

    // Chat Queues
    private Queue<ChatEntry> EveryEntryEntries;
    private Queue<ChatEntry> GlobalChatEntries;
    private Queue<ChatEntry> TeamChatEntries;
    private Queue<ChatEntry> ConsoleEntries;

    // Ui Elements
    [SerializeField]
    private ChatEntry ChatEntryPrefab;
    [SerializeField]
    private GameObject AllChatInput;
    [SerializeField]
    private GameObject TeamChatInput;
    [SerializeField]
    private GameObject ConsoleInput;
    [SerializeField]
    private RawImage PlayerNameBackground;
    [SerializeField]
    private RectTransform ScrollContentStartingPoint;
    [SerializeField]
    private List<Behaviour> UiObjectsToDisableAfterTime = new List<Behaviour>();
    [SerializeField]
    private List<Behaviour> UiObjectsToDisableOnExit = new List<Behaviour>();

    // Player Elements
    [SerializeField]
    private NetPlayer NP;

    // Other Player Chats
    private List<Chat> ChatList = new List<Chat>();

    // Bool Checks
    private bool EnabledChat = false;

    // Variables
    [SerializeField]
    private float TimeToDiableElements = 10;
    private float DisableTimer = 0;
    private bool timerActive = false;
    [SerializeField]
    private float ContentSpacinng = 3;
    [SerializeField]
    private int MaxEntries = 15;
    [SerializeField]
    private bool EraseConsoleEntries = false;
    

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (EnabledChat)
        {
        }
        else if (timerActive)
        {
            DisableTimer += Time.deltaTime;
            if (DisableTimer > TimeToDiableElements)
            {
                DisableComponents();
            }
        }
        if (EveryEntryEntries.Count > MaxEntries)
        {
            EveryEntryEntries.Dequeue();
        }
        if (GlobalChatEntries.Count > MaxEntries)
        {
            GlobalChatEntries.Dequeue();
        }
        if (TeamChatEntries.Count > MaxEntries)
        {
            GlobalChatEntries.Dequeue();
        }
        if (EraseConsoleEntries)
        {
            if (ConsoleEntries.Count > MaxEntries)
            {
                ConsoleEntries.Dequeue();
            }
        }
    }

    public void CreateGlobalEntry(string text)
    {
        CmdCreateEntry(gameObject.name, text, "All");
    }

    public void CreateTeamEntry(string text)
    {
        CmdCreateEntry(gameObject.name, text, "Team" + NP.GetTeamNum());
    }

    public void CreateConsoleEntry(string text)
    {
        CmdCreateEntry(gameObject.name, text, "Console");
    }

    [Command]
    public void CmdCreateEntry(string name, string text, string entryType)
    {
        RpcCreateEntry(name, text, entryType);
    }

    [ClientRpc]
    public void RpcCreateEntry(string name, string text, string entryType)
    {
        ChatEntry temp = Instantiate(ChatEntryPrefab, ScrollContentStartingPoint);
        if (entryType == "All")
        {
            temp.CreateMessege(name + ": " + text, AllChatColor);
            GlobalChatEntries.Enqueue(temp);
            EveryEntryEntries.Enqueue(temp);
        }
        else if (entryType == "Team1")
        {
            if (NP.GetTeamNum() == 1)
            {
                temp.CreateMessege(name + ": " + text, TeamChatColor);
                TeamChatEntries.Enqueue(temp);
                EveryEntryEntries.Enqueue(temp);
            }
        }
        else if (entryType == "Team2")
        {
            if (NP.GetTeamNum() == 2)
            {
                temp.CreateMessege(name + ": " + text, TeamChatColor);
                TeamChatEntries.Enqueue(temp);
                EveryEntryEntries.Enqueue(temp);
            }
        }
        else if (entryType == "Console")
        {
            temp.CreateMessege(name + ": " + text, ConsoleColor);
            ConsoleEntries.Enqueue(temp);
            EveryEntryEntries.Enqueue(temp);
        }
    }

    public void EnterChat(bool b = true)
    {
        if (EnabledChat == false && b == true)
        {
            EnableComponents();
        }
        else if (EnabledChat == true && b == false)
        {
            DisableComponents();
        }
        EnabledChat = b;
    }

    public void ToggleChat()
    {
        if (EnabledChat == false)
        {
            EnableUI();
            EnableComponents();
        }
        else
        {
            DisableUI();
        }
        EnabledChat = !EnabledChat;
    }

    private void EnableComponents()
    {
        DisableTimer = 0;
        timerActive = true;
        PlayerNameBackground.gameObject.SetActive(true);
        foreach (Behaviour b in UiObjectsToDisableAfterTime)
        {
            b.enabled = true;
        }
    }

    private void DisableComponents()
    {
        timerActive = false;
        foreach (Behaviour b in UiObjectsToDisableAfterTime)
        {
            b.enabled = false;
        }
    }

    private void EnableUI()
    {
        foreach (Behaviour b in UiObjectsToDisableOnExit)
        {
            b.enabled = true;
        }
        TeamChatInput.SetActive(true);
    }

    private void DisableUI()
    {
        foreach (Behaviour b in UiObjectsToDisableOnExit)
        {
            b.enabled = false;
        }
        AllChatInput.SetActive(false);
        TeamChatInput.SetActive(false);
        ConsoleInput.SetActive(false);
    }
}
