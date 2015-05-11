using UnityEngine;
using System.Collections;

public class ShipSelectionController : MonoBehaviour {

	ShipMovingOffMenu shipMovingOffMenu;
	
	public void ShipButtons(string shipName) {
		if (shipName == "RedSloop") {
			MoveShipOff();
			StartCoroutine(KillShipCoroutine());
		}
		if (shipName == "BlueSloop") {

		}
		if (shipName == "YellowSloop") {

		}
	}
	
	void MoveShipOff() {
		shipMovingOffMenu = GetComponent<ShipMovingOffMenu> ();
		shipMovingOffMenu.enabled = true;
	}

	IEnumerator KillShipCoroutine(){
		yield return new WaitForSeconds (10f);
		shipMovingOffMenu.enabled = false;
		Debug.Log ("This HAs Been 10Seconds");
	}
}
