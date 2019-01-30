using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTrack : MonoBehaviour {

    [SerializeField]
    private Transform followTransform = null;


    private void Update()
    {
        transform.position = followTransform.position;
    }
}
