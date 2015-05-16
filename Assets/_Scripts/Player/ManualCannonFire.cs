using UnityEngine;
using System.Collections;

public class ManualCannonFire : MonoBehaviour {

	public Transform ViewCamera;

	public Transform LeftCannonCamaraLocation;
	public Transform RightCannonCamaraLocation;

	bool cannonView = false;
	bool setupCannonStart = true;
	string currentShipSide;

	int NumberOfLeftGuns;
	int NumberOfRightGuns;

	int currentCannon = 1;

	NavalCameraMovement navalCameraMovement;
	ShipLoadout shipLoadout;

	void OnLevelWasLoaded(int level){
		if (level == 1) {
			LeftCannonCamaraLocation = transform.FindChild((transform.GetComponent<CharacterInventory>().characterCurrentShip.shipName).ToString() + "/Nodes/LeftGuns/gun1/CannonStand").GetComponent<Transform>();
			RightCannonCamaraLocation = transform.FindChild((transform.GetComponent<CharacterInventory>().characterCurrentShip.shipName).ToString() + "/Nodes/RightGuns/gun1/CannonStand").GetComponent<Transform>();

			shipLoadout = GetComponent<ShipLoadout>();
			NumberOfLeftGuns = shipLoadout.leftGuns.Count;
			NumberOfRightGuns = shipLoadout.rightGuns.Count;
		}
	}

	void Start () {

	}

	void Update () {
		ViewCamera = Camera.main.GetComponent<Transform> ();

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
				if(currentCannon > NumberOfLeftGuns) {
					currentCannon = NumberOfLeftGuns;
				} else if (currentCannon < 1) {
					currentCannon = 1;
				}
				LeftCannonCamaraLocation = transform.FindChild((transform.GetComponent<CharacterInventory>().characterCurrentShip.shipName).ToString() + "/Nodes/LeftGuns/gun"+ currentCannon.ToString() +"/CannonStand").GetComponent<Transform>();
				SetCurrentCannon(LeftCannonCamaraLocation);
			}
			if (currentShipSide == "Right") {
				currentCannon --;
				if(currentCannon > NumberOfRightGuns) {
					currentCannon = NumberOfRightGuns;
				} else if (currentCannon < 1) {
					currentCannon = 1;
				}
				RightCannonCamaraLocation = transform.FindChild((transform.GetComponent<CharacterInventory>().characterCurrentShip.shipName).ToString() + "/Nodes/RightGuns/gun"+ currentCannon.ToString() +"/CannonStand").GetComponent<Transform>();
				SetCurrentCannon(RightCannonCamaraLocation);
			}
		}

		if (cannonView == true && Input.GetKeyDown (KeyCode.RightArrow)) {
			if (currentShipSide == "Left") {
				currentCannon --;
				if(currentCannon > NumberOfLeftGuns) {
					currentCannon = NumberOfLeftGuns;
				} else if (currentCannon < 1) {
					currentCannon = 1;
				}
				LeftCannonCamaraLocation = transform.FindChild(transform.GetComponent<CharacterInventory>().characterCurrentShip.shipName + "/Nodes/LeftGuns/gun"+ currentCannon.ToString() +"/CannonStand").GetComponent<Transform>();
				SetCurrentCannon(LeftCannonCamaraLocation);
			}
			if (currentShipSide == "Right") {
				currentCannon ++;
				if(currentCannon > NumberOfRightGuns) {
					currentCannon = NumberOfRightGuns;
				} else if (currentCannon < 1) {
					currentCannon = 1;
				}
				RightCannonCamaraLocation = transform.FindChild((transform.GetComponent<CharacterInventory>().characterCurrentShip.shipName).ToString() + "/Nodes/RightGuns/gun"+ currentCannon.ToString() +"/CannonStand").GetComponent<Transform>();
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
