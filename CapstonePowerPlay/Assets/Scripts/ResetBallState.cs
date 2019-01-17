using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ResetBallState : NetworkBehaviour {

    [SerializeField]
    private Ball ball;

    public BallHandling PlayerHolding = null;
	
	// Update is called once per frame
	void Update () {
        if (PlayerHolding != null)
        {
            if (PlayerHolding.ball == null)
            {
                PlayerHolding.ball = ball;
            }
        }
	}

    [Command]
    public void CmdSetPlayerHolding(GameObject bhObject)
    {
        RpcSetPlayerHolding(bhObject);
    }

    [ClientRpc]
    public void RpcSetPlayerHolding(GameObject bhObject)
    {
        BallHandling bh = bhObject.GetComponent<BallHandling>();
        PlayerHolding = bh;
    }
}
