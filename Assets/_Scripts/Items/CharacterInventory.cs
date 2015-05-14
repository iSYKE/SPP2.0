using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LostPolygon.DynamicWaterSystem;

public class CharacterInventory : MonoBehaviour {



	public List<Item> characterItems = new List<Item>();


	public string desiredShip;
	public Ship characterCurrentShip;
	public GameObject characterShip;



	void Start(){

		FillCharCurrShip();

	}

	void Update(){

		ChangeShip();

	}





	void FillCharCurrShip(){

		if( characterCurrentShip == null || characterCurrentShip.shipName != desiredShip ){

			characterCurrentShip = GameObject.FindGameObjectWithTag("WorldController").GetComponent<ShipList>().gameShips.Find( x => x.shipName == desiredShip);

		}

	}

	void ChangeShip(){

		if( characterShip == null || characterShip.name != characterCurrentShip.shipPrefabName ){

			GameObject charShip;
			charShip = Instantiate( Resources.Load( "Ships/"+ characterCurrentShip.shipPrefabName ) , transform.position+ new Vector3(0,characterCurrentShip.shipSpawnYoffset,0) , transform.rotation) as GameObject;
			charShip.name = characterCurrentShip.shipPrefabName;
			charShip.transform.parent = transform.GetComponent<Transform>();

			characterShip = charShip;

			BuoyancyForce buoyancyForce = charShip.transform.parent.GetComponent<BuoyancyForce> ();

			buoyancyForce.ResetShip();

			charShip.transform.parent.GetComponent<NavalMovement>().DetermineSailPresence();


		}




	}









}
