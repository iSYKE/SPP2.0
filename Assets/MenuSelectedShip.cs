using UnityEngine;
using System.Collections;

public class MenuSelectedShip : MonoBehaviour {

	string ShipName = "Sloop";

	GameObject InGameShip;
	ShipSelectionController shipSelectionController;

	void Awake() {
		DontDestroyOnLoad (this);
	}

	void Update() {
		shipSelectionController = GetComponent<ShipSelectionController> ();

		InGameShip = GameObject.Find ("Player");
		//
	}
} 
