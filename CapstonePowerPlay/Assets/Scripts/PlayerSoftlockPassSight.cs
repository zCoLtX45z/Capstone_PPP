using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoftlockPassSight : MonoBehaviour {

    [SerializeField]
    private float softLockAngle = 40f;

    [SerializeField]
    private float distanceToTarget;

    [SerializeField]
    private List<GameObject> listOfTeamates = new List<GameObject>();


    private Vector3 directionFromPlayer;
    [SerializeField]
    private float angle = 0;

    [SerializeField]
    public GameObject target;
    

        [SerializeField]
    private List<GameObject> currentAcceptedTargets = new List<GameObject>();

    private float currentClossestAngle;

    private float currentAngle;

    // team gameobject tag
    private string teamTag;

    [SerializeField]
    private List<GameObject> playerAndChildren = new List<GameObject>();

    //private Vector3 playerPosition;

    // player and child colliders
    //[SerializeField]
    //private List<GameObject> pAndCColliders = new List<GameObject>();


    private Transform teater;


    private GameObject player;

    // Use this for initialization
    void Awake () {
        teamTag = transform.root.tag;
        softLockAngle = 20f;
        foreach (GameObject playerObj in GameObject.FindGameObjectsWithTag(teamTag))
        {
            if (playerObj != transform.gameObject)
            {
                listOfTeamates.Add(playerObj);
            }
        }

        for (int i = listOfTeamates.Count - 1; i >= 0; --i)
        {
            if(listOfTeamates[i].gameObject == transform.root.gameObject)
            {
                listOfTeamates.RemoveAt(i);
                break;
            }
        }
        playerAndChildren.Add(transform.root.gameObject);
       
        foreach (Transform child in transform.root)
        {
            playerAndChildren.Add(child.gameObject);
            //child is your child transform
        }

        player = transform.root.gameObject;


    }
	
	// Update is called once per frame
	void Update () {
        
      /* 
        List<GameObject> tempList = new List<GameObject>();

        foreach (GameObject playerObj in GameObject.FindGameObjectsWithTag(teamTag))
        {
            if (playerObj != transform.gameObject)
            {
                tempList.Add(playerObj);
            }
        }


        for (int j = tempList.Count - 1; j >= 0; j--)
        {
            int tempNumberOfPasses = 0;

            for (int i = listOfTeamates.Count - 1; i >= 0; --i)
            {


                if (tempList[j] != listOfTeamates[i] || tempList[j] != transform.gameObject)
                {
                    tempNumberOfPasses++;

                }

                if (tempNumberOfPasses == listOfTeamates.Count + 1)
                {

                    listOfTeamates.Add(tempList[j]);
                    break;
                }

                Debug.Log("i: " + i);
            }
            Debug.Log("Number of passes: " + tempNumberOfPasses);
        }
       
        */




        for (int i = 0; i < listOfTeamates.Count; i++)
        {
            directionFromPlayer = listOfTeamates[i].transform.position - transform.position;
            angle = Vector3.Angle(directionFromPlayer, transform.forward);

            
            RaycastHit tempHit = new RaycastHit();
            

            if (Physics.Raycast(transform.position, directionFromPlayer, out tempHit, Mathf.Infinity))
            {
                
                if (tempHit.transform.gameObject == listOfTeamates[i])
                {
                    if (angle < softLockAngle)
                    {
                        int temp = 0;
                        Debug.Log("TEMP: " + temp);
                        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z), directionFromPlayer, Color.blue);

                        if (currentAcceptedTargets.Count > 0)
                        {
                            for (int j = 0; j < currentAcceptedTargets.Count; j++)
                            {
                                if (currentAcceptedTargets[j] == listOfTeamates[i])
                                {
                                    Debug.Log("acceptedTargets == number of teamates");
                                    break;
                                }
                                else
                                {
                                    Debug.Log("acceptedTargets != number of teamates!");

                                    temp += 1;
                                }
                                if (temp >= currentAcceptedTargets.Count)
                                {
                                    Debug.Log("Add player to accepted targets list");

                                    currentAcceptedTargets.Add(listOfTeamates[i].gameObject);
                                }
                            }
                        }
                        else
                        {
                            currentAcceptedTargets.Add(listOfTeamates[i].gameObject);
                        }
                    }
                    else
                    {
                        for (int j = 0; j < currentAcceptedTargets.Count; j++)
                        {
                            if (currentAcceptedTargets[j] == listOfTeamates[i])
                            {
                                currentAcceptedTargets.Remove(currentAcceptedTargets[j]);
                            }
                        }
                        Debug.DrawRay(transform.position, directionFromPlayer, Color.red);
                    }
                }
                else
                {
                    for (int j = 0; j < currentAcceptedTargets.Count; j++)
                    {
                        if (currentAcceptedTargets[j] == listOfTeamates[i])
                        {
                            Debug.Log("test: " + currentAcceptedTargets[j]);
                            currentAcceptedTargets.Remove(currentAcceptedTargets[j]);
                        }
                    }
                    Debug.DrawRay(transform.position, directionFromPlayer, Color.red);
                }
            }                
        }
        currentClossestAngle = 360;
        target = null;

        for (int i = 0; i < currentAcceptedTargets.Count; i++)
        {
            Vector3 currentObjectDirection;
            currentObjectDirection = currentAcceptedTargets[i].transform.position - transform.position;
            currentAngle = Vector3.Angle(currentObjectDirection, transform.forward); 
                if (currentAngle <= currentClossestAngle)
                {
                    currentClossestAngle = currentAngle;
                    target = currentAcceptedTargets[i].gameObject;
                }
        }
        if (target != null)
        {
            Vector3 targetObjectDirection = target.transform.position - transform.position;
            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z), targetObjectDirection, Color.yellow);
        }




        // minus because of player
        int tempNumberCheck = -1;

        // lsit of gathered obj
        List<GameObject> newTeamateSearch = new List<GameObject>();

        // check if a new teamate has entered the game
        foreach (GameObject playerObj in GameObject.FindGameObjectsWithTag(teamTag))
        {
            newTeamateSearch.Add(playerObj);
            tempNumberCheck++;
        }
        Debug.Log("tempNumberCheck: " + tempNumberCheck + "  teamateNumber: " + listOfTeamates.Count);

        if (tempNumberCheck > listOfTeamates.Count)
        {
            Debug.Log("surplus!!");

            for (int i = newTeamateSearch.Count - 1; i >= 0; --i)
            {
                int numberOfPasses = 0;
                for (int j = listOfTeamates.Count - 1; j >= 0; --j)
                {
                    numberOfPasses++;

                    if (numberOfPasses >= listOfTeamates.Count && newTeamateSearch[i] != player)
                    {
                        listOfTeamates.Add(newTeamateSearch[i]);
                    }
                }
            }

        }


    }
}


