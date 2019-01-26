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

    public void CreateMessege(string text, Color TextColor)
    {
        MessegeText.text = text;
        MessegeText.color = TextColor;
        TextHeight = MessegeRectTransfrom.rect.height;
        TimeOfEntry = Time.time;
    }
}
