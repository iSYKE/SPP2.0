using UnityEngine;
using System.Collections;

public class AINavalBehaviour : MonoBehaviour {

	// AI Behaviour States
	//--------------------------------
	public enum BehaviourMode{

		Peaceful, Combat

	}

	public BehaviourMode behaviourMode;


	public enum Morale{
		Surrender, Wavering, Steady
	}

	public Morale morale;

	// AI Decision Making
	//--------------------------------

	public enum CurrentAction{
		Chase, Engage, Withdraw, Surrender, Travel, Wait
	}

	public CurrentAction currentAction;


	// Situational Awareness Vars
	//--------------------------------
	public enum TargetLRAB{
		Left, Right, Ahead, Behind
	}

	public TargetLRAB targetLRAB;

	//-----------------------------

	public enum TargetTALR{
		None, Towards, Away, Left, Right
	}
	public TargetTALR targetTALR;

	//-------------------------------

	bool hasEnemy;

	public GameObject target;

	public Vector3 	targetLocation;
	public Vector3 	destination;
	
	public Vector3 	heading;
	public Vector3 	headingToTarget;
	
	public Vector3 	targetVelocity;
	public float 	rightVelDot;
	public float 	forwardVelDot;

	//Movement Vars
	//--------------------------------
	public  float relativeDotRight;
	public 	float relativeDotForward;

	public 	float minusRelativeDot;
	private float zRot;
	private float yRot;
	private float windDirection;

	public Vector3 moveTo;




	//Engaging Vars
	//--------------------------------
	public 	float rangeToTarget;

	private float maxEngageRange;
	private float minEngageRange;

	private float maxWeaponsRange;
	private float minWeaponsRange;



	//------------------------------------
	void Start () {

		maxEngageRange  = 300f;
		minEngageRange  = 50f;

		maxWeaponsRange = 400f;
		minWeaponsRange =  0f;

		behaviourMode = BehaviourMode.Peaceful;

	}
	
	//------------------------------------
	void FixedUpdate () {

		zRot = transform.eulerAngles.z;
		yRot = transform.eulerAngles.y;


		relativeDotRight = Vector3.Dot( (transform.position - targetLocation).normalized, transform.right   );
		relativeDotForward = Vector3.Dot( (transform.position - targetLocation).normalized, transform.forward );

		//minusRelativeDot = -relativeDotRight;


		windDirection = GameObject.FindGameObjectWithTag("WorldController").GetComponent<WindSim>().windDirection;



		SeekCurrentFocus();



		if( hasEnemy ){

			targetLocation 	= target.transform.position;
			rangeToTarget 	= (transform.position - targetLocation).magnitude;


			//Feed into CombatSeamanship
			targetVelocity 	= target.GetComponent<Rigidbody>().velocity;
			rightVelDot 	= Vector3.Dot( transform.right.normalized, targetVelocity.normalized);
			forwardVelDot	= Vector3.Dot( transform.forward.normalized, targetVelocity.normalized);

		}else{

			targetLocation = destination;
			rangeToTarget 	= (transform.position - targetLocation).magnitude;

		}


		DetermineLRAB();
		DetermineTALR();
		DetermineMorale();
		DetermineAction();
		Action();

	}

	//------------------------------------

	// USES DOT PRODUCT OF POSITION VECTORS TO DETERMINE POSITION OF TARGET RELATIVE TO THE AI
	void DetermineLRAB(){

		if( relativeDotForward < -0.5f && relativeDotRight > -0.5f && relativeDotRight < 0.5f ){
			
			targetLRAB = TargetLRAB.Ahead; 
			
		}else if( relativeDotForward > 0.5f && relativeDotRight > -0.5f && relativeDotRight < 0.5f ){
			
			targetLRAB = TargetLRAB.Behind; 
			
		}else if( relativeDotRight < -0.5f && relativeDotForward > -0.5f && relativeDotForward < 0.5f ){
			
			targetLRAB = TargetLRAB.Right;
			
		}else if( relativeDotRight > 0.5f && relativeDotForward > -0.5f && relativeDotForward < 0.5f ){
			
			targetLRAB = TargetLRAB.Left;
		}

	}

