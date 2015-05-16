using UnityEngine;
using System.Collections;

public class DontDestroyHUD : MonoBehaviour {

	void Awake() {
		DontDestroyOnLoad (transform.gameObject);
	}

	void Update() {

	}
}
