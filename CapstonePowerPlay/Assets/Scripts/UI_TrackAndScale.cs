using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_TrackAndScale : MonoBehaviour {

    [SerializeField]
    private RectTransform RT;

    private PlayerColor Target;
    private PlayerColor[] Targets;
	// Use this for initialization
	void Start () {
        FindTarget();
    }
	
	// Update is called once per frame
	void Update () {
		if (Target == null)
        {
            FindTarget();
        }
        else
        {
            RT.LookAt(Target.transform);
        }
	}

    private void FindTarget()
    {
        Targets = FindObjectsOfType<PlayerColor>();
        int cnt = 0;
        while (cnt < Targets.Length || Target != null)
        {
            if (Targets[cnt].ParentPlayer == Targets[cnt].LocalPlayer)
            {
                Target = Targets[cnt];
            }
            cnt++;
        }

        if (Target == null)
        {
            Debug.Log("No Target Found");
        }
    }
}