	//METHODS USES DOT PRODUCT OF VELOCITY VECTORS RELATIVE TO THE POSITION OF THE TARGET TO SEE THE MOVEMENT IN RELATION TO THE AI
	void DetermineTALR(){


		if ( !hasEnemy ){
			
			targetTALR = TargetTALR.None;
		
		}else if( forwardVelDot > 0.5f && rightVelDot > -0.5f && rightVelDot < 0.5f ){

			if( targetLRAB == TargetLRAB.Ahead){

				targetTALR = TargetTALR.Away;

			}else if(targetLRAB == TargetLRAB.Behind){

				targetTALR = TargetTALR.Towards;
			
			}else if(targetLRAB == TargetLRAB.Left){
				
				targetTALR = TargetTALR.Right;
			
			}else if(targetLRAB == TargetLRAB.Right){
				
				targetTALR = TargetTALR.Left;
			
			}
			
		}else if( forwardVelDot < -0.5f && rightVelDot > -0.5f && forwardVelDot < 0.5f ){
			
			if( targetLRAB == TargetLRAB.Ahead){
				
				targetTALR = TargetTALR.Towards;
				
			}else if(targetLRAB == TargetLRAB.Behind){
				
				targetTALR = TargetTALR.Away;
				
			}else if(targetLRAB == TargetLRAB.Left){
				
				targetTALR = TargetTALR.Left;
				
			}else if(targetLRAB == TargetLRAB.Right){
				
				targetTALR = TargetTALR.Right;
				
			}
			
		}else if( rightVelDot > 0.5f && forwardVelDot > -0.5f && forwardVelDot < 0.5f ){
			
			if( targetLRAB == TargetLRAB.Ahead){
				
				targetTALR = TargetTALR.Right;
				
			}else if(targetLRAB == TargetLRAB.Behind){
				
				targetTALR = TargetTALR.Right;
				
			}else if(targetLRAB == TargetLRAB.Left){
				
				targetTALR = TargetTALR.Towards;
				
			}else if(targetLRAB == TargetLRAB.Right){
				
				targetTALR = TargetTALR.Away;
				
			} 
		
		}else if( rightVelDot < -0.5f && forwardVelDot > -0.5f && forwardVelDot < 0.5f ){
			
			if( targetLRAB == TargetLRAB.Ahead){
				
				targetTALR = TargetTALR.Left;
				
			}else if(targetLRAB == TargetLRAB.Behind){
				
				targetTALR = TargetTALR.Left;
				
			}else if(targetLRAB == TargetLRAB.Left){
				
				targetTALR = TargetTALR.Away;
				
			}else if(targetLRAB == TargetLRAB.Right){
				
				targetTALR = TargetTALR.Towards;
				
			}
			
		}
		
	}

	// AI SEEKS TARGETS TO FOCUSE ON IE DESTINATION, ENEMY, ETC.
	void SeekCurrentFocus(){


		if( GameObject.Find("Player") ){

			hasEnemy = true;

			behaviourMode = BehaviourMode.Combat;

			target = GameObject.Find("Player");

		}else if (destination != null ){

			hasEnemy = false;
			
			behaviourMode = BehaviourMode.Peaceful;
			
			//target = gameObject;

		}else{

			behaviourMode = BehaviourMode.Peaceful;
			//target = gameObject;
		}
	
	}


//--------------------------------------------------------

	void DetermineMorale(){
		float maxHealth = transform.GetComponent<CharacterShipStats>().maxHullHealth;
		float health = transform.GetComponent<CharacterShipStats>().hullHealth;
		if ( behaviourMode == BehaviourMode.Combat ){

			if ( health > 0.25 * maxHealth ){
				morale = Morale.Steady;
			}else if (  health <= 0.20 * maxHealth && health > 0.10 * maxHealth ){
				morale = Morale.Wavering;
			}else if (  health <= 0.10 * maxHealth ){
				morale = Morale.Surrender;
			}
		
		}else{

			morale = Morale.Steady;

		}
	
	}


