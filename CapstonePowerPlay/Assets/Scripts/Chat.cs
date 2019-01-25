using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chat : MonoBehaviour {

    // Colors
    [SerializeField]
    private Color AllChatColor;
    [SerializeField]
    private Color TeamChatColor;
    [SerializeField]
    private Color ConsoleColor;

    // Chat Queues
    private Queue<ChatEntry> AllChatEntries;
    private Queue<ChatEntry> Team1ChatEntries;
    private Queue<ChatEntry> Team2ChatEntries;
    private Queue<ChatEntry> ConsoleEntries;

    // EntryTypes
    public enum EntryType
    {
        All = 0,
        Team1 = 1,
        Team2 = 2,
        Console = 3,
        NullType,
    }

    // Ui Elements
    [SerializeField]
    private GameObject AllChatInput;
    [SerializeField]
    private GameObject TeamChatInput;
    [SerializeField]
    private GameObject ConsoleInput;
    [SerializeField]
    private RawImage PlayerNameBackground;

    // Player Elements
    [SerializeField]
    private NetPlayer NP;

    // Other Player Chats
    private List<Chat> ChatList = new List<Chat>();

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CreateEntry(string name, string text, EntryType entryType)
    {
        if (entryType == EntryType.All)
        {

        }
        else if (entryType == EntryType.Team1)
        {

        }
        else if (entryType == EntryType.Team2)
        {

        }
        else if (entryType == EntryType.Console)
        {

        }
    }
}
