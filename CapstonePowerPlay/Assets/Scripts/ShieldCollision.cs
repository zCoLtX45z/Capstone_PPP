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
        if (c.gameObject.tag == "Shield")
        {
            Vector3 dir = c.gameObject.transform.position - gameObject.transform.position;
            PlayerShove.CollideWithPlayer(c.gameObject.GetComponent<ShieldCollision>().GetPlayer(), dir);
        }
        else if (c.gameObject.tag == "Player")
        {
            Vector3 dir = c.gameObject.transform.position - gameObject.transform.position;
            PlayerShove.ShovePlayer(c.gameObject.GetComponent<Shove>(), dir);
        }
    }
}
