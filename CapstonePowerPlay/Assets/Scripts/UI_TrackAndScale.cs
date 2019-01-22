using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_TrackAndScale : MonoBehaviour {

    [SerializeField]
    private RectTransform RT;

    private PlayerColor Target;
    private PlayerColor[] Targets;

    [SerializeField]
    private float ReferenceDistance = 5;
    private Vector3 Direction;
    private Vector3 DefaultScale;
    private Vector3 NewScale;
	// Use this for initialization
	void Start () {
        FindTarget();
        DefaultScale = RT.localScale;
    }
	
	// Update is called once per frame
	void Update () {
		if (Target == null)
        {
            FindTarget();
        }
        else
        {
            Direction = Target.transform.position - transform.position;
            RT.LookAt(Target.transform);
            if (Direction.magnitude > ReferenceDistance)
            {
                NewScale = DefaultScale * Direction.magnitude / ReferenceDistance;
            }
            else
            {
                NewScale = DefaultScale;
            }
            RT.localScale = NewScale;
        }
	}

    private void FindTarget()
    {
        Targets = FindObjectsOfType<PlayerColor>();
        if (Targets != null)
        {
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
}
