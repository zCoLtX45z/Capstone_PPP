using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SetupLocalPlayer : NetworkBehaviour {

    [SyncVar(hook = "OnChangeName")]
    public string pname = "player";
    [SyncVar]
    public Color playerColor = Color.white;

    private void OnGUI()
    {
        //if (isLocalPlayer)
        //    pname = GUI.TextField(new Rect(25, Screen.height - 40, 100, 30), pname);
        //if(GUI.Button(new Rect(130,Screen.height - 40,80,30),"Change"))
        //{
        //    CmdChangeName(pname);
        //}
    }

    [Command]
    public void CmdChangeName(string newName)
    {
        RpcChangeName(newName);
    }
    [ClientRpc]
    public void RpcChangeName(string newName)
    {
        
        gameObject.name = pname;
        //this.GetComponentInChildren<TextMesh>().text = pname;
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

    private void Update()
    {
        //if (isLocalPlayer)
        //{
        //    this.GetComponentInChildren<TextMesh>().text = pname;
        //}

    }

    public override void OnStartLocalPlayer()
    {
      
        //CmdChangeName(pname);
        //base.OnStartLocalPlayer();
    }


    private void OnChangeName(string newName)
    {
        pname = newName;
        gameObject.name = pname;
        CmdChangeName(pname);
    }
}
