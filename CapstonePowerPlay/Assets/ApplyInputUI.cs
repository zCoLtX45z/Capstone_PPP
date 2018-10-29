using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApplyInputUI : MonoBehaviour {

    [SerializeField]
    private InputField Input;
    [SerializeField]
    private hoverBoardScript HB;
    
    public void ChangeHoverBoardKp(string newStr)
    {
        HB.ChangeKp(float.Parse(newStr));
    }

    public void ChangeHoverBoardKi(string newStr)
    {
        HB.ChangeKi(float.Parse(newStr));
    }

    public void ChangeHoverBoardKd(string newStr)
    {
        HB.ChangeKd(float.Parse(newStr));
    }
}
