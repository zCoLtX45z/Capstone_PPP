using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetVisual : MonoBehaviour {

    [SerializeField]
    private Text textTarget;

    [SerializeField]
    private PlayerSoftlockPassSight softLockScript;

    [SerializeField]
    private Camera thisPlayerCam;

    [SerializeField]
    private BallHandling ballHandlingScript; 

	// Use this for initialization
	void Start () {
        ballHandlingScript = GetComponentInParent<BallHandling>();
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("ballHandle hold" + ballHandlingScript.canHold);
        if (softLockScript.target != null || ballHandlingScript.canHold == false)
        {
            if (textTarget.enabled == false)
                textTarget.enabled = true;

            
            Vector3 targetPos = thisPlayerCam.WorldToScreenPoint(new Vector3(softLockScript.target.transform.position.x, 
                softLockScript.target.transform.position.y + 1, softLockScript.target.transform.position.z));
            
          

            

            textTarget.transform.position = targetPos;
        }

        else
        {
            textTarget.enabled = false;
        }
    }
}
