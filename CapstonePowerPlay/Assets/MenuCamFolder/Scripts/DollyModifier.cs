using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DollyModifier : MonoBehaviour {


    [SerializeField]
    private float m_maxSpeed;

    [SerializeField]
    private float m_speed;

    //[SerializeField]
   // private float slowDownModifier;

    [SerializeField]
    private float trackSlowDownDistanceModifier;

    [SerializeField]
    private float trackSpeedUpDistanceModifier;

    //[SerializeField]
    private float trackLength;


    [SerializeField]
    private CinemachineDollyCart cameraDollyCart;

    [SerializeField]
    private CinemachineSmoothPath path;

    [SerializeField]
    private int currentTrackNumber;


    [SerializeField]
    private float acceleration;
    
    public bool reachedSpeedMax;
    public bool reachedSpeedMaxRev;


    public bool allowMovement;

    public bool reverse;
  


    private void Start()
    {
        cameraDollyCart = transform.GetComponent<CinemachineDollyCart>();
        //m_speed = m_maxSpeed;
        //cameraDollyCart.m_Speed = m_speed;

        cameraDollyCart.m_Path = path;

        trackLength = path.PathLength;



    }


    // Update is called once per frame
    void Update () {
        if (allowMovement)
        {
            

            if (!reverse)
            {
                if (m_speed >= m_maxSpeed)
                {
                    reachedSpeedMax = true;
                }
                if (!reachedSpeedMax)
                {
                    m_speed +=(acceleration * Time.deltaTime);
                    m_speed = Mathf.Clamp(m_speed, 0, m_maxSpeed);
                }
                else
                {
                    m_speed = Mathf.Clamp((cameraDollyCart.m_Path.PathLength - cameraDollyCart.m_Position), 0, m_maxSpeed);
                }
            }

            else
            {
                if(m_speed <= -m_maxSpeed)
                {
                    reachedSpeedMaxRev = true;
                }

                if (!reachedSpeedMaxRev)
                {
                    m_speed -= (acceleration * Time.deltaTime);
                    m_speed = Mathf.Clamp(m_speed, -m_maxSpeed, 0);
                }

                else
                {
                    Debug.Log("achieved reverse max speed");
                    m_speed = Mathf.Clamp((0 - cameraDollyCart.m_Position), -m_maxSpeed, 0);
                }
                Debug.Log("Speed rev: " + (0 - cameraDollyCart.m_Position));
            }
            cameraDollyCart.m_Speed = m_speed;
        }
    }


  



}
