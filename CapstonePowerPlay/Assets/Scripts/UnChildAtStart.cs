﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnChildAtStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.parent = null;
	}
}
