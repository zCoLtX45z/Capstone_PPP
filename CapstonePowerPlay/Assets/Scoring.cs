using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
    [SerializeField]
    private Text scoreDisplay;
    [SerializeField]
    private int blueScore = 0;
    [SerializeField]
    private int redScore = 0;
    private bool scored = false;
	
	void Start ()
    {
        //scoreDisplay = GetComponentInChildren<Canvas>().GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        HandleScoreCanvas();

    }
    
    public void HandleScoreCanvas()
    {
        scoreDisplay.text = "team#1: " + redScore + "Team#2: " + blueScore;
    }
    public void RedPoint()
    {
        redScore++;
    }
    public void BluePoint()
    {
        blueScore++;
    }
    public void OnTriggerEnter(Collider c)
    {
        if(c.gameObject.tag == "Ball" && !scored)
        {
            if(c.gameObject.GetComponent<PlayerColor>().TeamNum == 1)
            {
                RedPoint();
                scored = true;
            }
            if (c.gameObject.GetComponent<PlayerColor>().TeamNum == 2)
            {
                BluePoint();
                scored = true;
            }
        }
    }
}
