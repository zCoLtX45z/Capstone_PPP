﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ResizeCanvases : MonoBehaviour {
	void Update () {
        transform.localScale = new Vector3(Screen.width, Screen.height, 1);
	}
}
