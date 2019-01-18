using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AnimationController : NetworkBehaviour {

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
    [SyncVar(hook = "PassAnimation")]
    private bool Pass = false;
    [SyncVar(hook = "JumpAnimation")]
    private bool Jump = false;
    [SyncVar(hook = "UpdateGrounded")]
    private bool Grounded = true;
    [SyncVar(hook = "UpdateSpeedRatio")]
    private float SpeedRatio = 0;

    [Command]
    public void CmdPassAnimation()
    {
        Pass = true;
    }
    private void PassAnimation(bool pass)
    {
        Pass = false;
        if (pass == true)
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
    }

    [Command]
    public void CmdJumpAnimation()
    {
        Jump = true;
    }
    private void JumpAnimation(bool jump)
    {
        Jump = false;
        if (jump == true)
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
    }

    [Command]
    public void CmdUpdateSpeedRatio(float ratio)
    {
        SpeedRatio = ratio;
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
    [Command]
    public void CmdUpdateGrounded(bool grounded)
    {
        Grounded = grounded;
    }
}
