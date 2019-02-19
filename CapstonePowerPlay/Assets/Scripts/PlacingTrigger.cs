using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacingTrigger : MonoBehaviour {

    [HideInInspector]
    public bool TriggerActive = false;
    [HideInInspector]
    public bool InGround = false;
    //[HideInInspector]
    //public Vector3 CollisionPos = Vector3.zero;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Player" || other.gameObject.tag == "Team 1" || other.gameObject.tag == "Team 2" || other.gameObject.tag == "Object" || other.gameObject.tag == "BoostPad")
        {
            TriggerActive = true;
            if (other.gameObject.tag == "Ground")
            {
                InGround = true;
            }
            else
            {
                InGround = false;
            }
        }
        else
        {
            TriggerActive = false;
        }
    }
}
