using UnityEngine;
using System.Collections;
using LostPolygon.DynamicWaterSystem;


public class CharacterShipStats : MonoBehaviour {

	public float maxHullHealth = 100f;
	public float hullHealth = 100f;

	public float maxSailHealth;
	public float sailHealth;

	public int maxCrew;
	public int minCrew;
	public int crew;

	public int gunMaxCountLeft;
	public int gunCurrentCountLeft;
	public int gunCountUnloadedLeft;

	public int gunMaxCountRight;
	public int gunCurrentCountRight;
	public int gunCountUnloadedRight;

	public bool isSinking  = false;
	public bool isSailable = false;
	public bool isCrewed   = false;

	public bool isRepaired = true;
	public bool isOnFire = false;

	public bool isUpdate = false;


	//-----------------------------------------------


	// Use this for initialization
	void Start () {
		isUpdate = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(isUpdate){

			HullUpdate();
			SailUpdate();
			CrewUpdate();

			ShowDamage();
			Death();

		}
	
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
	
		gunMaxCountLeft  =  transform.GetComponent<CharacterInventory>().characterCurrentShip.shipMaxGunsLeft;
		gunMaxCountRight =  transform.GetComponent<CharacterInventory>().characterCurrentShip.shipMaxGunsRight;

		//Returns guns, must be readded
		int gunCount = gunCurrentCountLeft+gunCurrentCountRight;
		while( gunCount > 0 ){
			transform.GetComponent<CharacterShipInventory>().characterShipInventory.Add( 
			                   GameObject.FindGameObjectWithTag("WorldController").GetComponent<ItemList>().gameItems.Find(x=>x.itemPrefabName == "Cannon1") ) ;
			gunCount-- ;

		}
		gunCurrentCountLeft  = 0;
		gunCurrentCountRight = 0;
	}

	//------------------------------------------------

	void HullUpdate(){


		if( hullHealth < 0){
			isSinking = true;
		}


	}
	void SailUpdate(){
		if( sailHealth < 0){
			isSailable = true;
		}


	}
	void CrewUpdate(){
		if( crew < minCrew ){
			isCrewed = false;
		}


	}



	void Death(){

		if( isSinking ){

			transform.GetComponent<NavalMovement>().isAlive = false;
			transform.GetComponent<BuoyancyForce>().Density = 1300f;
			Destroy( gameObject, 45f );

		}


	}


	void ShowDamage(){

		if( hullHealth < 0.5f*maxHullHealth && isRepaired == false && !isOnFire){

			isOnFire = true;

			GameObject fire;
			fire = Instantiate( Resources.Load("VFX/ShipDamageSmoke"), transform.position, Quaternion.Euler(transform.eulerAngles.x-90, transform.eulerAngles.y, transform.eulerAngles.z ) ) as GameObject;
			fire.name = Resources.Load("VFX/ShipDamageSmoke").name;
			fire.transform.parent = transform;
		}

	}





}
