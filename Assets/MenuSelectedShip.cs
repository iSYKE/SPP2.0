using UnityEngine;
using System.Collections;

public class MenuSelectedShip : MonoBehaviour {

	public string ShipName;

	GameObject InGameShip;
	ShipSelectionController shipSelectionController;

	void Awake() {
		DontDestroyOnLoad (this);
	}

	void Update() {
		shipSelectionController = GetComponent<ShipSelectionController> ();

		InGameShip = GameObject.Find ("Player");
		ShipName = shipSelectionController.ActiveShip;
	}
}
