using UnityEngine;
using System.Collections;

public class ManualCannonFire : MonoBehaviour {

	public Transform ViewCamera;

	public Transform LeftCannonCameraLocation;

	bool cannonView = false;

	NavalCameraMovement navalCameraMovement;
	WeaponCameraMovement weaponCameraMovement;
	MenuSelectedShip menuSelectedShip;

	void Start () {



	}

	void Update () {

		ViewCamera = GameObject.Find ("Main Camera").GetComponent<Transform> ();
		menuSelectedShip = GetComponent<MenuSelectedShip> ();

		LeftCannonCameraLocation = GameObject.Find ("Player/" + /*menuSelectedShip.ShipName*/ "Brig" + "/Nodes/LeftGuns/gun4/CannonStand").GetComponent<Transform> ();;

		if (Input.GetKeyDown (KeyCode.F) && cannonView == false) {
			navalCameraMovement = GameObject.Find("Main Camera").GetComponent<NavalCameraMovement> ();
			weaponCameraMovement = GameObject.Find("Main Camera").GetComponent<WeaponCameraMovement> ();
			navalCameraMovement.enabled = false;
			weaponCameraMovement.enabled = true;

			ViewCamera.transform.parent = LeftCannonCameraLocation.transform;
			ViewCamera.localPosition = new Vector3 (0f, 4f, -5f);
			ViewCamera.localEulerAngles = new Vector3 (0f, 0f, 0f);
			//ViewCamera.position = new Vector3(LeftCannonCameraLocation.position.x, LeftCannonCameraLocation.position.y + 3f, LeftCannonCameraLocation.position.z);
			//ViewCamera.eulerAngles = new Vector3(LeftCannonCameraLocation.eulerAngles.x, LeftCannonCameraLocation.eulerAngles.y, LeftCannonCameraLocation.eulerAngles.z);
			//ViewCamera.transform.parent = LeftCannonCamaraLocation.transform;
			Debug.Log("You Are Here");
			cannonView = true;
		} else if (Input.GetKeyDown (KeyCode.F) && cannonView == true) {
			weaponCameraMovement.enabled = false;
			navalCameraMovement.enabled = true;

			ViewCamera.transform.parent = null;
			cannonView = false;
		}

		if (cannonView == true && Input.GetMouseButtonDown (1)) {
			ViewCamera.GetComponent<Camera>().fieldOfView = 40f;
		}
		if (cannonView == true && Input.GetMouseButtonUp (1)) {
			ViewCamera.GetComponent<Camera>().fieldOfView = 80f;
		}
	}
}
