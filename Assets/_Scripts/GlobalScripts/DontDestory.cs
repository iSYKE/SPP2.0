﻿using UnityEngine;
using System.Collections;

public class DontDestory : MonoBehaviour {

	void Awake() {
		DontDestroyOnLoad (transform.gameObject);
	}

	void Update() {

	}
}
