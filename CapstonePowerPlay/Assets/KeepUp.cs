using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class KeepUp : MonoBehaviour
{
    [SerializeField]
    private bool LockX = false;
    [SerializeField]
    private bool LockY = false;
    [SerializeField]
    private bool LockZ = false;
    [SerializeField]
    private Transform _player;
   
	
	// Update is called once per frame
	void Update ()
    {
		if(LockX)
        {
            transform.position = new Vector3(_player.position.x, transform.position.y, transform.position.z);
        }

        if(LockY)
        {
            transform.position = new Vector3(transform.position.x, _player.position.y, transform.position.z);
        }

        if (LockZ)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, _player.position.z);
        }
    }
}
