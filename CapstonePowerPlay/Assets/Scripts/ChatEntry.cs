using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatEntry : MonoBehaviour {

    [SerializeField]
    private Text MessegeText;
    [SerializeField]
    private LayoutElement Layout;
    [SerializeField]
    private RectTransform MessegeRectTransfrom;
    [SerializeField]
    private ContentSizeFitter CSF;
    [HideInInspector]
    public float TextHeight;
    [HideInInspector]
    public float TimeOfEntry;
    [HideInInspector]
    public string EntryType;
    [HideInInspector]
    public string EntryText;
    [HideInInspector]
    public int EntryNumber;
    [HideInInspector]
    public GameObject EntryPerson;


    public void CreateMessege(string text, Color TextColor, string type, int EntryNum, GameObject WhoEntered)
    {
        EntryText = text;
        MessegeText.text = text;
        MessegeText.color = TextColor;
        TextHeight = MessegeRectTransfrom.rect.height;
        TimeOfEntry = Time.time;
        EntryType = type;
        EntryNumber = EntryNum;
        EntryPerson = WhoEntered;
    }
}
