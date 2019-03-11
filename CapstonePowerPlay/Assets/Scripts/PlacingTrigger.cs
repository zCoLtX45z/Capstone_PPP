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


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Player" || other.gameObject.tag == "Team 1" || other.gameObject.tag == "Team 2" || other.gameObject.tag == "Object" || other.gameObject.tag == "BoostPad")
        {
            //Debug.Log(other.gameObject.name + "P Hit - Trigger");
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
            InGround = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        TriggerActive = false;
        InGround = false;
    }

    //void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Object" || collision.gameObject.tag == "BoostPad")
    //    {
    //        Debug.Log(collision.gameObject.name + "P Hit - Collision");
    //        TriggerActive = true;
    //        if (collision.gameObject.tag == "Ground")
    //        {
    //            InGround = true;
    //        }
    //        else
    //        {
    //            InGround = false;
    //        }
    //    }
    //    else
    //    {
    //        TriggerActive = false;
    //    }
    //}

    public void ResetPT()
    {
        TriggerActive = false;
        InGround = false;
    }

    
}
