using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Camera_ProxyFade : MonoBehaviour {


    [SerializeField]
    private Renderer playerRenderer = null;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void FadeOut()
    {
        while(playerRenderer.material.color.a>0)
        {
            //playerRenderer.material.color -= playerRenderer.material Time.deltaTime / 2;
        }
    }

}
