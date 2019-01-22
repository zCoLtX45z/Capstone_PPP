using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_TrackAndScale : MonoBehaviour {

    [SerializeField]
    private RectTransform RT;

    private PlayerColor Target;
    private PlayerColor[] Targets;
	// Use this for initialization
	void Start () {
        Targets = FindObjectsOfType<PlayerColor>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
