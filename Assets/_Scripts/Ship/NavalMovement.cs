using UnityEngine;
using System.Collections;

public class NavalMovement : MonoBehaviour {

	public bool isAlive = true;

	public enum Wind {
		left, right
	}
	public Wind wind;

	//Sails:

	GameObject mainCourseSail;
	GameObject mainTopSail;
	GameObject mainTopGallant;
	GameObject mainRoyalSail;

	GameObject aftSail;

	GameObject foreCourseSail;
	GameObject foreTopSail;
	GameObject foreTopGallant;
	GameObject foreRoyalSail;

	//FoldedSails:
	GameObject mainCourseSailFolded;
	GameObject mainTopSailFolded;
	GameObject mainTopGallantFolded;
	GameObject mainRoyalSailFolded;

	GameObject aftSailFolded;
	
	GameObject foreCourseSailFolded;
	GameObject foreTopSailFolded;
	GameObject foreTopGallantFolded;
	GameObject foreRoyalSailFolded;


	public enum SailSet {
		no,
		min,
		max
	}
	
	public SailSet	sailSet;

	public float sailNoMod 		= 0f;
	public float sailMinMod 	= 0.5f;
	public float sailMaxMod		= 1f;

	public float currSailSetMod;


	public Quaternion 	shipDir;
	public Quaternion	windDir;

	public float	windHdg;

	public float 	shipVel;
	public float 	windStrength;
	public float 	sailCharCoef; // Comparative to the area of the sails... bigger == more force.
	public float 	sailIntoWind; 
	public float	sailIntoWindModifier; //Depends on type of rigging, essentially how much into the wind can a ship sail...

	public float 	turningForce;
	public float	turningForceCoeff;

	public bool turnRight;
	public bool turnLeft;




	// Use this for initialization
	void Start () {

		sailSet = SailSet.no;
		sailCharCoef 		= 15f;
		turningForceCoeff 	= 6f;

		DetermineSailPresence();




	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if( isAlive ){
			windHdg = GameObject.FindGameObjectWithTag("WorldController").GetComponent<WindSim>().windDirection;
			LRwind();
			
			
			
			windStrength = GameObject.FindGameObjectWithTag("WorldController").GetComponent<WindSim>().windStrength;
			
			windDir = Quaternion.Euler( 0, GameObject.FindGameObjectWithTag("WorldController").GetComponent<WindSim>().windDirection, 0 );
			shipDir = transform.rotation;
			//Calculate abs value of quaternion dot prduct between boat and the wind.
			sailIntoWind = Mathf.Abs(Quaternion.Dot(shipDir, windDir));
			
			//print(Quaternion.Dot(shipDir, windDir));
			
			WindForceOnShip();
			
			
			
			if( turnRight == true ){
				
				TurnForceOnShipRight();
				
			}else if( turnLeft == true){
				
				TurnForceOnShipLeft();
				
			}


			DetermineSetSail();


		}

		//print (transform.GetComponent<Rigidbody>().velocity.magnitude +"     "+ transform.GetComponent<Rigidbody>().angularVelocity.magnitude);

			
	}

	//---------------------------------------------------------//



	void WindForceOnShip(){

		Vector3 vec = new Vector3( 0, 0, 1);

		float windEffect = windStrength * (sailCharCoef * 100000) * (sailIntoWind + 0.3f) * currSailSetMod ;

		if( transform.position.y < 10f){
			print (transform.forward);
			transform.GetComponent<Rigidbody>().AddForceAtPosition( (windEffect * new Vector3( transform.forward.x, 0, transform.forward.z) ) ,  transform.position /*+ Vector3.up*/ );

		}


		Wind windLR = wind;

		if( windLR == Wind.left){

			transform.GetComponent<Rigidbody>().AddRelativeTorque(  -0.10f * (1-sailIntoWind) * vec * windEffect );

		}else if( windLR == Wind.right){
			
			transform.GetComponent<Rigidbody>().AddRelativeTorque(  0.10f * (1-sailIntoWind)* vec * windEffect );

		}


	}

	//Turns the Ship.
	void TurnForceOnShipLeft(){

		//transform.FindChild("Sloop/Rudder").transform.localEulerAngles = new Vector3(0,45,0);

		float velocity = transform.GetComponent<Rigidbody>().velocity.magnitude;

		Vector3 vec = new Vector3(0, 1, 0);

		turningForce =  ( (2*velocity + 5)/(velocity + 1) ) * turningForceCoeff * 1000000;

		transform.GetComponent<Rigidbody>().AddRelativeTorque(  -2 * vec * turningForce );

	}
	void TurnForceOnShipRight(){

		//transform.FindChild("Sloop/Rudder").transform.localEulerAngles = new Vector3(0,-45,0);


		float velocity = transform.GetComponent<Rigidbody>().velocity.magnitude;

		Vector3 vec = new Vector3( 0, 1, 0);

		turningForce = ( (2*velocity + 5)/(velocity + 1) ) * (turningForceCoeff * 1000000);
		
		transform.GetComponent<Rigidbody>().AddRelativeTorque(   2 * vec * turningForce );

	}





