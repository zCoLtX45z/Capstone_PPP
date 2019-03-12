using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTags : MonoBehaviour {

    [SerializeField]
    private List<GameObject> ChangeTagList = new List<GameObject>();

    public void ChangeObjectTags(string tag)
    {
        foreach (GameObject g in ChangeTagList)
        {
            g.tag = tag;
        }
    }
}
