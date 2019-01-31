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
    private Queue<ChatEntry> EveryEntry = new Queue<ChatEntry>(); // Entry List 0 - Does not include console logs
    private Queue<ChatEntry> GlobalChatEntries = new Queue<ChatEntry>(); // Entry List 1
    private Queue<ChatEntry> TeamChatEntries = new Queue<ChatEntry>(); // Entry List 2
    private Queue<ChatEntry> ConsoleEntries = new Queue<ChatEntry>(); // Entry List 3

    // Ui Elements
    [SerializeField]
    private ChatEntry ChatEntryPrefab;
    [SerializeField]
    private GameObject AllChatInput;
    [SerializeField]
    private InputField AllChatInputText;
    [SerializeField]
    private Text AllChatName;
    [SerializeField]
    private GameObject TeamChatInput;
    [SerializeField]
    private InputField TeamChatInputText;
    [SerializeField]
    private Text TeamChatName;
    [SerializeField]
    private GameObject ConsoleInput;
    [SerializeField]
    private InputField ConsoleInputText;
    [SerializeField]
    private Text ConsoleChatName;
    [SerializeField]
    private RawImage PlayerNameBackground;
    [SerializeField]
    private RectTransform ScrollContentStartingPoint;
    [SerializeField]
    private List<Behaviour> UiObjectsToDisableAfterTime = new List<Behaviour>();
    [SerializeField]
    private List<Behaviour> UiObjectsToDisableOnExit = new List<Behaviour>();
    [SerializeField]
    private GameObject ChatParent;

    // Player Elements
    [SerializeField]
    private NetPlayer NP;

    // Other Player Chats
    private Chat[] ChatList;

    // Bool Checks
    private bool EnabledChat = false;

    // Variables
    [SerializeField]
    private float TimeToDiableElements = 10;
    private float DisableTimer = 0;
    private bool timerActive = false;
    [SerializeField]
    private float ContentSpacing = 3;
    [SerializeField]
    private int MaxEntries = 15;
    [SerializeField]
    private bool EraseConsoleEntries = false;
    private int EntryList = 0; // The entry that will be shown in the text box
    private int EntryCount = 0;

    //ConsoleVariables
    [SerializeField]
    private bool IgnoreLogs = true;
    [SerializeField]
    private bool IgnoreWarnings = false;
    [SerializeField]
    private bool IgnoreErrors = false;
    [SerializeField]
    private bool IgnoreCopies = true;

    void Awake()
    {
    }

    void Start()
    {
        AllChatName.text = gameObject.name;
        TeamChatName.text = gameObject.name;
        ConsoleChatName.text = gameObject.name;
    }
	// Update is called once per frame
	void Update () {
        if (isLocalPlayer)
        {
            if (EnabledChat)
            {
                UpdateDisplay();
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    ToggleChat();
                }
                if (Input.GetKeyDown(KeyCode.Tab) && !Input.GetKey(KeyCode.LeftShift))
                {
                    ToggleMessege();
                }
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    if (Input.GetKeyDown(KeyCode.Tab))
                    {
                        ToggleEntries();
                    }
                }
            }
            else if (timerActive)
            {
                DisableTimer += Time.deltaTime;
                if (DisableTimer > TimeToDiableElements)
                {
                    DisableTimer = 0;
                    DisableComponents();
                }
            }

            Application.logMessageReceived += LogCall;
        }

        // Limit the number of entries
        if (EveryEntry.Count > MaxEntries)
        {
            ChatEntry temp = EveryEntry.Dequeue();
            if (!GlobalChatEntries.Contains(temp) && !TeamChatEntries.Contains(temp) && !ConsoleEntries.Contains(temp))
            {
                Destroy(temp.gameObject);
            }
        }
        if (GlobalChatEntries.Count > MaxEntries)
        {
            ChatEntry temp = GlobalChatEntries.Dequeue();
            if (!EveryEntry.Contains(temp) && !TeamChatEntries.Contains(temp) && !ConsoleEntries.Contains(temp))
            {
                Destroy(temp.gameObject);
            }

        }
        if (TeamChatEntries.Count > MaxEntries)
        {
            ChatEntry temp = TeamChatEntries.Dequeue();
            if (!GlobalChatEntries.Contains(temp) && !EveryEntry.Contains(temp) && !ConsoleEntries.Contains(temp))
            {
                Destroy(temp.gameObject);
            }
        }
        if (EraseConsoleEntries)
        {
            if (ConsoleEntries.Count > MaxEntries)
            {
                ChatEntry temp = ConsoleEntries.Dequeue();
                if (!GlobalChatEntries.Contains(temp) && !TeamChatEntries.Contains(temp) && !EveryEntry.Contains(temp))
                {
                    Destroy(temp.gameObject);
                }
            }
        }
    }

    private void LogCall(string logString, string stackTrace, LogType type)
    {
        if (type == LogType.Error)
        {
            if(!IgnoreErrors)
                CreateConsoleEntry("Error", " -LogString- " + logString + " -StackTrace- ");
        }
        else if (type == LogType.Log)
        {
            if(!IgnoreLogs)
                CreateConsoleEntry("Log", " -LogString- " + logString + " -StackTrace- ");
        }
        else if (type == LogType.Warning)
        {
            if (!IgnoreWarnings)
                CreateConsoleEntry("Warning", " -LogString- " + logString + " -StackTrace- ");
        }
        else if (type == LogType.Assert)
        {
            CreateConsoleEntry("Assert", " -LogString- " + logString + " -StackTrace- ");
        }
        else if (type == LogType.Exception)
        {
            CreateConsoleEntry("Exception", " -LogString- " + logString + " -StackTrace- ");
        }
    }
    public bool GetEnabled()
    {
        return EnabledChat;
    }

    public void CreateGlobalEntry(string text)
    {
        CmdCreateEntry(gameObject.name, text, "All", this.gameObject);
    }

    public void CreateTeamEntry(string text)
    {
        CmdCreateEntry(gameObject.name, text, "Team" + NP.GetTeamNum(), this.gameObject);
    }

    public void CreateConsoleEntry(string text)
    {
        CreateConsoleEntry(gameObject.name, text);
    }

    public void EnterEntry(ChatEntry Entry)
    {
        ChatEntry temp = Instantiate(Entry, ScrollContentStartingPoint);
        //Debug.Log("Enter Entry");
        if (temp.EntryType == "All")
        {
            //Debug.Log("Enter Entry ALL");
            if (!GlobalChatEntries.Contains(temp))
            {
                //Debug.Log("Enter Entry ALL in");
                EveryEntry.Enqueue(temp);
                GlobalChatEntries.Enqueue(temp);
                RefreshUi();
            }
            else
            {
                Destroy(temp);
            }
        }
        else if (temp.EntryType == "Team1")
        {
            //Debug.Log("Enter Entry Team1");
            if (NP.GetTeamNum() == 1)
            {
                if (!TeamChatEntries.Contains(temp))
                {
                    //Debug.Log("Enter Entry Team1 in");
                    EveryEntry.Enqueue(temp);
                    TeamChatEntries.Enqueue(temp);
                    RefreshUi();
                }
                else
                {
                    Destroy(temp);
                }
            }
        }
        else if (temp.EntryType == "Team2")
        {
            //Debug.Log("Enter Entry Team2");
            if (NP.GetTeamNum() == 2)
            {
                if (!TeamChatEntries.Contains(temp))
                {
                    //Debug.Log("Enter Entry Team2 in");
                    EveryEntry.Enqueue(temp);
                    TeamChatEntries.Enqueue(temp);
                    RefreshUi();
                }
                else
                {
                    Destroy(temp);
                }
            }
        }
        else
        {
            Destroy(temp);
        }


        UpdateDisplay();
    }
    public void BroadcastEntry(ChatEntry Entry, Chat WhoBroadcasted)
    {
        //Debug.Log("Broadcast Entry");
        ChatList = FindObjectsOfType<Chat>();
        foreach (Chat c in ChatList)
        {
            if (c != WhoBroadcasted)
                c.EnterEntry(Entry);
        }
    }

    public void RefreshUi()
    {
        if (NP.LocalPlayer == NP)
        {
            EnableUI();
            EnableComponents();
            DisableUI();
            UpdateDisplay();
        }
    }

    public void ReEnterChat(string text)
    {
        if (text == "")
        {
            //ToggleMessege();
        }
        else
        {
            ToggleChat();
        }
    }

    public void UpdateDisplay()
    {
        int count = 0;
        if (EntryList == 0)
        {
            count = EveryEntry.Count;
            foreach (ChatEntry c in EveryEntry)
            {
                count--;
                RectTransform rt = c.GetComponent<RectTransform>();
                if (rt.transform.parent != ScrollContentStartingPoint)
                {
                    rt.parent = ScrollContentStartingPoint;
                }
                rt.localPosition = new Vector3(rt.localPosition.x, -count * (ContentSpacing), 0);
                c.gameObject.SetActive(true);
            }
        }
        else if (EntryList == 1)
        {
            count = GlobalChatEntries.Count;
            foreach (ChatEntry c in GlobalChatEntries)
            {
                count--;
                RectTransform rt = c.GetComponent<RectTransform>();
                if (rt.transform.parent != ScrollContentStartingPoint)
                {
                    rt.parent = ScrollContentStartingPoint;
                }
                rt.localPosition = new Vector3(rt.localPosition.x, -count * (ContentSpacing), 0);
                c.gameObject.SetActive(true);
            }
        }
        else if (EntryList == 2)
        {
            count = TeamChatEntries.Count;
            foreach (ChatEntry c in TeamChatEntries)
            {
                count--;
                RectTransform rt = c.GetComponent<RectTransform>();
                if (rt.transform.parent != ScrollContentStartingPoint)
                {
                    rt.parent = ScrollContentStartingPoint;
                }
                rt.localPosition = new Vector3(rt.localPosition.x, -count * (ContentSpacing), 0);
                c.gameObject.SetActive(true);
            }
        }
        else if (EntryList == 3)
        {
            count = ConsoleEntries.Count;
            foreach (ChatEntry c in ConsoleEntries)
            {
                count--;
                RectTransform rt = c.GetComponent<RectTransform>();
                if (rt.transform.parent != ScrollContentStartingPoint)
                {
                    rt.parent = ScrollContentStartingPoint;
                }
                rt.localPosition = new Vector3(rt.localPosition.x, -count * (ContentSpacing + 30 * 2), 0);
                c.gameObject.SetActive(true);
            }
        }
    }

    public void CreateConsoleEntry(string name, string text)
    {
        if (IgnoreCopies)
        {
            bool CompleteCommand = true;
            foreach (ChatEntry c in ConsoleEntries)
            {
                if (c.EntryText == name + ": " + text)
                {
                    CompleteCommand = false;
                    break;
                }
            }
            if (CompleteCommand)
            {
                ChatEntry temp = Instantiate(ChatEntryPrefab, ScrollContentStartingPoint);
                EntryCount++;
                temp.CreateMessege(name + ": " + text, ConsoleColor, "Console", EntryCount, gameObject.name);
                ConsoleEntries.Enqueue(temp);
                if (EntryList != 3)
                {
                    temp.gameObject.SetActive(false);
                }
            }
        }
    }
    [Command]
    public void CmdCreateEntry(string name, string text, string entryType, GameObject WhoEntered)
    {
        if (text != "")
        {
            if (entryType != "Console")
            {
                RpcCreateEntry(name, text, entryType, WhoEntered);
            }
        }
    }

    [ClientRpc]
    public void RpcCreateEntry(string name, string text, string entryType, GameObject WhoEntered)
    {
        ChatEntry temp = Instantiate(ChatEntryPrefab, ScrollContentStartingPoint);
        if (entryType == "All")
        {
            EntryCount++;
            temp.CreateMessege(name + ": " + text, AllChatColor, entryType, EntryCount, gameObject.name);
            GlobalChatEntries.Enqueue(temp);
            EveryEntry.Enqueue(temp);
            if (EntryList != 0)
            {
                temp.gameObject.SetActive(false);
            }
            BroadcastEntry(temp, WhoEntered.GetComponent<Chat>());
        }
        else if (entryType == "Team1")
        {
            if (NP.GetTeamNum() == 1)
            {
                EntryCount++;
                temp.CreateMessege(name + ": " + text, TeamChatColor, entryType, EntryCount, gameObject.name);
                TeamChatEntries.Enqueue(temp);
                EveryEntry.Enqueue(temp);
                if (EntryList != 1)
                {
                    temp.gameObject.SetActive(false);
                }
                BroadcastEntry(temp, WhoEntered.GetComponent<Chat>());
            }
        }
        else if (entryType == "Team2")
        {
            if (NP.GetTeamNum() == 2)
            {
                EntryCount++;
                temp.CreateMessege(name + ": " + text, TeamChatColor, entryType, EntryCount, gameObject.name);
                TeamChatEntries.Enqueue(temp);
                EveryEntry.Enqueue(temp);
                if (EntryList != 2)
                {
                    temp.gameObject.SetActive(false);
                }
                BroadcastEntry(temp, WhoEntered.GetComponent<Chat>());
            }
        }
        else if (entryType == "Console")
        {
            EntryCount++;
            temp.CreateMessege(name + ": " + text, ConsoleColor, entryType, EntryCount, gameObject.name);
            ConsoleEntries.Enqueue(temp);
            if (EntryList != 3)
            {
                temp.gameObject.SetActive(false);
            }
 
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
            UpdateDisplay();
            TeamChatInputText.ActivateInputField();
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
        ChatParent.SetActive(true);
        foreach (Behaviour b in UiObjectsToDisableAfterTime)
        {
            b.enabled = true;
        }
    }

    private void DisableComponents()
    {
        timerActive = false;
        ChatParent.SetActive(false);
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

    public void ToggleEntries()
    {
        EntryList++;
        if (EntryList == 4)
        {
            EntryList = 0;
        }

        if (EntryList == 0)
        {
            foreach (ChatEntry CE in ConsoleEntries)
            {
                CE.gameObject.SetActive(false);
            }

            foreach (ChatEntry CE in EveryEntry)
            {
                CE.gameObject.SetActive(true);
            }
        }

        if (EntryList == 1)
        {
            foreach (ChatEntry CE in EveryEntry)
            {
                CE.gameObject.SetActive(false);
            }

            foreach (ChatEntry CE in GlobalChatEntries)
            {
                CE.gameObject.SetActive(true);
            }
        }

        if (EntryList == 2)
        {
            foreach (ChatEntry CE in GlobalChatEntries)
            {
                CE.gameObject.SetActive(false);
            }

            foreach (ChatEntry CE in TeamChatEntries)
            {
                CE.gameObject.SetActive(true);
            }
        }

        if (EntryList == 3)
        {
            foreach (ChatEntry CE in TeamChatEntries)
            {
                CE.gameObject.SetActive(false);
            }

            foreach (ChatEntry CE in ConsoleEntries)
            {
                CE.gameObject.SetActive(true);
            }
        }
        UpdateDisplay();
    }

    // Toggle type of messege you are writing
    private void ToggleMessege()
    {
        if (AllChatInput.activeSelf)
        {
            AllChatInput.SetActive(false);
            TeamChatInput.SetActive(true);
            TeamChatInputText.ActivateInputField();
        }
        else if (TeamChatInput.activeSelf)
        {
            ConsoleInput.SetActive(true);
            TeamChatInput.SetActive(false);
            ConsoleInputText.ActivateInputField();
        }
        else if (ConsoleInput.activeSelf)
        {
            ConsoleInput.SetActive(false);
            AllChatInput.SetActive(true);
            AllChatInputText.ActivateInputField();
        }
    }
}
