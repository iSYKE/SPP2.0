using UnityEngine;
using System.Collections;

public class MenuSelectedShip : MonoBehaviour {

	GameObject InGameShip;

	void Awake() {
		DontDestroyOnLoad (this);
	}

	void Start() {
		InGameShip = GameObject.Find ("Player");
	}
}
