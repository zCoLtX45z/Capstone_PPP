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
    [SerializeField]
    private bool KeepScale = false;

    private Transform LocalPlayerObject;
    private bool Started = false;

    private float distance;
    private float scale;

    public void ForceStart()
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
            if (KeepScale)
            {
                distance = (LocalPlayerObject.position - transform.position).magnitude;
                scale = distance > OneTimeScaleDistance ? distance / OneTimeScaleDistance
                    : 1;
                transform.localScale = new Vector3(scale, scale, scale);
            }
            transform.LookAt(-LocalPlayerObject.position);
        }
    }

    public void SetLocalPlayer(Transform local)
    {
        LocalPlayerObject = local;
        Started = true;
    }
}