	void DetermineAction(){

		if( behaviourMode == BehaviourMode.Combat ){
			if ( morale == Morale.Steady){

				if ( rangeToTarget > maxEngageRange ){
					
					currentAction = CurrentAction.Chase;
					
				}else if( rangeToTarget < maxEngageRange){
					
					currentAction = CurrentAction.Engage;
					
				}


			}else if(morale == Morale.Wavering){
				
				currentAction = CurrentAction.Withdraw;
			
			}else if(morale == Morale.Surrender){

				currentAction = CurrentAction.Surrender;

			}
		
		}else if( behaviourMode == BehaviourMode.Peaceful){


			if( rangeToTarget > 5f ){

				currentAction = CurrentAction.Travel;

			}else{
				 
				currentAction = CurrentAction.Wait;

			}


		}

	}


	void Action(){

		if( currentAction == CurrentAction.Chase){

			Chase();

		}else if( currentAction == CurrentAction.Engage){

			Engage();

		}else if( currentAction == CurrentAction.Withdraw){
			
			Withdraw();

		}else if( currentAction == CurrentAction.Surrender){

			behaviourMode = BehaviourMode.Peaceful;
			Surrender();
			
		}else if( currentAction == CurrentAction.Travel){
			
			Travel();

		}else if( currentAction == CurrentAction.Wait){
			
			Wait();
			
		}


	}

//--------------------------------------------------------

	void Chase(){

		SailTo ( targetLocation);
		SailSpeed( NavalMovement.SailSet.max);

	}

	void Engage(){

		DetermineTALR(); //MIGHT NEED TO BE MOVED

		CombatMovement();
		//SailSpeed( NavalMovement.SailSet.min);
	
	}

	void Withdraw(){
	
		SailToHeading( windDirection );
		SailSpeed( NavalMovement.SailSet.max);
	
	}

	void Surrender(){

		SailSpeed( NavalMovement.SailSet.no );

	}

	void Travel(){

		SailTo( targetLocation  );

		if ( rangeToTarget > 200f ){
			SailSpeed( NavalMovement.SailSet.max);
		}else{
			SailSpeed( NavalMovement.SailSet.min);
		}


	}

	void Wait(){

		SailSpeed(NavalMovement.SailSet.no);
		StopTurn();
		
	}
	
//--------------------------------------------------------

	void SailSpeed( NavalMovement.SailSet sail ){
		
		transform.GetComponent<NavalMovement>().sailSet = sail; 
		
	}


	void SailTo( Vector3 tarLoc ){

		moveTo = tarLoc;
		
		if( moveTo != transform.position ){
			
			if(  relativeDotRight > 0.02 && relativeDotForward > -0.98 ){
				
				LeftTurn();
				
			}else if( relativeDotRight < -0.02 && relativeDotForward > -0.98 ){
				
				RightTurn();
				
			}else{
				
				StopTurn();
				
			}
			
			
		}
		
	}

	void SailToHeading( float hdg ){

		//Vector3 heading = new Vector3(0, hdg, 0);

		if( transform.eulerAngles.y  > hdg-1 ){
			
			LeftTurn();
			
		}else if( transform.eulerAngles.y  < hdg+1 ){
			
			RightTurn();
			
		}else{
			
			StopTurn();
			
		}


	}


