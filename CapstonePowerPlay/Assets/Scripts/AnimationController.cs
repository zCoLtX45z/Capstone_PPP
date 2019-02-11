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
    [SerializeField]
    private Transform LookAtPosition;

    [Command]
    public void CmdPassAnimation()
    {
        Pass = true;
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

    [Command]
    public void CmdJumpAnimation()
    {
        Jump = true;
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
