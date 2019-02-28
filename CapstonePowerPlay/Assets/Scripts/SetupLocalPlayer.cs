using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class SetupLocalPlayer : MonoBehaviour {

   // [SyncVar(hook = "OnChangeName")]
   public string pname = "player";
    //[SyncVar]
    public Color playerColor = Color.white;

    private PhotonView PV;



    private void OnGUI()
    {
        //if (isLocalPlayer)
        //    pname = GUI.TextField(new Rect(25, Screen.height - 40, 100, 30), pname);
        //if(GUI.Button(new Rect(130,Screen.height - 40,80,30),"Change"))
        //{
        //    CmdChangeName(pname);
        //}
    }

    [PunRPC]
    public void RPC_ChangeName(string newName)
    {
        ChangeName(newName);
    }
    
    public void ChangeName(string newName)
    {
        
        gameObject.name = pname;
        //this.GetComponentInChildren<TextMesh>().text = pname;
    }
    private void Start()
    {
        PV = GetComponent<PhotonView>();
        if (PV.IsMine/*isLocalPlayer*/)
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

    //public override void OnStartLocalPlayer()
    //{
      
    //    //CmdChangeName(pname);
    //    //base.OnStartLocalPlayer();
    //}


    private void OnChangeName(string newName)
    {
        pname = newName;
        gameObject.name = pname;
        //CmdChangeName(pname);
        PV.RPC("RPC_ChangeName", RpcTarget.All, pname);
    }
}
