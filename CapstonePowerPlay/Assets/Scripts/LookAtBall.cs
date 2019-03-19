using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtBall : MonoBehaviour
{

    private Transform lookatBall;

    [SerializeField]
    private BallHandling thisPlayerBallHandling;


    public bool allow = false;

    //public List<Transform> players = new List<Transform>();


    private CameraModeMedium cMM;

    [SerializeField]
    private Transform upRotationRootObj;


    [SerializeField]
    private float maxAngle;

    [SerializeField]
    private float speedRot;

    public bool hardLock = false;
    [SerializeField]
    private Transform blueHandTransform;
    [SerializeField]
    private Transform redHandTransform;

    // Use this for initialization
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Ball") != null)
            lookatBall = GameObject.FindGameObjectWithTag("Ball").transform;
       
        cMM = transform.GetComponent<CameraModeMedium>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lookatBall != null)
        {
            if (allow)
            {

                if (lookatBall.parent != blueHandTransform && lookatBall.parent != redHandTransform)
                {
                    if (!hardLock)
                    {
                        Vector3 direction = lookatBall.position - transform.position;
                        float angle = Vector3.Angle(direction, transform.forward);
                        if (angle >= maxAngle)
                        {
                            Quaternion rot = Quaternion.LookRotation(direction);
                            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, speedRot * angle * Time.deltaTime);
                        }
                        else
                            hardLock = true;
                    }
                    else
                    {
                        transform.LookAt(lookatBall, upRotationRootObj.up);
                    }
                }
                else
                {
                    Debug.Log("Else");
                    cMM.ChangeCameraMode();
                }
               
            }
        }
        else
        {
            if (GameObject.FindGameObjectWithTag("Ball") != null)
                lookatBall = GameObject.FindGameObjectWithTag("Ball").transform;
        }
    }
}