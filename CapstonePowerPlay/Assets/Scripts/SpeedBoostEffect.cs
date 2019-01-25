using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostEffect : MonoBehaviour {




    private hoverBoardScript hBS;

    [SerializeField]
    private float speedBoost;

    [SerializeField]
    private float maxTime;

    private float timeLeft;

    private bool boosting;

    private void Start()
    {
        hBS = transform.GetComponent<hoverBoardScript>();
        hBS.BoostPaddBosstLinearPercent = 0;
    }



    // create boost pad linear percentage in hover board script 





    //   private hoverBoardScript tpControler;

    private void Update()
    {
        if(timeLeft > 0 && boosting)
        {
            timeLeft -= Time.deltaTime;
        }
        else if(timeLeft <= 0 && boosting)
        {
            boosting = false;
            // hbs boost is set to 0
            hBS.BoostPaddBosstLinearPercent = 0;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "BoostPad")
        {
            //Debug.Log("Boosting");
            timeLeft = maxTime;
            boosting = true;

            // hbs boost is set to speedBoost
            hBS.BoostPaddBosstLinearPercent = speedBoost;
        }
    }

}
