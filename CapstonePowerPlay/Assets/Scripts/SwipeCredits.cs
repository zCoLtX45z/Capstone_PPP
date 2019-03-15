using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeCredits : MonoBehaviour {

    [SerializeField]
    private int numberOfCreditSections;

   // [SerializeField]
    //private Vector2 targetPosition;

    [SerializeField]
    private float acceleration;

    [SerializeField]
    private Transform slide;

    private int currentSlide = 0;

    [SerializeField]
    private float distanceBetweenObjs;

    private bool directionIsRight;
    
    private float xTarget;

    [SerializeField]
    private GameObject nextButton;
    [SerializeField]
    private GameObject prevButton;
    


    public void DirectionRight(bool right)
    {
        directionIsRight = right;


        if(directionIsRight)
        {
            xTarget -= distanceBetweenObjs;
            currentSlide++;
        }
        else
        {
            xTarget += distanceBetweenObjs;
            currentSlide--;
        }


        Debug.Log("is it right: " + directionIsRight);
    }

    private void FixedUpdate()
    {
        if(nextButton.activeInHierarchy)
        {
            if(currentSlide == (numberOfCreditSections - 1))
            {
                nextButton.SetActive(false);
            }
        }
        else
        {
            if(currentSlide != (numberOfCreditSections - 1))
            {
                nextButton.SetActive(true);
            }
        }

        if (prevButton.activeInHierarchy)
        {
            if (currentSlide == 0)
            {
                prevButton.SetActive(false);
            }
        }
        else
        {
            if (currentSlide != 0)
            {
                prevButton.SetActive(true);
            }
        }



        slide.localPosition += new Vector3(((xTarget - slide.localPosition.x) / acceleration), slide.localPosition.y , 0);

        //selectedRect.localPosition += new Vector3(0, (target.localPosition.y - selectedRect.localPosition.y) / acceleration, 0);
    }


}
