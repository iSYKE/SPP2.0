using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public float portAimRange = 100f;
	public float starBoardAimRange = 100f;
	public float aimRange = 100f;
	public float maxRange = 400f;
	public float minRange = 0f;

	public Text portNumberText;
	public Text starboardNumberText;

	void OnLevelWasLoaded(int level) {
		if (level == 1) {
			portNumberText = GameObject.Find("Canvas/ShipFiringUI/PortNumberText").GetComponent<Text>();
			portNumberText.text = portAimRange + "m";
			starboardNumberText = GameObject.Find("Canvas/ShipFiringUI/StarboardNumberText").GetComponent<Text>();
			starboardNumberText.text = starBoardAimRange + "m";
		}

	}

	void Start(){
	

	}



	
	void Update()
	{
		ChangeSailSetting();
		TurnShip();
		FireCannons();


	}









	void ChangeSailSetting(){

		NavalMovement.SailSet currSet = transform.GetComponent<NavalMovement>().sailSet;


		if( Input.GetKeyDown(KeyCode.W) ){

			if ( currSet == NavalMovement.SailSet.no ){

				transform.GetComponent<NavalMovement>().sailSet = NavalMovement.SailSet.min;

			}else if ( currSet == NavalMovement.SailSet.min ){
				
				transform.GetComponent<NavalMovement>().sailSet = NavalMovement.SailSet.max;
				
			}

		}else if( Input.GetKeyDown(KeyCode.S) ){

			if ( currSet == NavalMovement.SailSet.max ){
				
				transform.GetComponent<NavalMovement>().sailSet = NavalMovement.SailSet.min;
				
			}else if ( currSet == NavalMovement.SailSet.min ){
				
				transform.GetComponent<NavalMovement>().sailSet = NavalMovement.SailSet.no;
				
			}
			
		}


	}

	void TurnShip(){

		if( Input.GetAxis("Horizontal") > 0 ){
			transform.FindChild(string.Format("{0}/Rudder", transform.GetComponent<CharacterInventory>().characterCurrentShip.shipName )).transform.localEulerAngles = new Vector3(0,-45*Input.GetAxis("Horizontal")*Input.GetAxis("Horizontal"),0);
			transform.GetComponent<NavalMovement>().turnRight = true;
			transform.GetComponent<NavalMovement>().turnLeft = false;
			
			
		}else if(Input.GetAxis("Horizontal") < 0 ){
			transform.FindChild(string.Format("{0}/Rudder", transform.GetComponent<CharacterInventory>().characterCurrentShip.shipName )).transform.localEulerAngles = new Vector3(0,-45*Input.GetAxis("Horizontal")*-Input.GetAxis("Horizontal"),0);
			transform.GetComponent<NavalMovement>().turnRight = false;
			transform.GetComponent<NavalMovement>().turnLeft = true;
			
		}else{
			transform.FindChild(string.Format("{0}/Rudder", transform.GetComponent<CharacterInventory>().characterCurrentShip.shipName ) ).transform.localEulerAngles = new Vector3(0,0,0);
			transform.GetComponent<NavalMovement>().turnLeft = false;
			transform.GetComponent<NavalMovement>().turnRight = false;
			
		}
	}

	public void SetCannonAngle(string gunSide) {
		if (gunSide == "PortUp") {
			if(portAimRange < 375f) {
				portAimRange += 25f;
			} else { portAimRange = 400f; }
		
		}
		if (gunSide == "PortDown") {
			if(portAimRange > 75f) {
				portAimRange -= 25f;
			} else { portAimRange = 50f; }
		}
		if (gunSide == "StarboardUp") {
			if(starBoardAimRange < 375f) {
				starBoardAimRange += 25f;
			} else { starBoardAimRange = 400f; }
		}
		if (gunSide == "StarboardDown") {
			if(starBoardAimRange > 75f) {
				starBoardAimRange -= 25f;
			}	else { starBoardAimRange = 50f; }
		}

		portNumberText.text = portAimRange + "m";
		starboardNumberText.text = starBoardAimRange + "m";

		RangeCannonsLeft();
		RangeCannonsRight();

	}

	void RangeCannonsLeft(){

		print ("ranging!");

		float aimTheta = 0;

		if( portAimRange >= 100f){

			float aimForm	= (( portAimRange *9.81f)/(100f*100f)) ; 
			float aimThetaR	= 0.5f * Mathf.Asin( aimForm ) ;

			aimTheta	= aimThetaR * (360/(2*Mathf.PI));
		
		}else if( portAimRange == 75f){

			aimTheta = -5f;

		}else if( portAimRange == 50f ){

			aimTheta = -10f ;
		}

		print (aimTheta);
		print ( transform.GetComponent<ShipLoadout>().leftGuns.Count );


		/*
		 * Don't know what's causing this bug when cannons don't range up/down after loading in from main menu scene.
		 * 
		 * Update1:
		 * Used ship loadout instead of a foreach, which fixed the null reference error, but still cannons refuse to actually move now.
		 * All works fine when in Scene1 itself... but going from main menu game refuses to get past the calcualting theta part and
		 * acknowledge the existance of the guns left/right lists... count remains at 0 despite both lists having gun objects in them.
		 * Clearing/Reseting a list breaks it??
		 * 
		 * Update2: 
		 * No, when in scene1 and change ship the clearing of a list as ListGuns is called returns the correct values for list.count.
		 * Meanwhile after laoding from menu into scene1 list.count returns zero even after changing a ship...
		 * Definitely a load level issue with Lists... hmm, hard reset/remove&add all scripts on player perhaps?
		 * 
		 * Update3:
		 * Went from menu to scene1, paused and removed shiploadout component, attached new one with emtpy list. Called changeship. 
		 * Lists still returns zero count despite the lists filling up with gun game objects as expected... ffs!
		 * List.ForEach is a bitch, eats frames. Going back to the foreach.(fps drop fixed)
		 * 
		 * Update4:
		 * List still returns count as zero.
		 * NulleRef is back... error in finding transform.FindChild(string.Format("{0}/Nodes/LeftGuns", transform.GetComponent<CharacterInventory>().characterCurrentShip.shipPrefabName )) ???
		 * Why the fuck would charCurrShip.shipPrefabName be missing? Or Nodes? Or Left/RightGuns?! Everything is as should be in the inspector... Cast Transform is fine... wtf?
		 * Tried using "Sloop" instead of string formatting same thing. Null reference despite the obejcts being there. I give up.
.		 */

	/*
		transform.GetComponent<ShipLoadout>().leftGuns.ForEach( delegate(GameObject obj) {
			print ("Cannon exists1");	
			if(	obj.transform.GetComponentInChildren<CannonFire>()  ){
				print ("Cannon exists2");	
				obj.transform.GetComponentInChildren<CannonFire>().transform.localEulerAngles = new Vector3(  -aimTheta , transform.GetComponentInChildren<CannonFire>().transform.localEulerAngles.y , 0);
						
				}
			
			});
		}
		*/

	///*


		foreach( Transform child in transform.FindChild(string.Format("{0}/Nodes/LeftGuns", transform.GetComponent<CharacterInventory>().characterCurrentShip.shipPrefabName )) ){
			
			if( child.GetComponentInChildren<CannonFire>() ){

				child.GetComponentInChildren<CannonFire>().transform.localEulerAngles = new Vector3(  -aimTheta , child.GetComponentInChildren<CannonFire>().transform.localEulerAngles.y , 0);

			}
		}
	}

	//*/

	void RangeCannonsRight(){
		

		float aimTheta = 0;

		if( starBoardAimRange >= 100f){
			
			float aimForm	= (( starBoardAimRange *9.81f)/(100f*100f)) ; 
			float aimThetaR	= 0.5f * Mathf.Asin( aimForm ) ;
			
			aimTheta	= aimThetaR * (360/(2*Mathf.PI));
			
		}else if( starBoardAimRange == 75f){
			
			aimTheta = -5f;
			
		}else if( starBoardAimRange == 50f ){
			
			aimTheta = -10f ;
		}

	/*
		transform.GetComponent<ShipLoadout>().rightGuns.ForEach( delegate(GameObject obj) {
			if(	obj.transform.GetComponentInChildren<CannonFire>()  ){
					
				obj.transform.GetComponentInChildren<CannonFire>().transform.localEulerAngles = new Vector3(  -aimTheta , transform.GetComponentInChildren<CannonFire>().transform.localEulerAngles.y , 0);

			}

		});
	*/

		///*	
		foreach( Transform child in transform.FindChild(string.Format("{0}/Nodes/RightGuns", transform.GetComponent<CharacterInventory>().characterCurrentShip.shipPrefabName )) ){
			
			if( child.GetComponentInChildren<CannonFire>() ){
				
				child.GetComponentInChildren<CannonFire>().transform.localEulerAngles = new Vector3(  -aimTheta , child.GetComponentInChildren<CannonFire>().transform.localEulerAngles.y , 0);
				
			}	
		}
		//*/
	}
	

	void FireCannons(){

		//Go through each child in Left and Right gun nodes and invoke firing command in the objects possessing the CannonFire cmponent, ie. cannons
		if( Input.GetKeyDown(KeyCode.Q) ){
			
			foreach( Transform child in transform.FindChild(string.Format("{0}/Nodes/LeftGuns", transform.GetComponent<CharacterInventory>().characterCurrentShip.shipPrefabName )) ){
				
				if(child.GetComponentInChildren<CannonFire>()){

					child.GetComponentInChildren<CannonFire>().doFire = true;
					//Debug.Log(child.name + " FIRED!!!");
				}
				
				
			}
			
			
		}else if( Input.GetKeyDown(KeyCode.E) ){
			
			foreach( Transform child in transform.FindChild(string.Format("{0}/Nodes/RightGuns", transform.GetComponent<CharacterInventory>().characterCurrentShip.shipPrefabName )) ){
				
				if(child.GetComponentInChildren<CannonFire>()){

					child.GetComponentInChildren<CannonFire>().doFire = true;
					//Debug.Log(child.name + " FIRED!!!");
				}
				
				
			}
			
			
			
		}

	}




}
