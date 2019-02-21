using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;



public class DollyManager : MonoBehaviour {

    [SerializeField]
    private Transform[] c_DollyCarts;
    /*
     * 0 = Null to start
     * 1 = Start To menu
     * 2 = Menu to lobby list 
     * 3 = lobby list To lobby
     */

    [SerializeField]
    Cinemachine.CinemachineVirtualCamera[] c_virtualCameras;
    /*
     * 0 = Null to start
     * 1 = Start To menu
     * 2 = Menu to lobby list 
     * 3 = lobby list To lobby
     */

    [SerializeField]
    private int lowPriority;

    [SerializeField]
    private int highPriority;

    private void Start()
    {
        for (int i = 0; i < c_virtualCameras.Length; i++)
        {
            c_virtualCameras[i].Priority = lowPriority;
        }
        c_virtualCameras[0].Priority = highPriority;
    }

    public void Null_And_Start(bool forwards)
    {
        if(forwards)
        {
            ForwardMovement(0);
        }
        else
        {
            BackwardMovement(0);
        }
    }

    public void Start_And_Menu(bool forwards)
    {
        if (forwards)
        {
            ForwardMovement(1);
        }
        else
        {
            BackwardMovement(1);
        }
    }

    public void Menu_And_Play(bool forwards)
    {
        

        if (forwards)
        {
            ForwardMovement(2);
        }
        else
        {
            BackwardMovement(2);
        }
    }

    public void Play_And_Menu(bool forwards)
    {
        if (forwards)
        {
            ForwardMovement(3);
        }
        else
        {
            BackwardMovement(3);
        }
    }


    private void ForwardMovement(int trackInteger)
    {
        for (int i = 0; i < c_DollyCarts.Length; i++)
        {
            c_virtualCameras[i].Priority = lowPriority;
        }

        DollyModifier dollyMod = c_DollyCarts[trackInteger].GetComponent<DollyModifier>();

        dollyMod.reachedSpeedMax = false;
        dollyMod.reachedSpeedMaxRev = false;
        c_virtualCameras[trackInteger].Priority = highPriority;
        dollyMod.reverse = false;
        dollyMod.allowMovement = true;
        c_DollyCarts[trackInteger].GetComponent<Cinemachine.CinemachineDollyCart>().m_Position = 0;
    }

    private void BackwardMovement(int trackInteger)
    {
        for (int i = 0; i < c_DollyCarts.Length; i++)
        {
            c_virtualCameras[i].Priority = lowPriority;
        }

        DollyModifier dollyMod = c_DollyCarts[trackInteger].GetComponent<DollyModifier>();

        dollyMod.reachedSpeedMax = false;
        dollyMod.reachedSpeedMaxRev = false;
        c_virtualCameras[trackInteger].Priority = highPriority;
        dollyMod.reverse = true;
        dollyMod.allowMovement = true;
        //c_DollyCarts[trackInteger].GetComponent<Cinemachine.CinemachineDollyCart>().m_Position = c_DollyCarts[0].GetComponent<Cinemachine.CinemachineDollyCart>().m_Path.PathLength;
    }


}
