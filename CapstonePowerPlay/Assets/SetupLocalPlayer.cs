using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetupLocalPlayer : NetworkBehaviour {

    [SyncVar]
    public string pname = "player";
    [SyncVar]
    public Color playerColor = Color.white;

    private void OnGUI()
    {
        if (isLocalPlayer)
            pname = GUI.TextField(new Rect(25, Screen.height - 40, 100, 30), pname);
        if(GUI.Button(new Rect(130,Screen.height - 40,80,30),"Change"))
        {
            CmdChangeName(pname);
        }
    }

    [Command]
    public void CmdChangeName(string newName)
    {
        pname = newName;
        this.GetComponentInChildren<TextMesh>().text = pname;
    }
    private void Start()
    {
        if(isLocalPlayer)
        {
            Renderer[] rends = GetComponentsInChildren<Renderer>();
            foreach (Renderer r in rends)
                r.material.color = playerColor;
        }
        
    }
}