	//Determining wind direction relative ot the ship:
	
	void LRwind(){
		
		float parentHdg = transform.rotation.eulerAngles.y;
		
		float parentHdgR 	= parentHdg * 2 * Mathf.PI / 360;
		float windHdgR 		= windHdg * 2 * Mathf.PI / 360;
		
		
		
		//FIRST QUADRANT
		if( Mathf.Cos(parentHdgR) > 0 && Mathf.Sin(parentHdgR) > 0 ){
			
			if(  Mathf.Sin(windHdgR) < 0  &&  ((windHdg+180)%360) > parentHdg  ){
				
				wind = Wind.right;
				//print (wind);
				
			}else if(  Mathf.Sin(windHdgR) > 0  &&  (windHdg) < parentHdg ){
				
				wind = Wind.right;
				//print (wind);
				
			}else {
				
				wind = Wind.left;
				//print (wind);
			}
			
			
			
			//SECOND QUADRANT
			
		}else if( Mathf.Cos(parentHdgR) < 0 && Mathf.Sin(parentHdgR) > 0 ){
			
			if( Mathf.Sin(windHdgR) < 0  &&  ((windHdg+180)%360) > parentHdg    ){
				
				wind = Wind.right;
				//print (wind);
				
			}else if(  Mathf.Sin(windHdgR) > 0  &&  (windHdg) < parentHdg ){
				
				wind = Wind.right;
				//print (wind);
				
			}else {
				
				wind = Wind.left;
				//print (wind);
			}
			
			
			
			
			
			//THIRD QUADRANT
			
		}else if( Mathf.Cos(parentHdgR) < 0 && Mathf.Sin(parentHdgR) < 0 ){
			
			if( Mathf.Sin(windHdgR) < 0  &&  windHdg > parentHdg    ){
				
				wind = Wind.left;
				//print (wind);
				
			}else if(  Mathf.Sin(windHdgR) > 0  &&  (windHdg) < ((parentHdg+180)%360) ){
				
				wind = Wind.left;
				//print (wind);
				
			}else {
				
				wind = Wind.right;
				//print (wind);
			}
			
			
			//FOURTH QUADRANT
			
		}else if( Mathf.Cos(parentHdgR) > 0 && Mathf.Sin(parentHdgR) < 0 ){
			
			if( Mathf.Sin(windHdgR) < 0  &&  windHdg > parentHdg    ){
				
				wind = Wind.left;
				//print (wind);
				
			}else if(  Mathf.Sin(windHdgR) > 0  &&  (windHdg) < ((parentHdg+180)%360) ){
				
				wind = Wind.left;
				//print (wind);
				
			}else {
				
				wind = Wind.right;
				//print (wind);
			}
			
		}
	}


