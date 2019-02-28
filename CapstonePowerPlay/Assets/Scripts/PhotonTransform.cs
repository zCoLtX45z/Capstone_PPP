using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonTransform : MonoBehaviourPun, IPunObservable {

    [SerializeField]
    private float SmoothingPositionPercent = 0.1f;
    [SerializeField]
    private float SmoothingRotationPercent = 0.9f;
    [SerializeField]
    private int PhotonSendRate = 60;
    [SerializeField]
    private int PhotonSerializeRate = 30;
    [SerializeField]
    private Transform PT;
    [SerializeField]
    private PhotonView PV;
    private Vector3 TargetPosition = Vector3.zero;
    private Quaternion TargetRotation = Quaternion.identity;

    void Awake()
    {
        PhotonNetwork.SendRate = PhotonSendRate;
        PhotonNetwork.SerializationRate = PhotonSerializeRate;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(PT.position);
            stream.SendNext(PT.rotation);
        }
        else
        {
            TargetPosition = (Vector3)stream.ReceiveNext();
            TargetRotation = (Quaternion)stream.ReceiveNext();
        }
    }
        // Use this for initialization
    void Start () {
        TargetPosition = PT.position;
        TargetRotation = PT.rotation;
    }
	
	// Update is called once per frame
	void Update () {
		
        if(PV.IsMine)
        {
            // Local Player
        }
        else
        {
            // Smooth Player
            SmoothMovement();
        }
	}

    private void SmoothMovement()
    {
        PT.position = Vector3.Lerp(PT.position, TargetPosition, SmoothingPositionPercent);
        PT.rotation = Quaternion.Lerp(PT.rotation, TargetRotation, SmoothingPositionPercent);
    }
}
