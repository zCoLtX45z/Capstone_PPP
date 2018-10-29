using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow_And_Look : MonoBehaviour {

    [SerializeField]
    private Transform Target;

    //[SerializeField]
    //private Transform LookAtTarget;

    private Transform T;

    // Use this for initialization
    void Start()
    {
        T = transform;
    }

    // Update is called once per frame
    void Update()
    {
        T.position = Target.position;
        //T.rotation = Target.rotation;

        //T.rotation = Quaternion.Euler(new Vector3(T.rotation.x, Target.rotation.y * 180 / Mathf.PI, T.rotation.z));
        
    }
}
