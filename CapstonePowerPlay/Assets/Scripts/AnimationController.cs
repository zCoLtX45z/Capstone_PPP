using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimationController : MonoBehaviour {

    // Player Components
    [SerializeField]
    private Animator BlueAnimator;
    [SerializeField]
    private Animator RedAnimator;
    [SerializeField]
    private GameObject BlueAvatar;
    [SerializeField]
    private GameObject RedAvatar;
    [SerializeField]
    private hoverBoardScript HoverBoard;
    [SerializeField]
    private PlayerColor PC;

    // Animation Components
    //[SyncVar(hook = "PassAnimation")]
    private bool Pass = false;
    //[SyncVar(hook = "JumpAnimation")]
    private bool Jump = false;
    //[SyncVar(hook = "UpdateGrounded")]
    private bool Grounded = true;
    //[SyncVar(hook = "UpdateSpeedRatio")]
    private float SpeedRatio = 0;
    [SerializeField]
    private Transform LookAtPosition;
    [HideInInspector]
    public Vector3 LookPos;
    [SerializeField]
    private PhotonView PV;

    
    public void UpdateTargetPosition(Vector3 pos)
    {
        //RpcUpdateTargetPosition(pos);
        PV.RPC("RPC_UpdateTargetPosition", RpcTarget.All, pos);
    }
    [PunRPC]
    public void RPC_UpdateTargetPosition(Vector3 pos)
    {
        LookPos = pos;
    }
    
    public void PassAnimation()
    {
        //RpcPassAnimation();
        PV.RPC("RPC_PassAnimation", RpcTarget.All);
    }
    [PunRPC]
    public void RPC_PassAnimation()
    {
        Pass = true;
        PassAnimation(Pass);
    }
    private void PassAnimation(bool pass)
    {
        Pass = pass;
        if (Pass == true)
        {
            if (RedAvatar.activeSelf)
            {
                RedAnimator.SetTrigger("Pass");
            }
            else if (BlueAvatar.activeSelf)
            {
                BlueAnimator.SetTrigger("Pass");
            }
        }
        Pass = false;
    }

    
    public void JumpAnimation()
    {
        //RpcJumpAnimation();
        PV.RPC("RPC_JumpAnimation", RpcTarget.All);
    }
    [PunRPC]
    public void RPC_JumpAnimation()
    {
        Jump = true;
        JumpAnimation(Jump);
    }
    private void JumpAnimation(bool jump)
    {
        Jump = jump;
        if (Jump == true)
        {
            if (RedAvatar.activeSelf)
            {
                RedAnimator.SetTrigger("Jump");
            }
            else if (BlueAvatar.activeSelf)
            {
                BlueAnimator.SetTrigger("Jump");
            }
        }
        Jump = false;
    }

    
    public void UpdateSpeedRatio1(float ratio)
    {
        //RpcUpdateSpeedRatio(ratio);
        PV.RPC("RPC_UpdateSpeedRatio", RpcTarget.All, ratio);
    }
    [PunRPC]
    public void RPC_UpdateSpeedRatio(float ratio)
    {
        SpeedRatio = ratio;
        UpdateSpeedRatio(SpeedRatio);
    }

    public void UpdateSpeedRatio(float ratio)
    {
        SpeedRatio = ratio;
        if (RedAvatar.activeSelf)
        {
            RedAnimator.SetFloat("SpeedRatio", SpeedRatio);
        }
        else if (BlueAvatar.activeSelf)
        {
            BlueAnimator.SetFloat("SpeedRatio", SpeedRatio);
        }
    }

    private void UpdateGrounded(bool grounded)
    {
        Grounded = grounded;
        if (RedAvatar.activeSelf)
        {
            RedAnimator.SetBool("Grounded", Grounded);
        }
        else if (BlueAvatar.activeSelf)
        {
            BlueAnimator.SetBool("Grounded", Grounded);
        }
    }
    
    public void UpdateGrounded1(bool grounded)
    {
        //RpcUpdateGrounded(Grounded);
        PV.RPC("RPC_UpdateGrounded", RpcTarget.All, grounded);
    }
    [PunRPC]
    public void RPC_UpdateGrounded(bool grounded)
    {
        Grounded = grounded;
        UpdateGrounded(Grounded);
    }

    public GameObject ReturnBlueAvatar()
    {
        return BlueAvatar;
    }
    public GameObject ReturnRedAvatar()
    {
        return RedAvatar;
    }

    public Animator ReturnBlueAnimator()
    {
        return BlueAnimator;
    }

    public Animator ReturnRedAnimator()
    {
        return RedAnimator;
    }

    public Transform ReturnLookPos()
    {
        return LookAtPosition;
    }
}
