using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeCanvases : MonoBehaviour {

    [SerializeField]
    private RectTransform[] canvasTransform;


	// Use this for initialization
	void Start () {
        for (int i = canvasTransform.Length - 1; i >= 0; i--)
        {
            canvasTransform[i].sizeDelta = new Vector2(Screen.width, Screen.height);
        }
        
	}
	
	//// Update is called once per frame
	//void Update () {
 //       Debug.Log("w: " + Screen.currentResolution.width + " / h: " + Screen.currentResolution.height);
 //   }
}
