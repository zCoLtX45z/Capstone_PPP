using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmoothHoverSelectorUITransition : MonoBehaviour {

    //[SerializeField]
    //private RectTransform start;

    //[SerializeField]
    //private RectTransform settings;

    [SerializeField]
    private RectTransform[] buttonGroup;


    [SerializeField]
    private RectTransform selectedRect;

    private Transform target;

    [SerializeField]
    private float acceleration;

    public bool allow = false;
    private bool set;


    public void SwitchSelected(Transform button)
    {
        target = button;
    }

    private void FixedUpdate()
    {
        if(target != null)
            selectedRect.localPosition += new Vector3(0, (target.localPosition.y - selectedRect.localPosition.y) / acceleration, 0);
    }


    //public void MovePosition(bool up)
    //{
    //    if (!allow)
    //        allow = !allow;

    //    set = up;
    //}
    //private void FixedUpdate()
    //{
    //    if (allow)
    //    {
    //        if (set)
    //        {
    //            //Debug.Log("ON START");
    //            selectedRect.localPosition += new Vector3(0, (start.localPosition.y - selectedRect.localPosition.y) / acceleration, 0);
    //        }
    //        else
    //        {
    //            //Debug.Log("ON Settings");
    //            selectedRect.localPosition += new Vector3(0, (settings.localPosition.y - selectedRect.localPosition.y) / acceleration, 0);
    //        }
    //    }
    //}
}
