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

	void FixedUpdate(){

		ChangeShip();

	}





	public void FillCharCurrShip(){

		if( characterCurrentShip == null || characterCurrentShip.shipName != desiredShip ){

			characterCurrentShip = GameObject.FindGameObjectWithTag("WorldController").GetComponent<ShipList>().gameShips.Find( x => x.shipName == desiredShip);

		}

	}

	void ChangeShip(){

		if( characterShip == null || characterShip.name != characterCurrentShip.shipPrefabName){

			//Spawn entity with new ship.
			GameObject charShip;
			charShip = Instantiate( Resources.Load( "Ships/"+ characterCurrentShip.shipPrefabName ) , transform.position+ new Vector3(0,characterCurrentShip.shipSpawnYoffset,0) , transform.rotation) as GameObject;
			charShip.name = characterCurrentShip.shipPrefabName;
			charShip.transform.parent = transform.GetComponent<Transform>();
			characterShip = charShip;

			//Reset key ship parameters.
			charShip.transform.parent.GetComponent<NavalMovement>().DetermineSailPresence();
			transform.GetComponent<Rigidbody>().mass = characterCurrentShip.shipMass;
			transform.GetComponent<NavalMovement>().sailIntoWindModifier 	= characterCurrentShip.shipSailIntoWindModifier;
			transform.GetComponent<NavalMovement>().turningForceCoeff 		= characterCurrentShip.shipTurnCoefficient;
			transform.GetComponent<NavalMovement>().sailCharCoef			= characterCurrentShip.shipSailCoefficient;

			//Update values in Ship stats.
			transform.GetComponent<CharacterShipStats>().UpdateToNewShip();
			//Add buoyancy.
			transform.gameObject.AddComponent<BuoyancyForce>();


		}else if( characterCurrentShip.shipName != desiredShip){

			//Destroy old ship.
			Destroy(characterShip);
			Destroy( transform.GetComponent<BuoyancyForce>() );


			//Spawn new ship.
			characterCurrentShip = GameObject.FindWithTag("WorldController").GetComponent<ShipList>().gameShips.Find(x => x.shipName == desiredShip );

			GameObject charShip;

			charShip = Instantiate( Resources.Load( "Ships/"+ characterCurrentShip.shipPrefabName ) , transform.position+ new Vector3(0,characterCurrentShip.shipSpawnYoffset,0) , transform.rotation) as GameObject;
			charShip.name = characterCurrentShip.shipPrefabName;
			charShip.transform.parent = transform.GetComponent<Transform>();
			characterShip = charShip;

			//Reset key ship parameters.
			charShip.transform.parent.GetComponent<NavalMovement>().DetermineSailPresence();
			transform.GetComponent<Rigidbody>().mass = characterCurrentShip.shipMass;
			transform.GetComponent<NavalMovement>().sailIntoWindModifier 	= characterCurrentShip.shipSailIntoWindModifier;
			transform.GetComponent<NavalMovement>().turningForceCoeff 		= characterCurrentShip.shipTurnCoefficient;
			transform.GetComponent<NavalMovement>().sailCharCoef			= characterCurrentShip.shipSailCoefficient;

			//Update values in Ship stats.
			transform.GetComponent<CharacterShipStats>().UpdateToNewShip();

			//Reset Buoyancy.
			StartCoroutine(DelayBuoyancy(0.5f));
		
		}

	}



	IEnumerator DelayBuoyancy(float time) {
		yield return new WaitForSeconds(time);
	
		transform.gameObject.AddComponent<BuoyancyForce>();
		transform.GetComponent<BuoyancyForce>().Density = characterCurrentShip.shipDensity;

	}







}
