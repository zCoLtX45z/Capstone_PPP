using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalGoalScript : MonoBehaviour {

    private string team1Tag;
    private string team2Tag;

    private ScoreTracker sTracker;

    // Use this for initialization
    void Start () {
        // temp
        team1Tag = "tempTag";
        // no team 2 yet
        team2Tag = "Player";

        sTracker = FindObjectOfType<ScoreTracker>();

	}
	
    
    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "Ball")
        {
            string tempString = c.transform.GetComponent<Ball>().teamTag;

            if (tempString == team1Tag)
            {
                Debug.Log("team1Scored");
                sTracker.AddScoreToTeam1(1);
            }

            else if (tempString == team2Tag)
            {
                Debug.Log("team2Scored");
                sTracker.AddScoreToTeam2(1);
            }

            else
            {
                Debug.LogError("Ball collided with goal, but the team tag variable does not match the tags of either team!");
            }
        }
    }
}
