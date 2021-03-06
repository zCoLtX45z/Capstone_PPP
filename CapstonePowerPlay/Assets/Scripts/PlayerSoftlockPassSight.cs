﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoftlockPassSight : MonoBehaviour
{

    // the max angle that a teamate must be in between, in order to be an eligable target for passing
    [SerializeField]
    private float softLockAngle = 40f;

    // distance between player and target
    [SerializeField]
    private float distanceToTarget;

    // list of teamates in game
    [SerializeField]
    private List<GameObject> listOfTeamates = new List<GameObject>();

    // direction from the player
    private Vector3 directionFromPlayer;

    // current angle
    [SerializeField]
    private float angle = 0;


    // target of passing
    [SerializeField]
    public Transform target;

    [SerializeField]
    public Vector3 targetPosition;

    // accepted target list, the group of targets that are elligable to be targeted
    [SerializeField]
    private List<GameObject> currentAcceptedTargets = new List<GameObject>();

    // the smallest angle between the player's forward vector and another object
    private float currentClossestAngle;

    // current angle between a target and the player
    private float currentAngle;

    // team gameobject tag
    [SerializeField]
    public string teamTag;

    // list containing main player object and children
    [SerializeField]
    private List<GameObject> playerAndChildren = new List<GameObject>();


    // the max distance for a teamate must be within, in order to be an eligable target for passing
    [SerializeField]
    private float maxDistance = 0.0f;

    // player gameObject
    [SerializeField]
    public GameObject player;

    private bool passed = false;

    


    public void SetChanges()
    {
        Debug.Log("changes setting");
        //set player object (parent object that this script is attached to)
        //player = transform.parent.gameObject;

        // get the tag of the player
        teamTag = player.tag;
        //Debug.Log("teamTag = " + teamTag);
        //Debug.Log("Player name: " + player.name);
        // set soft lock angle (to be removed)
        softLockAngle = 20f;

        // find all objects with the same tag as the player (used to see which is a temate, may change in future)
        foreach (GameObject playerObj in GameObject.FindGameObjectsWithTag(teamTag))
        {
            if (playerObj != transform.gameObject)
            {
                listOfTeamates.Add(playerObj);
            }
        }

        // find the player's gameObject and remove it from teamate list.
        for (int i = listOfTeamates.Count - 1; i >= 0; --i)
        {
            if (listOfTeamates[i].gameObject == player.transform.gameObject)
            {
                listOfTeamates.RemoveAt(i);
                listOfTeamates.TrimExcess();
                break;
            }
        }
        // add player gameObject to the list
        playerAndChildren.Add(player.transform.gameObject);

        // add all children to main player gameObject to this list
        foreach (Transform child in player.transform)
        {
            playerAndChildren.Add(child.gameObject);
            //child is your child transform
        }



        // unchiled this gameObject
        //  transform.parent = null;




        /// if you don't want any distance restrictions (ill advised) the use this
        /// maxDistance = Mathf.Infinity;
        ///

        passed = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (passed)
        {
            // problem the host sees itself as a teamate

            //Debug.Log("listofteamates count: " + listOfTeamates.Count);
            if (listOfTeamates.Count <= 0)
            {
                //Debug.Log("no teamates");
                // find all objects with the same tag as the player (used to see which is a temate, may change in future)
                //Debug.Log("Team tag: " + teamTag);
                foreach (GameObject playerObj in GameObject.FindGameObjectsWithTag(teamTag))
                {
                    if (playerObj != transform.gameObject)
                    {
                        listOfTeamates.Add(playerObj);
                    }
                }

                // find the player's gameObject and remove it from teamate list.
                for (int i = listOfTeamates.Count - 1; i >= 0; --i)
                {
                    if (listOfTeamates[i].gameObject == player)
                    {
                        listOfTeamates.RemoveAt(i);
                        listOfTeamates.TrimExcess();
                        break;
                    }
                }
            }
            else
            {
                // Debug.Log("has teamates");
                // check every teamates within the list
                for (int i = 0; i < listOfTeamates.Count; i++)
                {
                    // find direction from player
                    directionFromPlayer = (listOfTeamates[i].transform.position + ((new Vector3(listOfTeamates[i].transform.up.x, listOfTeamates[i].transform.up.y, listOfTeamates[i].transform.up.z) / 4) * 3)) - transform.position;
                    // get current angle between the current teamate and the player
                    angle = Vector3.Angle(directionFromPlayer, transform.forward);

                    // set distance to target
                    distanceToTarget = directionFromPlayer.magnitude;

                    // create RaycastHit
                    RaycastHit tempHit = new RaycastHit();

                    // send out raycast to the current teamate being referenced
                    if (Physics.Raycast(transform.position, directionFromPlayer, out tempHit, Mathf.Infinity))
                    {
                        //Debug.Log("tempHit has hit: " + tempHit.transform.name + " tempHit should hit: " + listOfTeamates[i]);
                        // if the raycast hits is target
                        if (tempHit.transform.gameObject == listOfTeamates[i])
                        {
                            // if target is within angle
                            if (angle < softLockAngle /*just added distance check->*/&& distanceToTarget < maxDistance)
                            {
                                // number of passes without succes of finding the current teammate being referenced from listOfTeamates
                                int tempNumberOfCurrentAcceptedTargetPasses = 0;

                                //Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z), directionFromPlayer, Color.blue);

                                // if the currentAcceptedTargets list have anyting in the list
                                if (currentAcceptedTargets.Count > 0)
                                {
                                    // check every object in currentAcceptedTargets
                                    for (int j = 0; j < currentAcceptedTargets.Count; j++)
                                    {
                                        // if it equals to the current teammate being referenced from listOfTeamates
                                        if (currentAcceptedTargets[j] == listOfTeamates[i])
                                        {
                                            break;
                                        }
                                        // if it does NOT equals to the current teammate being referenced from listOfTeamates
                                        else
                                        {
                                            // add another pass
                                            tempNumberOfCurrentAcceptedTargetPasses++;
                                        }
                                        // if there is no object in currentAcceptedTargets that equals the teammate being referenced from listOfTeamates
                                        if (tempNumberOfCurrentAcceptedTargetPasses >= currentAcceptedTargets.Count)
                                        {
                                            // add the teammate being referenced from listOfTeamates to currentAcceptedTargets
                                            currentAcceptedTargets.Add(listOfTeamates[i].gameObject);
                                        }
                                    }
                                }
                                // if there is noting in currentAccpeted target list
                                else
                                {
                                    // add the teammate being referenced from listOfTeamates to currentAcceptedTargets
                                    currentAcceptedTargets.Add(listOfTeamates[i].gameObject);
                                }
                            }
                            // if the target is NOT within angle
                            else
                            {
                                // check every object within currentAcceptedTargets list
                                for (int j = 0; j < currentAcceptedTargets.Count; j++)
                                {
                                    // if it equals to the gameObject referenced in listOfTeamates
                                    if (currentAcceptedTargets[j] == listOfTeamates[i])
                                    {
                                        //Debug.Log("removing: " + currentAcceptedTargets[j] + " from acceptableTargets by target not being in view of the angle");
                                        // remove the gameObject referenced in listOfTeamates from currentAcceptedTargets
                                        currentAcceptedTargets.Remove(currentAcceptedTargets[j]);
                                        currentAcceptedTargets.TrimExcess();
                                        // just added...
                                        break;
                                    }
                                }
                                // Debug.DrawRay(transform.position, directionFromPlayer, Color.red);
                            }
                        }
                        // if the raycast does not hit intended target
                        else
                        {
                            // check every object within currentAcceptedTargets list
                            for (int j = 0; j < currentAcceptedTargets.Count; j++)
                            {
                                // if it equals to the gameObject referenced in listOfTeamates
                                if (currentAcceptedTargets[j] == listOfTeamates[i])
                                {
                                    // Debug.Log("removing: " + currentAcceptedTargets[j] + " from acceptableTargets by raycast not connecting");
                                    // remove the gameObject referenced in listOfTeamates from currentAcceptedTargets
                                    currentAcceptedTargets.Remove(currentAcceptedTargets[j]);
                                    currentAcceptedTargets.TrimExcess();
                                    // just added...
                                    break;
                                }
                            }
                            //Debug.DrawRay(transform.position, directionFromPlayer, Color.red);
                        }
                    }
                }
                // reset closest angle to the max of 360 degrees (may change later in favour of a localy instantiated value in update)
                currentClossestAngle = 360;
                // current target reset (may change later in favour of a localy instantiated value in update)
                target = null;
                targetPosition = Vector3.zero;

                // check every object in currentAcceptedTargets, (eligable targets)
                for (int i = 0; i < currentAcceptedTargets.Count; i++)
                {

                    Vector3 currentObjectDirection;
                    // check the direction of the current checked object of currentAcceptedTargets list and the player
                    currentObjectDirection = currentAcceptedTargets[i].transform.position - transform.position;
                    // set the current angle between the current checked object of currentAcceptedTargets list and the player
                    currentAngle = Vector3.Angle(currentObjectDirection, transform.forward);

                    // if the currentClossestAngle variable is less than the currentAngle variable
                    if (currentAngle <= currentClossestAngle)
                    {
                        // set currentClossestAngle as the  currentAngle;
                        currentClossestAngle = currentAngle;
                        // set the target as the currently checked gameObject in the currentAcceptedTargets list 
                        target = currentAcceptedTargets[i].transform;
                        //Debug.Log("Target set as: " + target);
                        // set target position to center the players
                        targetPosition = target.position + ((new Vector3(target.up.x, target.up.y, target.up.z) / 4) * 3);
                    }
                }
                // if there is an elidgable target to pass too
                if (target != null)
                {
                    // draw ray
                    //Vector3 targetObjectDirection = target.transform.position - transform.position;
                    //  Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z), targetObjectDirection, Color.yellow);
                }


                //
                // Section is to check for new players, who've joined after the awake 
                //



                // number of temate to check, minus because of player
                int tempNumberCheck = -1;

                // lsit of gathered obj
                List<GameObject> newTeamateSearch = new List<GameObject>();

                // check if a new teamate has entered the game
                //Debug.Log("TeamTag: " + teamTag);
                foreach (GameObject playerObj in GameObject.FindGameObjectsWithTag(teamTag))
                {
                    // add current playerObj into the newTeamateSearch list
                    newTeamateSearch.Add(playerObj);
                    // add one to tempNumberCheck
                    tempNumberCheck++;
                }

                // if there is more gameObejcts with the same tag as the player that is not in the listOfTeamates list
                if (tempNumberCheck > listOfTeamates.Count)
                {
                    //Debug.Log("tempNumberCheck > listOfTeamates");
                    // for every object in newTeamateSearch
                    for (int i = newTeamateSearch.Count - 1; i >= 0; --i)
                    {
                        // number of for loop passes for loop j
                        int numberOfPasses = 0;
                        // check every object in lisst of temates
                        for (int j = listOfTeamates.Count - 1; j >= 0; --j)
                        {
                            // changed
                            // if the referenced gameObject of the listOfTeamates list,  does NOT equale the referenced object in newTeamateSearch list
                            if (listOfTeamates[j] != newTeamateSearch[i])
                            {
                                // add one to numberOfPasses
                                numberOfPasses++;
                            }
                            // if the referenced gameObject of the listOfTeamates list,  does equale the referenced object in newTeamateSearch list
                            else
                            {
                                // exit loop
                                break;
                            }

                            // if numberOfPasses surpass the number of objects in listOfTeamates and the current referenced object in newTeamateSearch list does not equal to the player
                            // the new teamate has been found
                            if (numberOfPasses >= listOfTeamates.Count && newTeamateSearch[i] != player)
                            {
                                // Debug.Log("added: " + newTeamateSearch[i].name);
                                // add the newly discoverd teamate into the listOfTeamates list variable
                                listOfTeamates.Add(newTeamateSearch[i]);
                            }
                        }
                        // Debug.Log("number of passes: " + numberOfPasses);
                    }

                }
                for (int i = newTeamateSearch.Count - 1; i >= 0; i--)
                {
                    newTeamateSearch[i] = null;
                    newTeamateSearch.TrimExcess();
                }

            }
        }
    }
}


