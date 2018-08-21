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
    private bool LockROTX = false;
    [SerializeField]
    private bool LockROTY = false;
    [SerializeField]
    private bool LockROTZ = false;
    [SerializeField]
    private Transform _player;
    [SerializeField]
    private bool LookAtTarget = false;
    [SerializeField]
    private Transform LookAt;


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

        if (LockROTY)
        {
            transform.rotation.eulerAngles.Set(transform.rotation.eulerAngles.x, _player.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        }

        if (LockROTX)
        {
            transform.rotation.eulerAngles.Set(_player.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        }

        if (LockROTZ)
        {
            transform.rotation.eulerAngles.Set(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, _player.rotation.eulerAngles.z);
        }

        if (LookAtTarget)
        {
            transform.LookAt(LookAt);
        }
    }
}
