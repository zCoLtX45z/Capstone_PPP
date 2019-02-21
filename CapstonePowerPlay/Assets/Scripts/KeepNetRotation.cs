using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class KeepNetRotation : NetworkBehaviour {

    [SerializeField]
    private Animator AN;
    [SyncVar(hook = "UpdateRotation")]
    private bool IsRotating = false;

    private NetPlayer[] NetPlayerList;
    private hoverBoardScript[] HoverboardList;

    //new spawning mechanics
    [SerializeField]
    private Transform FinalSpot;

    private void Start()
    {
        FinalSpot = GameObject.FindObjectOfType<netSpawner>().transform;
        
        AN.enabled = false;
    }

    // Update is called once per frame
    void Update ()
    {
        Debug.Log(FinalSpot.transform.position.y);
        if(this.gameObject.transform.position.y < FinalSpot.transform.position.y)
        {
            Debug.Log("moving on up");
            transform.position += Vector3.up * 1 * Time.deltaTime;
        }

        if (!IsRotating)
        {
            NetPlayerList = FindObjectsOfType<NetPlayer>();
            HoverboardList = FindObjectsOfType<hoverBoardScript>();
            if (NetPlayerList.Length > 0)
            {
                if (HoverboardList.Length > 0)
                {
                    if (NetPlayerList.Length == HoverboardList.Length)
                    {
                        if (isServer)
                            CmdUpdateRotation(true);
                    }
                }
            }
        }
        else
        {
            if (!AN.GetBool("Rotate"))
            {
                AN.SetBool("Rotate", IsRotating);
            }
        }
	}

    [Command]
    private void CmdUpdateRotation(bool rotating)
    {
        IsRotating = rotating;
    }

    private void UpdateRotation(bool rotating)
    {
        IsRotating = rotating;
    }
}
