using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShipSelectionController : MonoBehaviour {

	public GameObject Ship;
	public GameObject PlayerObject;
	public Transform PlayerTransform;
	GameObject SpawningShip;

	public GameObject _ShipSelectionMenuObj;
	public GameObject _WeaponSelectionMenuObj;

	public GameObject startButtonActivateObj;

	ShipMovingOffMenu shipMovingOffMenu;

	public bool readyToStartGame = false;
	
	public void ShipButtons(string buttonName) {
		if (buttonName == "RedSloop") {
			MoveShipOff();
			StartCoroutine(KillShipCoroutine());

		}
		if (buttonName == "BlueSloop") {

		}
		if (buttonName == "YellowSloop") {

		}
		if (buttonName == "Next") {
			TriggerWeaponsMenu();
		}
		if (buttonName == "Cannon1") {

		}
		if (buttonName == "Back") {
			TriggerShipsMenu();
		}
		if (buttonName == "Confirm") {
			foreach( Transform child in GameObject.Find("Player").transform.FindChild("Sloop/Nodes/LeftGuns").transform ){
				
				if(child.GetComponentInChildren<CannonFire>()){
					child.GetComponentInChildren<CannonFire>().doFire = true;
					//Debug.Log(child.name + " FIRED!!!");
				}
			}

			foreach( Transform child in GameObject.Find("Player").transform.FindChild("Sloop/Nodes/RightGuns").transform ){
				
				if(child.GetComponentInChildren<CannonFire>()){
					child.GetComponentInChildren<CannonFire>().doFire = true;
					//Debug.Log(child.name + " FIRED!!!");
				}
			}

			startButtonActivateObj = GameObject.Find ("Canvas/ShipSelectionMenuButtons/StartGameButton");
			startButtonActivateObj.GetComponent<Button>().interactable = true;
		}

	}
	
	void MoveShipOff() {
		shipMovingOffMenu = GetComponent<ShipMovingOffMenu> ();
		shipMovingOffMenu.enabled = true;
	}

	IEnumerator KillShipCoroutine(){
		yield return new WaitForSeconds (10f);
		DestroyCurrentShip ();
		SpawnsSelectedShip ();
		shipMovingOffMenu.enabled = false;
		Debug.Log ("This HAs Been 10Seconds");
	}

	void DestroyCurrentShip() {
		Ship = GameObject.Find("Player/Sloop");
		Destroy (Ship.gameObject);
	}

	void SpawnsSelectedShip() {
		//PlayerObject = GameObject.Find ("Player");
		PlayerTransform = GameObject.Find ("Player").GetComponent<Transform> ();
		PlayerTransform.position = new Vector3 (0f, 0f, -60f);
		SpawningShip = Instantiate (Resources.Load ("Ships/Sloop"), PlayerTransform.position , PlayerTransform.rotation) as GameObject;
		SpawningShip.transform.parent = PlayerTransform;
		SpawningShip.transform.name = "Sloop";
	}

	void TriggerWeaponsMenu() {
		_ShipSelectionMenuObj = GameObject.Find ("Canvas/ShipSelectionMenu");
		_WeaponSelectionMenuObj = GameObject.Find ("Canvas/WeaponSelectionMenu");

		_ShipSelectionMenuObj.GetComponent<Animator> ().SetTrigger ("MenuSlideOff");
		_WeaponSelectionMenuObj.GetComponent<Animator> ().SetTrigger ("WeaponMenuSlideIn");
	}

	void TriggerShipsMenu() {
		_ShipSelectionMenuObj.GetComponent<Animator> ().SetTrigger ("MenuSlideIn");
		_WeaponSelectionMenuObj.GetComponent<Animator> ().SetTrigger ("WeaponMenuSlideOff");
	}
}
