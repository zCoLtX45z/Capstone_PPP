using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour {

    private float team1Score;
    private float team2Score;

    [SerializeField]
    public List<Text> t1Scoring = new List<Text>();
    [SerializeField]
    public List<Text> t2Scoring = new List<Text>();
    [SerializeField]
    public GameObject[] scoreCanvases;

    // Use this for initialization
    void Start () {
        //t1Scoring = transform.GetChild(0).GetComponent<Text>();
        //t2Scoring = transform.GetChild(1).GetComponent<Text>();

        for (int i = 0; i < scoreCanvases.Length; i++)
        {
            //Debug.Log("i: " + i);
            t1Scoring.Add(scoreCanvases[i].transform.GetChild(0).GetComponent<Text>());
            t2Scoring.Add(scoreCanvases[i].transform.GetChild(1).GetComponent<Text>());
        }

        team1Score = 0;
        team2Score = 0;

        //for (int i = 0; i < t1Scoring.Count; i++)
        //{
        //    t1Scoring[i].text = "Team1: " + team1Score.ToString();
        //    t2Scoring[i].text = "Team2: " + team2Score.ToString();
        //}
        
    }
	
    public void AddScoreToTeam1(float score)
    {
        team1Score += score;
        //for (int i = 0; i < t1Scoring.Count; i++)
        //{
        //    t1Scoring[i].text = "Team1: " + team1Score.ToString();
        //}
    }
    public void AddScoreToTeam2(float score)
    {
        team2Score += score;
        //for (int i = 0; i < t1Scoring.Count; i++)
        //{
        //    t2Scoring[i].text = "Team2: " + team2Score.ToString();
        //}
    }
}