	void DetermineSailPresence(){
		
		//Aft Mast
		if( transform.FindChild( string.Format("{0}/AftMast1/AftSail", transform.GetComponent<CharacterInventory>().characterInventory.Find(x=>x.itemType == Item.ItemType.Ship).itemPrefabName ) ) ){
			aftSail 	= transform.FindChild( string.Format("{0}/AftMast1/AftSail", transform.GetComponent<CharacterInventory>().characterInventory.Find(x=>x.itemType == Item.ItemType.Ship).itemPrefabName)).gameObject;
		}
		if( transform.FindChild( string.Format("{0}/AftMast1/AftSailFolded", transform.GetComponent<CharacterInventory>().characterInventory.Find(x=>x.itemType == Item.ItemType.Ship).itemName ) ) ){
			aftSailFolded 	= transform.FindChild(string.Format("{0}/AftMast1/AftSailFolded", transform.GetComponent<CharacterInventory>().characterInventory.Find(x=>x.itemType == Item.ItemType.Ship).itemName)).gameObject;
		}

		//Main mast:
		if( transform.FindChild( string.Format("{0}/MainMast1/CourseSailMast/CourseSail", transform.GetComponent<CharacterInventory>().characterInventory.Find(x=>x.itemType == Item.ItemType.Ship).itemName ))){
			mainCourseSail 	= transform.FindChild(string.Format("{0}/MainMast1/CourseSailMast/CourseSail", transform.GetComponent<CharacterInventory>().characterInventory.Find(x=>x.itemType == Item.ItemType.Ship).itemName )).gameObject;
		}
		if( transform.FindChild( string.Format("{0}/MainMast1/CourseSailMast/CourseSailFolded", transform.GetComponent<CharacterInventory>().characterInventory.Find(x=>x.itemType == Item.ItemType.Ship).itemName ) ) ){
			mainCourseSailFolded 	= transform.FindChild(string.Format("{0}/MainMast1/CourseSailMast/CourseSailFolded", transform.GetComponent<CharacterInventory>().characterInventory.Find(x=>x.itemType == Item.ItemType.Ship).itemName)).gameObject;
		}
		if( transform.FindChild( string.Format("{0}/MainMast1/TopSailMast/TopSail", transform.GetComponent<CharacterInventory>().characterInventory.Find(x=>x.itemType == Item.ItemType.Ship).itemName ) ) ){
			mainTopSail 	= transform.FindChild(string.Format("{0}/MainMast1/TopSailMast/TopSail", transform.GetComponent<CharacterInventory>().characterInventory.Find(x=>x.itemType == Item.ItemType.Ship).itemName)).gameObject;
		}
		if( transform.FindChild( string.Format("{0}/MainMast1/TopSailMast/TopSailFolded", transform.GetComponent<CharacterInventory>().characterInventory.Find(x=>x.itemType == Item.ItemType.Ship).itemName ) ) ){
			mainTopSailFolded 	= transform.FindChild(string.Format("{0}/MainMast1/TopSailMast/TopSailFolded", transform.GetComponent<CharacterInventory>().characterInventory.Find(x=>x.itemType == Item.ItemType.Ship).itemName)).gameObject;
		}
		if( transform.FindChild( string.Format("{0}/MainMast1/TopGallantMast/TopGallant", transform.GetComponent<CharacterInventory>().characterInventory.Find(x=>x.itemType == Item.ItemType.Ship).itemName ) ) ){
			mainTopGallant 	= transform.FindChild(string.Format("{0}/MainMast1/TopGallantMast/TopGallant", transform.GetComponent<CharacterInventory>().characterInventory.Find(x=>x.itemType == Item.ItemType.Ship).itemName)).gameObject;
		}
		if( transform.FindChild( string.Format("{0}/MainMast1/TopGallantMast/TopGallant", transform.GetComponent<CharacterInventory>().characterInventory.Find(x=>x.itemType == Item.ItemType.Ship).itemName ) ) ){
			mainTopGallantFolded 	= transform.FindChild(string.Format("{0}/MainMast1/TopGallantMast/TopGallantFolded", transform.GetComponent<CharacterInventory>().characterInventory.Find(x=>x.itemType == Item.ItemType.Ship).itemName)).gameObject;
		}
		if( transform.FindChild( string.Format("{0}/MainMast1/RoyalSailMast/RoyalSail", transform.GetComponent<CharacterInventory>().characterInventory.Find(x=>x.itemType == Item.ItemType.Ship).itemName ))){
			mainRoyalSail	= transform.FindChild(string.Format("{0}/MainMast1/RoyalSailMast/RoyalSail", transform.GetComponent<CharacterInventory>().characterInventory.Find(x=>x.itemType == Item.ItemType.Ship).itemName)).gameObject;
		}
		if( transform.FindChild( string.Format("{0}/MainMast1/RoyalSailMast/RoyalSailFolded", transform.GetComponent<CharacterInventory>().characterInventory.Find(x=>x.itemType == Item.ItemType.Ship).itemName ) ) ){
			mainRoyalSailFolded	= transform.FindChild(string.Format("{0}/MainMast1/RoyalSailMast/RoyalSailFolded", transform.GetComponent<CharacterInventory>().characterInventory.Find(x=>x.itemType == Item.ItemType.Ship).itemName)).gameObject;
		}


		//Foremast:
		if( transform.FindChild( string.Format("{0}/ForeMast1/CourseSailMast/CourseSail", transform.GetComponent<CharacterInventory>().characterInventory.Find(x=>x.itemType == Item.ItemType.Ship).itemName ))){
			foreCourseSail 	= transform.FindChild(string.Format("{0}/ForeMast1/CourseSailMast/CourseSail", transform.GetComponent<CharacterInventory>().characterInventory.Find(x=>x.itemType == Item.ItemType.Ship).itemName)).gameObject;
		}
		if( transform.FindChild( string.Format("{0}/ForeMast1/CourseSailMast/CourseSailFolded", transform.GetComponent<CharacterInventory>().characterInventory.Find(x=>x.itemType == Item.ItemType.Ship).itemName )) ){
			foreCourseSailFolded 	= transform.FindChild(string.Format("{0}/ForeMast1/CourseSailMast/CourseSailFolded", transform.GetComponent<CharacterInventory>().characterInventory.Find(x=>x.itemType == Item.ItemType.Ship).itemName)).gameObject;
		}
		if( transform.FindChild( string.Format("{0}/ForeMast1/TopSailMast/TopSail", transform.GetComponent<CharacterInventory>().characterInventory.Find(x=>x.itemType == Item.ItemType.Ship).itemName ) ) ){
			foreTopSail 	= transform.FindChild(string.Format("{0}/ForeMast1/TopSailMast/TopSail", transform.GetComponent<CharacterInventory>().characterInventory.Find(x=>x.itemType == Item.ItemType.Ship).itemName)).gameObject;
		}
		if( transform.FindChild( string.Format("{0}/ForeMast1/TopSailMast/TopSailFolded", transform.GetComponent<CharacterInventory>().characterInventory.Find(x=>x.itemType == Item.ItemType.Ship).itemName ) ) ){
			foreTopSailFolded 	= transform.FindChild(string.Format("{0}/ForeMast1/TopSailMast/TopSailFolded", transform.GetComponent<CharacterInventory>().characterInventory.Find(x=>x.itemType == Item.ItemType.Ship).itemName)).gameObject;
		}
		if( transform.FindChild( string.Format("{0}/ForeMast1/TopGallantMast/TopGallant", transform.GetComponent<CharacterInventory>().characterInventory.Find(x=>x.itemType == Item.ItemType.Ship).itemName ) ) ){
			foreTopGallant 	= transform.FindChild(string.Format("{0}/ForeMast1/TopGallantMast/TopGallant", transform.GetComponent<CharacterInventory>().characterInventory.Find(x=>x.itemType == Item.ItemType.Ship).itemName)).gameObject;
		}
		if( transform.FindChild( string.Format("{0}/ForeMast1/TopGallantMast/TopGallant", transform.GetComponent<CharacterInventory>().characterInventory.Find(x=>x.itemType == Item.ItemType.Ship).itemName ) ) ){
			foreTopGallantFolded 	= transform.FindChild(string.Format("{0}/ForeMast1/TopGallantMast/TopGallantFolded", transform.GetComponent<CharacterInventory>().characterInventory.Find(x=>x.itemType == Item.ItemType.Ship).itemName)).gameObject;
		}
		if( transform.FindChild( string.Format("{0}/ForeMast1/RoyalSailMast/RoyalSail", transform.GetComponent<CharacterInventory>().characterInventory.Find(x=>x.itemType == Item.ItemType.Ship).itemName ) ) ){
			foreRoyalSail	= transform.FindChild(string.Format("{0}/MForeMast1/RoyalSailMast/RoyalSail", transform.GetComponent<CharacterInventory>().characterInventory.Find(x=>x.itemType == Item.ItemType.Ship).itemName)).gameObject;
		}
		if( transform.FindChild( string.Format("{0}ForeMast1/RoyalSailMast/RoyalSailFolded", transform.GetComponent<CharacterInventory>().characterInventory.Find(x=>x.itemType == Item.ItemType.Ship).itemName ) ) ){
			foreRoyalSailFolded	= transform.FindChild(string.Format("{0}/ForeMast1/RoyalSailMast/RoyalSailFolded", transform.GetComponent<CharacterInventory>().characterInventory.Find(x=>x.itemType == Item.ItemType.Ship).itemName)).gameObject;
		}






	}
	void DetermineSetSail(){

		if( sailSet == SailSet.no){
			
			currSailSetMod = sailNoMod;

			//Sails
			if(mainCourseSail != null){
				mainCourseSail.SetActive(false);
			}
			if(mainTopSail != null){
				mainTopSail.SetActive(false);
			}
			if( mainTopGallant != null){
				mainTopGallant.SetActive(false);
			}
			if( mainRoyalSail != null){
				mainRoyalSail.SetActive(false);
			}


			if(aftSail != null){
				aftSail.SetActive(false);
			}


			if(foreCourseSail != null){
				foreCourseSail.SetActive(false);
			}
			if(foreTopSail != null){
				foreTopSail.SetActive(false);
			}
			if( foreTopGallant != null){
				foreTopGallant.SetActive(false);
			}
			if( foreRoyalSail != null){
				foreRoyalSail.SetActive(false);
			}



			//FoldedSails
			if(mainCourseSailFolded != null){
				mainCourseSailFolded.SetActive(true);
			}
			if(mainTopSailFolded != null){
				mainTopSailFolded.SetActive(true);
			}
			if( mainTopGallantFolded != null){
				mainTopGallantFolded.SetActive(true);
			}
			if( mainRoyalSailFolded != null){
				mainRoyalSailFolded.SetActive(true);
			}


			if(aftSailFolded != null){
				aftSailFolded.SetActive(true);
			}

			
			if(foreCourseSailFolded != null){
				foreCourseSailFolded.SetActive(true);
			}
			if(foreTopSailFolded != null){
				foreTopSailFolded.SetActive(true);
			}
			if( foreTopGallantFolded != null){
				foreTopGallantFolded.SetActive(true);
			}
			if( foreRoyalSailFolded != null){
				foreRoyalSailFolded.SetActive(true);
			}
			
		
		}else if (sailSet == SailSet.min){
			
			currSailSetMod = sailMinMod;
			
			//Sails
			if(mainCourseSail != null){
				mainCourseSail.SetActive(false);
			}
			if(mainTopSail != null){
				mainTopSail.SetActive(false);
			}
			if( mainTopGallant != null){
				mainTopGallant.SetActive(true);
			}
			if( mainRoyalSail != null){
				mainRoyalSail.SetActive(true);
			}
			

			if(aftSail != null){
				aftSail.SetActive(true);
			}


			if(foreCourseSail != null){
				foreCourseSail.SetActive(false);
			}
			if(foreTopSail != null){
				foreTopSail.SetActive(false);
			}
			if( foreTopGallant != null){
				foreTopGallant.SetActive(true);
			}
			if( foreRoyalSail != null){
				foreRoyalSail.SetActive(true);
			}
			
			
			
			//FoldedSails
			if(mainCourseSailFolded != null){
				mainCourseSailFolded.SetActive(true);
			}
			if(mainTopSailFolded != null){
				mainTopSailFolded.SetActive(true);
			}
			if( mainTopGallantFolded != null){
				mainTopGallantFolded.SetActive(false);
			}
			if( mainRoyalSailFolded != null){
				mainRoyalSailFolded.SetActive(false);
			}
			

			if(aftSailFolded != null){
				aftSailFolded.SetActive(false);
			}


			if(foreCourseSailFolded != null){
				foreCourseSailFolded.SetActive(true);
			}
			if(foreTopSailFolded != null){
				foreTopSailFolded.SetActive(true);
			}
			if( foreTopGallantFolded != null){
				foreTopGallantFolded.SetActive(false);
			}
			if( foreRoyalSailFolded != null){
				foreRoyalSailFolded.SetActive(false);
			}
			
		
		
		
		
		}else if(sailSet == SailSet.max){

			currSailSetMod = sailMaxMod;

			//Sails
			if(mainCourseSail != null){
				mainCourseSail.SetActive(true);
			}
			if(mainTopSail != null){
				mainTopSail.SetActive(true);
			}
			if( mainTopGallant != null){
				mainTopGallant.SetActive(true);
			}
			if( mainRoyalSail != null){
				mainRoyalSail.SetActive(true);
			}
			

			if(aftSail != null){
				aftSail.SetActive(true);
			}


			if(foreCourseSail != null){
				foreCourseSail.SetActive(true);
			}
			if(foreTopSail != null){
				foreTopSail.SetActive(true);
			}
			if( foreTopGallant != null){
				foreTopGallant.SetActive(true);
			}
			if( foreRoyalSail != null){
				foreRoyalSail.SetActive(true);
			}
			
			
			
			//FoldedSails
			if(mainCourseSailFolded != null){
				mainCourseSailFolded.SetActive(false);
			}
			if(mainTopSailFolded != null){
				mainTopSailFolded.SetActive(false);
			}
			if( mainTopGallantFolded != null){
				mainTopGallantFolded.SetActive(false);
			}
			if( mainRoyalSailFolded != null){
				mainRoyalSailFolded.SetActive(false);
			}
			

			if(aftSailFolded != null){
				aftSailFolded.SetActive(false);
			}


			if(foreCourseSailFolded != null){
				foreCourseSailFolded.SetActive(false);
			}
			if(foreTopSailFolded != null){
				foreTopSailFolded.SetActive(false);
			}
			if( foreTopGallantFolded != null){
				foreTopGallantFolded.SetActive(false);
			}
			if( foreRoyalSailFolded != null){
				foreRoyalSailFolded.SetActive(false);
			}

		}

	}




}
