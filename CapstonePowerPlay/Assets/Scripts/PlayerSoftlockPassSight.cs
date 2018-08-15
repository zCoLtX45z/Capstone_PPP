using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoftlockPassSight : MonoBehaviour {

    [SerializeField]
    private float softLockAngle = 90f;
    //[SerializeField]
    //private bool acceptableTargetInSight;
    //[SerializeField]
    //private GameObject acceptableTargetGameObject;
    [SerializeField]
    private float distanceToTarget;

    //[SerializeField]
    //private SphereCollider col;

    [SerializeField]
    private List<GameObject> listOfTeamates = new List<GameObject>();


    private Vector3 directionFromPlayer;
    [SerializeField]
    private float angle = 0;

    [SerializeField]
    public GameObject target;
    
    //private GameObject currentTarget;

        [SerializeField]
    private List<GameObject> currentAcceptedTargets = new List<GameObject>();


    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {

        for (int i = 0; i < listOfTeamates.Count; i++)
        {
            directionFromPlayer = listOfTeamates[i].transform.position - transform.position;

            Debug.Log("detected somelaop");
            directionFromPlayer = listOfTeamates[i].transform.position - transform.position;
            angle = Vector3.Angle(directionFromPlayer, transform.forward);


            //Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
            //Debug.DrawRay(transform.position, forward, Color.green);



            if (angle < softLockAngle / 2)
            {
                int temp = 0;
                Debug.Log("TEMP: " + temp);
                Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z), directionFromPlayer, Color.blue);




                if(currentAcceptedTargets.Count > 0)
                {
                    for (int j = 0; j < currentAcceptedTargets.Count; j++)
                    {
                        if (currentAcceptedTargets[j] == listOfTeamates[i])
                        {
                            Debug.Log("enteredededeeeedeedeed");
                            break;
                        }
                        else
                        {
                            Debug.Log("wazzup!");

                            temp += 1;
                        }
                        if (temp >= currentAcceptedTargets.Count)
                        {
                            Debug.Log("FISH!!!");

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



        float currentClossestAngle = 360; ;

        float currentAngle;






        

        for (int i = 0; i < currentAcceptedTargets.Count; i++)
        {
            Vector3 currentObjectDirection;
            currentObjectDirection = currentAcceptedTargets[i].transform.position - transform.position;
            currentAngle = Vector3.Angle(currentObjectDirection, transform.forward);


            if(i== 0)
            {
                target = currentAcceptedTargets[i].gameObject;
                currentClossestAngle = currentAngle;

            }

            if (currentAngle <= currentClossestAngle)
            {
                currentClossestAngle = currentAngle;
                target = currentAcceptedTargets[i].gameObject;
            }

            

        }
        Vector3 targetObjectDirection = target.transform.position - transform.position;
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z), targetObjectDirection, Color.yellow);




    }


    
}


