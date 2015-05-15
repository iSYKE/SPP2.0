using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public float portAimRange = 100f;
	public float starBoardAimRange = 100f;
	public float aimRange = 100f;
	public float maxRange = 400f;
	public float minRange =   -100f;

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

		foreach( Transform child in transform.FindChild(string.Format("{0}/Nodes/LeftGuns", transform.GetComponent<CharacterInventory>().characterCurrentShip.shipPrefabName )) ){
			
			if( child.GetComponentInChildren<CannonFire>() ){

				child.GetComponentInChildren<CannonFire>().transform.localEulerAngles = new Vector3(  -aimTheta , child.GetComponentInChildren<CannonFire>().transform.localEulerAngles.y , 0);

			}
		}
	}

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
			
		foreach( Transform child in transform.FindChild(string.Format("{0}/Nodes/RightGuns", transform.GetComponent<CharacterInventory>().characterCurrentShip.shipPrefabName )) ){
			
			if( child.GetComponentInChildren<CannonFire>() ){
				
				child.GetComponentInChildren<CannonFire>().transform.localEulerAngles = new Vector3(  -aimTheta , child.GetComponentInChildren<CannonFire>().transform.localEulerAngles.y , 0);
				
			}	
		}
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
