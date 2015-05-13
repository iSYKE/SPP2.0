using UnityEngine;
using System.Collections;

public class LevelLoad : MonoBehaviour {

	GameObject PlayerShip;
	public string ShipName = "Brig";

	MenuSelectedShip menuSelectedShip;

	void Start () {
		menuSelectedShip = GetComponent<MenuSelectedShip>();


	}

	void Update () {

	}
}
