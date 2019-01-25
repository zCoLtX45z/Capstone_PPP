using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Shove : NetworkBehaviour {

    // Player Components
    [SerializeField]
    private MeshRenderer Shield;
    [SerializeField]
    private hoverBoardScript Hoverboard;
    private float CurrentSpeed = 0;
    private float MaxSpeed = 0;
    [SerializeField]
    private Rigidbody PlayerRigidBody;
    [SerializeField]
    private PlayerColor PC;

    // Shield Components
    [SyncVar(hook = "ChangeOpacity")]
    public float ShieldOpacity = 0;
    [SyncVar(hook = "ChangeShielding")]
    public float ShieldStrength = 0;
    private float StrengthTimer = 0;
    [SerializeField]
    private float MaxShieldStrength = 3f;
    [SerializeField]
    private float MaxShieldOpacity = 3f;
    [SerializeField]
    private float MaxShoveForce = 50;
    [SerializeField]
    private float MinSpeedToDropBallPercent = 0.5f;
    // Use this for initialization
    void Start () {
        if (PC.LocalPlayer == PC.ParentPlayer)
        {
            MaxSpeed = Hoverboard.GetMaxSpeed();
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (PC.LocalPlayer == PC.ParentPlayer)
        {
            CurrentSpeed = Hoverboard.Speed;
            float opacity = MaxShieldOpacity - Mathf.Abs((MaxSpeed - CurrentSpeed) * MaxShieldOpacity / MaxSpeed;
            if (opacity > MaxShieldOpacity)
            {
                opacity = MaxShieldOpacity;
            }
            float shieldStrenth = CurrentSpeed > MaxSpeed ? 1
                : 1 - (MaxSpeed - CurrentSpeed) / MaxSpeed;
            if (shieldStrenth > 1)
            {
                StrengthTimer += Time.deltaTime;
                float extraShieldStrenth = shieldStrenth + StrengthTimer / 5;
                shieldStrenth += extraShieldStrenth;
                if (shieldStrenth > MaxShieldStrength)
                {
                    shieldStrenth = MaxShieldStrength;
                }
                float extraOpacity = (1 - MaxShieldOpacity) * (MaxShieldStrength - shieldStrenth) / MaxShieldStrength;
                opacity += extraOpacity;
            }
            else
            {
                StrengthTimer = 0;
            }
            if (opacity > 1)
            {
                opacity = 1;
            }

            CmdUpdateShield(opacity, shieldStrenth);
        }
	}

    [Command]
    private void CmdUpdateShield(float Opacity, float Shielding)
    {
        //Debug.Log("Updated Shield: " + Opacity + ", " + Shielding);
        ShieldOpacity = Opacity;
        ShieldStrength = Shielding;
    }

    private void ChangeOpacity(float Opacity)
    {
        Shield.material.SetFloat("Vector1_2C301F3A", 1 - Opacity);
    }

    private void ChangeShielding(float Strength)
    {
        ShieldStrength = Strength;
    }

    public void ShovePlayer(Shove OtherPlayer, Vector3 Direction)
    {
        if (PC.LocalPlayer == PC.ParentPlayer)
        {
            CmdShovePlayer(OtherPlayer.gameObject, Direction);
        }
    }

    public void CollideWithPlayer(Shove OtherPlayer, Vector3 Direction)
    {
        if (PC.LocalPlayer == PC.ParentPlayer)
        {
            CmdCollidePlayer(OtherPlayer.gameObject, Direction);
        }
    }

    [Command]
    private void CmdCollidePlayer(GameObject player, Vector3 Direction)
    {
        Shove shove = player.GetComponent<Shove>();
        if (shove.ShieldStrength / (ShieldStrength + 0.1f) < 0.8f && ShieldStrength > 0.4f)
        {
            BallHandling bh = player.GetComponent<BallHandling>();
            bh.CmdDropBall();
            shove.LaunchPlayer(MaxShoveForce, Direction);
        }
        else
        {
            shove.LaunchPlayer(MaxShoveForce * 0.5f, Direction);
            LaunchPlayer(MaxShoveForce * 0.5f, Direction);
        }
    }

    [Command]
    private void CmdShovePlayer(GameObject player, Vector3 Direction)
    {
        Shove shove = player.GetComponent<Shove>();
        shove.LaunchPlayer(MaxShoveForce, Direction);
        BallHandling bh = player.GetComponent<BallHandling>();
        bh.CmdDropBall();

    }

    public void LaunchPlayer(float Force, Vector3 Direction)
    {
        PlayerRigidBody.AddForce(Force * Direction, ForceMode.Impulse);
    }
}
