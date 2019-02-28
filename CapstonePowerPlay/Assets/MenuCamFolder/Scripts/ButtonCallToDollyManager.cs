using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCallToDollyManager : MonoBehaviour {

   public enum ButtonEfect
   {
        Start, Play, Setting
   }

    public ButtonEfect bFect;

    [SerializeField]
    private DollyManager dManager;

    private void Start()
    {
        dManager = FindObjectOfType<DollyManager>();
    }

    public void CallEffect()
    {
        if(bFect == ButtonEfect.Start)
        {
            StartToMenuButton();
        }
        else if (bFect == ButtonEfect.Play)
        {
            PlayToLobbyButton();
        }
        else if (bFect == ButtonEfect.Setting)
        {
            LobbyButton();
        }
    }


    public void StartToMenuButton()
    {
        dManager.Start_And_Menu(true);
    }

    public void PlayToLobbyButton()
    {
        dManager.Menu_And_LobbyList(true);
    }

    public void LobbyButton()
    {
        dManager.Menu_And_Setting(true);
    }
}