	void CombatMovement(){


		if( rangeToTarget > minEngageRange){

			if( targetTALR == TargetTALR.Away){

				if(rangeToTarget > 200 ){

					SailTo( targetLocation );
					SailSpeed( NavalMovement.SailSet.max);

				}else if (targetLRAB == TargetLRAB.Left){

					SailSpeed( NavalMovement.SailSet.min);

					if( relativeDotForward < -0.01f ){
						
						RightTurn();
						
					}else if( relativeDotForward > 0.01f ){
						
						LeftTurn();
						
					}
				}else if( targetLRAB == TargetLRAB.Right ){

					SailSpeed( NavalMovement.SailSet.min);

						if( relativeDotForward < -0.01f ){
							
							LeftTurn();

							
						}else if( relativeDotForward > 0.01f ){
							
							RightTurn();
						
						}

				}else{

					SailSpeed( NavalMovement.SailSet.min);

					SailTo( targetLocation );
				}


			}else if( targetTALR == TargetTALR.Towards){

				if( targetLRAB == TargetLRAB.Ahead || targetLRAB == TargetLRAB.Behind ){

					SailSpeed( NavalMovement.SailSet.min);

					if( relativeDotForward < -0.01f ){
					
						RightTurn();
					
					}else if( relativeDotForward > 0.01f ){
					
						LeftTurn();

					}
				
				}else if( targetLRAB == TargetLRAB.Right ){

					SailSpeed( NavalMovement.SailSet.min);

					if( relativeDotForward < -0.01f ){

						LeftTurn();

					}else if( relativeDotForward > 0.01f ){
						
						RightTurn();
						
					}

				}else if( targetLRAB == TargetLRAB.Left ){

					SailSpeed( NavalMovement.SailSet.min);

					if( relativeDotForward < -0.01f ){
						
						RightTurn();
						
					}else if( relativeDotForward > 0.01f ){
						
						LeftTurn();
						
					}
				
				}

			}else if( targetTALR == TargetTALR.Left){

				if( targetLRAB == TargetLRAB.Ahead){

					SailSpeed ( NavalMovement.SailSet.min );

					if( relativeDotRight < -0.01f ){
						
						LeftTurn();
						
					}else if( relativeDotRight > 0.01f ){
						
						RightTurn();
						
					}

				}else if( targetLRAB == TargetLRAB.Behind){

					SailSpeed ( NavalMovement.SailSet.min );

					if( relativeDotForward > 0.01f ){
						
						LeftTurn();
						
					}else if( relativeDotForward < -0.01f ){
						
						RightTurn();
						
					}

				}else if( targetLRAB == TargetLRAB.Left){

					SailSpeed ( target.transform.GetComponent<NavalMovement>().sailSet );

					if( relativeDotForward > 0.01f ){
						
						LeftTurn();
						
					}else if( relativeDotForward < -0.01f ){
						
						RightTurn();
						
					}
					
				}else if( targetLRAB == TargetLRAB.Right){

					SailSpeed ( target.transform.GetComponent<NavalMovement>().sailSet );

					if( relativeDotForward > 0.01f ){
						
						RightTurn();
						
					}else if( relativeDotForward < -0.01f ){
						
						LeftTurn();
						
					}
				}

			}else if( targetTALR == TargetTALR.Right ){

				if( targetLRAB == TargetLRAB.Ahead){

					SailSpeed ( NavalMovement.SailSet.min );

					if( relativeDotRight < -0.01f ){
						
						RightTurn();
						
					}else if( relativeDotRight > 0.01f ){
						
						LeftTurn();
						
					}
					
				}else if( targetLRAB == TargetLRAB.Behind){

					SailSpeed ( NavalMovement.SailSet.min );

					if( relativeDotForward > 0.01f ){
						
						RightTurn();
						
					}else if( relativeDotForward < -0.01f ){
						
						LeftTurn();
						
					}
					
				}else if( targetLRAB == TargetLRAB.Left){

					SailSpeed ( target.transform.GetComponent<NavalMovement>().sailSet );

					if( relativeDotForward > 0.01f ){
						
						LeftTurn();
						
					}else if( relativeDotForward < -0.01f ){
						
						RightTurn();
						
					}
					
				}else if( targetLRAB == TargetLRAB.Right){

					SailSpeed ( target.transform.GetComponent<NavalMovement>().sailSet );

					if( relativeDotForward > 0.01f ){
						
						RightTurn();
						
					}else if( relativeDotForward < -0.01f ){
						
						LeftTurn();
						
					}
				}

			}else if (targetTALR == TargetTALR.None){

				if( rangeToTarget < maxEngageRange ){

					if( relativeDotForward > 0.01f ){
						
						RightTurn();
						
					}else if( relativeDotForward < -0.01f ){
						
						LeftTurn();
						
					}

				}else if( rangeToTarget > maxEngageRange){
					SailTo( targetLocation );
					SailSpeed( NavalMovement.SailSet.max);

				}

			}
		
		}else{
			// ESCAPE IF TOO CLOSE? Right now it turns.
			SailSpeed( NavalMovement.SailSet.no);

			if( relativeDotForward > 0.01f ){
				
				RightTurn();
				
			}else if( relativeDotForward < -0.01f ){
				
				LeftTurn();
			}


		}

		AimAndFireCannons();

	}
	

