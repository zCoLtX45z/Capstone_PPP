using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCollision : MonoBehaviour {

    [SerializeField]
    private Shove PlayerShove;

    public Shove GetPlayer()
    {
        return PlayerShove;
    }
    void OnCollisionEnter(Collision c)
    {
        Debug.Log("collided with: " + c.gameObject.name + " " + "tag: " + c.gameObject.tag);
      
        if (c.gameObject.tag == "Shield")
        {
            Debug.Log(c.gameObject.name + " has tag Shield");
            Vector3 dir = c.gameObject.transform.position - gameObject.transform.position;
            PlayerShove.CollideWithPlayer(c.gameObject.GetComponent<ShieldCollision>().GetPlayer(), dir);
        }
        else if (c.gameObject.tag == "Team 1" || c.gameObject.tag == "Team 2")
        {
            Debug.Log(c.gameObject.name + " has tag Player");
            Vector3 dir = c.gameObject.transform.position - gameObject.transform.position;
            PlayerShove.ShovePlayer(c.gameObject.GetComponent<Shove>(), dir);
        }
    }
}
