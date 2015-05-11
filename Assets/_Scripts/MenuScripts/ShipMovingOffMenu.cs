using UnityEngine;
using System.Collections;

public class ShipMovingOffMenu : MonoBehaviour {

	Transform selectedShip;
	Vector3 targetPosition = new Vector3(0f,0f,150f);


	void Update () {
		selectedShip = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
		selectedShip.position = Vector3.Lerp (selectedShip.position, targetPosition, (Time.deltaTime * .1f));

	}
}
