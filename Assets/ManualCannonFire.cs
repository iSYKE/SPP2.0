using UnityEngine;
using System.Collections;

public class ManualCannonFire : MonoBehaviour {

	public Transform ViewCamera;

	public Transform LeftCannonCamaraLocation;
	public Transform RightCannonCamaraLocation;

	bool cannonView = false;
	bool setupCannonStart = false;
	string currentShipSide;

	int currentCannon = 1;

	NavalCameraMovement navalCameraMovement;
	MenuSelectedShip menuSelectedShip;

	void Start () {



	}

	void Update () {
		ViewCamera = Camera.main.GetComponent<Transform> ();
		menuSelectedShip = GetComponent<MenuSelectedShip> ();

		if (setupCannonStart == false) {
			LeftCannonCamaraLocation = GameObject.Find ("Player/" + /*menuSelectedShip.ShipName*/ "Brig" + "/Nodes/LeftGuns/gun1/CannonStand").GetComponent<Transform> ();
			RightCannonCamaraLocation = GameObject.Find ("Player/" + /*menuSelectedShip.ShipName*/ "Brig" + "/Nodes/RightGuns/gun1/CannonStand").GetComponent<Transform> ();
			setupCannonStart = true;
		}

		if (Input.GetKeyDown (KeyCode.F) && cannonView == false) {
			currentShipSide = "Left";
			EnableCannonView();
			SetCurrentCannon(LeftCannonCamaraLocation);
		} else if (Input.GetKeyDown (KeyCode.F) && cannonView == true) {
			DisableCannonView();
		}

		if (Input.GetKeyDown (KeyCode.G) && cannonView == false) {
			currentShipSide = "Right";
			EnableCannonView();
			SetCurrentCannon(RightCannonCamaraLocation);
		} else if (Input.GetKeyDown(KeyCode.G)) {
			DisableCannonView();
		}

		if (cannonView == true && Input.GetMouseButtonDown (1)) {
			ViewCamera.GetComponent<Camera>().fieldOfView = 40f;
		}
		if (cannonView == true && Input.GetMouseButtonUp (1)) {
			ViewCamera.GetComponent<Camera>().fieldOfView = 80f;
		}

		if (cannonView == true && Input.GetKeyDown (KeyCode.LeftArrow)) {

			if (currentShipSide == "Left") {
				currentCannon ++;
				LeftCannonCamaraLocation = GameObject.Find ("Player/" + /*menuSelectedShip.ShipName*/ "Brig" + "/Nodes/LeftGuns/gun"+ currentCannon.ToString() +"/CannonStand").GetComponent<Transform> ();
				SetCurrentCannon(LeftCannonCamaraLocation);
			}
			if (currentShipSide == "Right") {
				currentCannon --;
				RightCannonCamaraLocation = GameObject.Find ("Player/" + /*menuSelectedShip.ShipName*/ "Brig" + "/Nodes/RightGuns/gun"+ currentCannon.ToString() +"/CannonStand").GetComponent<Transform> ();
				SetCurrentCannon(RightCannonCamaraLocation);
			}
		}

		if (cannonView == true && Input.GetKeyDown (KeyCode.RightArrow)) {
			if (currentShipSide == "Left") {
				currentCannon --;
				LeftCannonCamaraLocation = GameObject.Find ("Player/" + /*menuSelectedShip.ShipName*/ "Brig" + "/Nodes/LeftGuns/gun"+ currentCannon.ToString() +"/CannonStand").GetComponent<Transform> ();
				SetCurrentCannon(LeftCannonCamaraLocation);
			}
			if (currentShipSide == "Right") {
				currentCannon ++;
				RightCannonCamaraLocation = GameObject.Find ("Player/" + /*menuSelectedShip.ShipName*/ "Brig" + "/Nodes/RightGuns/gun"+ currentCannon.ToString() +"/CannonStand").GetComponent<Transform> ();
				SetCurrentCannon(RightCannonCamaraLocation);
			}
		}
	}

	void EnableCannonView() {
			navalCameraMovement = Camera.main.GetComponent<NavalCameraMovement> ();
			navalCameraMovement.enabled = false;
		}

	void SetCurrentCannon(Transform side) {
		ViewCamera.transform.parent = side.transform;
		ViewCamera.localPosition = new Vector3( 0f, 4f, -6f);
		ViewCamera.localEulerAngles = new Vector3(side.localEulerAngles.x, side.localEulerAngles.y, side.localEulerAngles.z);
		
		cannonView = true;
	}

	void DisableCannonView() {
		navalCameraMovement.enabled = true;
			
		ViewCamera.transform.parent = null;
		cannonView = false;
		setupCannonStart = false;
		currentCannon = 1;
		}
}
