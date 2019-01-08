using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Camera_ProxyFade : MonoBehaviour {


    [SerializeField]
    private Renderer playerRenderer = null;

    [SerializeField]
    private Material fadingMaterial = null;

    [SerializeField]
    private Material standardMaterial = null;
   

    // can only fade mats if their renderin mode is eitehr fade or transparent
    // change the mat to an exact copy of the mat with the only rendering mode being change back and fourth between Opaque and Fade/Transperant 


	// Update is called once per frame
	void Update () {

        // change this to see the distance between player and their camera
        ///

        //if(Input.GetKeyDown(KeyCode.Q))
        //{
        //    Debug.Log("Q has been pressed");
        //    // set the material to fade

        //    playerRenderer.material = new Material(Shader.Find("Fade"));
        //}

        //if(Input.GetKey(KeyCode.R))
        //{
        //    Debug.Log("R is being pressed");
        //    FadeOut();
        //}
        //if(Input.GetKey(KeyCode.T))
        //{
        //    Debug.Log("T is being pressed");
        //    FadeIn();
        //}

    }


    private void FadeOut()
    {
        if(playerRenderer.material.color.a>0)
        {
            Debug.Log("fading");
            playerRenderer.material.color = new Color(playerRenderer.material.color.r, playerRenderer.material.color.g, playerRenderer.material.color.b, playerRenderer.material.color.a - Time.deltaTime / 2);
        }
        Debug.Log("playerRenderer.material.color.a : " + playerRenderer.material.color.a);
    }

    private void FadeIn()
    {
        if (playerRenderer.material.color.a < 1)
        {
            Debug.Log("fading");
            playerRenderer.material.color = new Color(playerRenderer.material.color.r, playerRenderer.material.color.g, playerRenderer.material.color.b, playerRenderer.material.color.a + Time.deltaTime / 2);
        }
        Debug.Log("playerRenderer.material.color.a : " + playerRenderer.material.color.a);
    }

}
