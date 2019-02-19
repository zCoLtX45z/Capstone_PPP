using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class IKControl : NetworkBehaviour {

    [SerializeField]
    private AnimationController AC;
    [SerializeField]
    private PlayerColor PC;

    [SerializeField]
    private GameObject Avatar;

    [SerializeField]
    private Animator Animator;

    [SerializeField]
    private Transform LookAtPos;
    [SerializeField]
    private Transform LeftShoulderHint;
    [SerializeField]
    private Transform RightShoulderHint;
    [SyncVar]
    private Vector3 TargetPosition;
    // Use this for initialization

    [Command]
    public void CmdUpdateTargetPosition(Vector3 pos)
    {
        RpcUpdateTargetPosition(pos);
    }
    [ClientCallback]
    public void RpcUpdateTargetPosition(Vector3 pos)
    {
        TargetPosition = pos;
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (Avatar.activeSelf)
        {
            if (PC.LocalPlayer == PC.ParentPlayer)
            {
                TargetPosition = LookAtPos.position;
                AC.CmdUpdateTargetPosition(TargetPosition);
            }
            Animator.SetLookAtPosition(AC.LookPos);
            Animator.SetLookAtWeight(1);
            AnimatorClipInfo[] clipInfo = Animator.GetCurrentAnimatorClipInfo(layerIndex);
            //Debug.Log("Clip: " + clipInfo[0].clip);
            if ("" + clipInfo[0].clip == "Pass (UnityEngine.AnimationClip)" || "" + clipInfo[0].clip == "Shoot (UnityEngine.AnimationClip)")
            {
                //Debug.Log("Clip Passed Enter ");
                Animator.SetIKPosition(AvatarIKGoal.RightHand, TargetPosition);
                Animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                Animator.SetIKPosition(AvatarIKGoal.LeftHand, TargetPosition);
                Animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                //Animator.SetIKHintPosition(AvatarIKHint.)
            }
            else
            {
                Animator.SetIKPosition(AvatarIKGoal.RightHand, TargetPosition);
                Animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                Animator.SetIKPosition(AvatarIKGoal.LeftHand, TargetPosition);
                Animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
            }
        }
    }
}
