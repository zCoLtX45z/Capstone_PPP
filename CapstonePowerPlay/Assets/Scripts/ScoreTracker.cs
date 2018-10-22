using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour {

    private float team1Score;
    private float team2Score;

    private Text t1Scoring;
    private Text t2Scoring;
    // Use this for initialization
    void Start () {
        t1Scoring = transform.GetChild(0).GetComponent<Text>();
        t2Scoring = transform.GetChild(1).GetComponent<Text>();

        team1Score = 0;
        team2Score = 0;

        t1Scoring.text = "Team1: " + team1Score.ToString();
        t2Scoring.text = "Team2: " + team2Score.ToString();
    }
	
    public void AddScoreToTeam1(float score)
    {
        team1Score += score;
        t1Scoring.text = "Team1: " + team1Score.ToString();
        //Scored();
    }
    public void AddScoreToTeam2(float score)
    {
        team2Score += score;
        t2Scoring.text = "Team2: " + team2Score.ToString();
        //Scored();
    }

    /*
    private void Scored()
    {

    }
    */
}
