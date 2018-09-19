using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {

    [SerializeField]
    private float XOffset = 0;
    [SerializeField]
    private float YOffset = 0;
    [SerializeField]
    private float ZOffset = 0;
    [SerializeField]
    private Transform Target;

    private Transform T;
    private Vector3 Offset;

	// Use this for initialization
	void Start () {
        T = transform;
        Offset = new Vector3(XOffset, YOffset, ZOffset);
	}
	
	// Update is called once per frame
	void Update () {
        T.position = Target.position + Offset;
	}
}
