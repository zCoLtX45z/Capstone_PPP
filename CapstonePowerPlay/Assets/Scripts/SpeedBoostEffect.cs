using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostEffect : MonoBehaviour {


    [SerializeField]
    private float maxTime;

    private float timeLeft;

    private hoverBoardScript hBS;


    [SerializeField]
    private float speedBoostPercentage;

    private bool boosting = false;
    // Use this for initialization
    void Start()
    {
        hBS = transform.GetComponent<hoverBoardScript>();
        hBS.SpeedBoostPadLinearPercent = 0;
        boosting = false;
    }


    private void Update()
    {
        // Debug.Log("velo: " + rb.velocity.magnitude);
        if (timeLeft > 0 && boosting)
        {
            Debug.Log("Time ticks");
            timeLeft -= Time.deltaTime;
        }
        else if (timeLeft <= 0 && boosting)
        {
            Debug.Log("End boost");
            boosting = false;
            hBS.SpeedBoostPadLinearPercent = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BoostPad")
        {
            Debug.Log("Boosting");
            hBS.SpeedBoostPadLinearPercent = speedBoostPercentage;
            timeLeft = maxTime;
            boosting = true;
        }
    }

}
