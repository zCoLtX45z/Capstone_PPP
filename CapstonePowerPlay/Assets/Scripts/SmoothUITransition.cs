using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmoothUITransition : MonoBehaviour {

    [SerializeField]
    private RectTransform start;

    [SerializeField]
    private RectTransform settings;

    [SerializeField]
    private RectTransform selectedRect;

    [SerializeField]
    private float acceleration;

    private bool allow = false;
    private bool set;


    public void MovePosition(bool up)
    {
        if (!allow)
            allow = !allow;

        set = up;
    }
    private void FixedUpdate()
    {
        if (allow)
        {
            if (set)
            {
                Debug.Log("ON START");
                selectedRect.localPosition += new Vector3(0, (start.localPosition.y - selectedRect.localPosition.y) / acceleration, 0);
            }
            else
            {
                Debug.Log("ON Settings");
                selectedRect.localPosition += new Vector3(0, (settings.localPosition.y - selectedRect.localPosition.y) / acceleration, 0);
            }
        }
    }
}
