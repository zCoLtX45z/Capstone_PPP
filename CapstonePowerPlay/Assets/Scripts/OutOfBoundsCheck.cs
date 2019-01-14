using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBoundsCheck : MonoBehaviour {

    [SerializeField]
    private hoverBoardScript PlayerBoard;

    //void OnCollisionStay(Collision c)
    //{
    //    if (c.gameObject.tag == "OutOfBounds")
    //    {
    //        PlayerBoard.FlipCharacter();
    //    }
    //}

    void OnTriggerStay(Collider c)
    {
        if (c.gameObject.tag == "OutOfBounds")
        {
            PlayerBoard.FlipCharacter();
        }
    }
}
