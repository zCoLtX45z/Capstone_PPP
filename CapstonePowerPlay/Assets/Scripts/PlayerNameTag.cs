using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerNameTag : MonoBehaviour {

    [SerializeField]
    private float OneTimeScaleDistance = 5;
    [SerializeField]
    private TextMesh TM;
    [SerializeField]
    private PhotonView PV;

    private Transform LocalPlayerObject;
    private bool Started = false;

    private float distance;
    private float scale;

    private void ForceStart()
    {
        if (PV.IsMine)
        {
            PlayerNameTag[] PlayerList = FindObjectsOfType<PlayerNameTag>();
            foreach(PlayerNameTag PNT in PlayerList)
            {
                PNT.SetLocalPlayer(gameObject.transform);
            }
            Started = true;
        }
    }
    private void Update()
    {
        if (Started)
        {
            distance = (LocalPlayerObject.position - transform.position).magnitude;
            scale = OneTimeScaleDistance / distance;
            transform.localScale = new Vector3(scale, scale, scale);
            transform.LookAt(LocalPlayerObject);
        }
    }

    public void SetLocalPlayer(Transform local)
    {
        LocalPlayerObject = local;
        Started = true;
    }
}
