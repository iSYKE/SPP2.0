using UnityEngine;
using System.Collections;

public class CharacterShipStats : MonoBehaviour {

	public float maxHullHealth;
	public float hullHealth;

	public float maxSailHealth;
	public float sailHealth;

	public int maxCrew;
	public int minCrew;
	public int crew;

	public bool isSinking  = false;
	public bool isSailable = false;
	public bool isCrewed   = false;

	//-----------------------------------------------


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		HullUpdate();
		SailUpdate();
		CrewUpdate();

	}


	//-----------------------------------------------

	public void UpdateToNewShip(){

		maxHullHealth = transform.GetComponent<CharacterInventory>().characterCurrentShip.shipMaxHull;
		maxSailHealth = transform.GetComponent<CharacterInventory>().characterCurrentShip.shipMaxSail;

		hullHealth = maxHullHealth;
		sailHealth = maxSailHealth;

		
		maxCrew		  = transform.GetComponent<CharacterInventory>().characterCurrentShip.shipMaxCrew;
		minCrew		  = maxCrew / 10;

		if( crew >= minCrew){
			crew = crew;
		}else{
			crew = minCrew;
		}
	
	}

	//------------------------------------------------

	void HullUpdate(){


		if( hullHealth <= 0){
			isSinking = true;
		}


	}
	void SailUpdate(){
		if( sailHealth <= 0){
			isSailable = true;
		}


	}
	void CrewUpdate(){
		if( crew < minCrew ){
			isCrewed = false;
		}


	}






}