	//-----------------------------------------------------
	
		void AimAndFireCannons(){

		float aimForm	= (( rangeToTarget *9.81f)/(100f*100f)) ; 
		float aimThetaR	= 0.5f * Mathf.Asin( aimForm ) ;
		float aimTheta	= aimThetaR * (360/(2*Mathf.PI));
		//print (aimTheta);
		
		foreach( Transform child in transform.FindChild(string.Format("{0}/Nodes/LeftGuns", transform.GetComponent<CharacterInventory>().characterCurrentShip.shipPrefabName) ) ){
			
			if( child.GetComponentInChildren<CannonFire>() ){
				
				child.GetComponentInChildren<CannonFire>().transform.localEulerAngles = new Vector3(  -aimTheta , child.GetComponentInChildren<CannonFire>().transform.localEulerAngles.y , 0);
				
			}
			
		}foreach( Transform child in transform.FindChild(string.Format("{0}/Nodes/RightGuns", transform.GetComponent<CharacterInventory>().characterCurrentShip.shipPrefabName ) ) ){
			
			if( child.GetComponentInChildren<CannonFire>() ){
				
				child.GetComponentInChildren<CannonFire>().transform.localEulerAngles = new Vector3(  -aimTheta , child.GetComponentInChildren<CannonFire>().transform.localEulerAngles.y , 0);
				
			}
			
		}


		if( rangeToTarget <= maxWeaponsRange  &&  relativeDotRight > 0.95f && ( zRot > 355f || zRot < 5f) ){
			
			foreach( Transform child in transform.FindChild(string.Format("{0}/Nodes/LeftGuns", transform.GetComponent<CharacterInventory>().characterCurrentShip.shipPrefabName ) ) ){
				
				if(child.GetComponentInChildren<CannonFire>()){
					child.GetComponentInChildren<CannonFire>().doFire = true;
					//Debug.Log(child.name + " FIRED!!!");
				}
				
				
			}
			
			
		}else if( rangeToTarget <= maxWeaponsRange && relativeDotRight < -0.95f && ( zRot > 355f || zRot < 5f) ){
			
			foreach( Transform child in transform.FindChild(string.Format("{0}/Nodes/RightGuns", transform.GetComponent<CharacterInventory>().characterCurrentShip.shipPrefabName ) ) ){
				
				if(child.GetComponentInChildren<CannonFire>()){
					child.GetComponentInChildren<CannonFire>().doFire = true;
					//Debug.Log(child.name + " FIRED!!!");
				}
				
				
			}
	
		}
	}

	
void LeftTurn(){
	transform.GetComponent<NavalMovement>().turnLeft  = true;
	transform.GetComponent<NavalMovement>().turnRight = false;
}
void RightTurn(){
	transform.GetComponent<NavalMovement>().turnLeft  = false;
	transform.GetComponent<NavalMovement>().turnRight = true;
}
void StopTurn(){
	transform.GetComponent<NavalMovement>().turnLeft  = false;
	transform.GetComponent<NavalMovement>().turnRight = false;
}

	
	
	
	

}
