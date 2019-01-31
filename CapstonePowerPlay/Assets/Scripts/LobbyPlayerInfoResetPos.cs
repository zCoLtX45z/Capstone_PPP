using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyPlayerInfoResetPos : MonoBehaviour {

    private RectTransform rectTransform;


	// Use this for initialization
	void Start () {
        rectTransform = transform.GetComponent<RectTransform>();

        rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, rectTransform.localPosition.y, 0);
        rectTransform.eulerAngles = new Vector3(rectTransform.eulerAngles.x, 0, rectTransform.eulerAngles.z);

	}
}
