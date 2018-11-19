using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetVisual : MonoBehaviour {

    //[SerializeField]
    //private Text textTarget = null;

        [SerializeField]
    private RawImage targetReticle = null;

    [SerializeField]
    private PlayerSoftlockPassSight softLockScript = null;

    [SerializeField]
    private Camera thisPlayerCam = null;

    [SerializeField]
    private BallHandling ballHandlingScript = null; 

	// Use this for initialization
	void Start () {
        //ballHandlingScript = GetComponentInParent<BallHandling>();
	}
	
	// Update is called once per frame
	void Update () {

       // Debug.Log("ballHandle hold" + ballHandlingScript.canHold);
       // Debug.Log("softLockScript.target: " + softLockScript.target);

      

        if (softLockScript.target != null && ballHandlingScript.ball != null)
        {
            Debug.Log("Hoi");
            //if (textTarget.enabled == false)
            //    textTarget.enabled = true;

            if(targetReticle.enabled == false)
            {
                targetReticle.enabled = true;
            }

            
            Vector3 targetPos = thisPlayerCam.WorldToScreenPoint(new Vector3(softLockScript.target.transform.position.x, 
                softLockScript.target.transform.position.y + 1, softLockScript.target.transform.position.z));





            //textTarget.transform.position = targetPos;
            targetReticle.transform.position = targetPos;
        }

        else
        {
            //textTarget.enabled = false;
            targetReticle.enabled = false;
        }
    }
}
