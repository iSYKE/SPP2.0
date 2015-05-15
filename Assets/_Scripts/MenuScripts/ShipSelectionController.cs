using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using LostPolygon.DynamicWaterSystem;

public class ShipSelectionController : MonoBehaviour {

	public GameObject Ship;
	public GameObject PlayerObject;
	public Transform PlayerTransform;
	GameObject CurrentShip;
	public string ActiveShip = "Sloop";
	GameObject SpawningShip;
	GameObject CurrentWeapons;
	
	public GameObject _ShipSelectionMenuObj;
	public GameObject _WeaponSelectionMenuObj;

	public GameObject startButtonActivateObj;

	ShipMovingOffMenu shipMovingOffMenu;
	BuoyancyForce buoyancyForce;
	CharacterInventory characterInventory;

	public bool readyToStartGame = false;
	
	public void ShipButtons(string buttonName) {
		if (buttonName == "Sloop" && ActiveShip != buttonName) {
			MoveShipOff();
			StartCoroutine(KillShipCoroutine(buttonName));
		}
		if (buttonName == "Brig" && ActiveShip != buttonName) {
			MoveShipOff();
			StartCoroutine(KillShipCoroutine(buttonName));
		}
		if (buttonName == "YellowSloop") {

		}
		if (buttonName == "Next") {
			TriggerWeaponsMenu();
		}
		if (buttonName == "Cannon1") {
			DestroyCurrentWeapons();
			SpawnSelectedWeapons(buttonName);
		}
		if (buttonName == "Back") {
			TriggerShipsMenu();
		}
		if (buttonName == "Confirm") {
			foreach( Transform child in GameObject.Find("Player").transform.FindChild(ActiveShip +"/Nodes/LeftGuns").transform ){
				
				if(child.GetComponentInChildren<CannonFire>()){
					child.GetComponentInChildren<CannonFire>().doFire = true;
					//Debug.Log(child.name + " FIRED!!!");
				}
			}

			foreach( Transform child in GameObject.Find("Player").transform.FindChild(ActiveShip +"/Nodes/RightGuns").transform ){
				
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

	IEnumerator KillShipCoroutine(string newShip){
		yield return new WaitForSeconds (10f);
		SpawnNewShip(newShip);
		yield return new WaitForSeconds (1f);
		ResetShipLocation ();
		shipMovingOffMenu.enabled = false;
	}

	void SpawnNewShip(string newShip) {
		characterInventory = GameObject.Find ("Player").GetComponent<CharacterInventory> ();
		characterInventory.desiredShip = newShip;

		ActiveShip = newShip;
	}

	void ResetShipLocation() {
		PlayerTransform = GameObject.Find ("Player").GetComponent<Transform> ();
		PlayerTransform.position = new Vector3 (0f, 0f, -60f);
	}

	void DestroyCurrentWeapons() {
		foreach (Transform child in GameObject.Find("Player").transform.FindChild("Sloop/Nodes/LeftGuns").transform) {

		}

	}

	void SpawnSelectedWeapons(string newWeapons) {

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
