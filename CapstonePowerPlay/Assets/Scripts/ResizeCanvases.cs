using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeCanvases : MonoBehaviour {

    [SerializeField]
    private Transform canvasTransform;


	// Use this for initialization
	void Start () {
        canvasTransform.localScale = new Vector3(Screen.width, Screen.height, 1);
	}
}
