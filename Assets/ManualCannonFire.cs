using UnityEngine;
using System.Collections;

public class ManualCannonFire : MonoBehaviour {

	public Transform ViewCamera;

	public Transform LeftCannonCamaraLocation;

	bool cannonView = false;

	NavalCameraMovement navalCameraMovement;
	WeaponCameraMovement weaponCameraMovement;
	MenuSelectedShip menuSelectedShip;

	void Start () {



	}

	void Update () {

		ViewCamera = GameObject.Find ("Main Camera").GetComponent<Transform> ();
		menuSelectedShip = GetComponent<MenuSelectedShip> ();

		LeftCannonCamaraLocation = GameObject.Find ("Player/" + /*menuSelectedShip.ShipName*/ "Brig" + "/Nodes/LeftGuns/gun4").GetComponent<Transform> ();;

		if (Input.GetKeyDown (KeyCode.F) && cannonView == false) {
			navalCameraMovement = GameObject.Find("Main Camera").GetComponent<NavalCameraMovement> ();
			weaponCameraMovement = GameObject.Find("Main Camera").GetComponent<WeaponCameraMovement> ();
			navalCameraMovement.enabled = false;
			weaponCameraMovement.enabled = true;

			ViewCamera.transform.parent = LeftCannonCamaraLocation.transform;
			ViewCamera.position = new Vector3(LeftCannonCamaraLocation.position.x+6f, LeftCannonCamaraLocation.position.y + 3f, LeftCannonCamaraLocation.position.z + 6f);
			ViewCamera.eulerAngles = new Vector3(LeftCannonCamaraLocation.eulerAngles.x, LeftCannonCamaraLocation.eulerAngles.y, LeftCannonCamaraLocation.eulerAngles.z);
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
