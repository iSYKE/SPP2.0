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
		Chase, Engage, Withdraw, Surrender, Travel
	}

	public CurrentAction currentAction;


	// Situational Awareness Vars
	//--------------------------------
	public enum TargetLeftRight{
		Left, Right
	}

	public TargetLeftRight targetLR;

	bool hasEnemy;

	public GameObject target;

	public Vector3 	targetLocation;

	
	public Vector3 	heading;
	public Vector3 	headingToTarget;
	
	public Vector3 	targetVelocity;
	public float 	rightVelDot;
	public float 	forwardVelDot;
	//Movement Vars
	//--------------------------------
	public  float relativeDot;
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

		maxEngageRange  = 200f;
		minEngageRange  = 100f;

		maxWeaponsRange = 400f;
		minWeaponsRange =  0f;

		behaviourMode = BehaviourMode.Peaceful;

	}
	
	//------------------------------------
	void FixedUpdate () {


		zRot = transform.eulerAngles.z;
		yRot = transform.eulerAngles.y;


		relativeDot = Vector3.Dot( (transform.position - targetLocation).normalized, transform.right );
		minusRelativeDot = -relativeDot;


		windDirection = GameObject.FindGameObjectWithTag("WorldController").GetComponent<WindSim>().windDirection;



		SeekEnemy();



		if( hasEnemy ){

			targetLocation 	= target.transform.position;
			rangeToTarget 	= (transform.position - targetLocation).magnitude;


			//Feed into CombatSeamanship
			targetVelocity 	= target.GetComponent<Rigidbody>().velocity;
			rightVelDot 	= Vector3.Dot( transform.right.normalized, targetVelocity.normalized);
			forwardVelDot	= Vector3.Dot( transform.forward.normalized, targetVelocity.normalized);

		}else{

			targetLocation = transform.position;
			rangeToTarget  = 0f;

		}



		if( relativeDot > 0 ){
			targetLR = TargetLeftRight.Left; 
		}else if( relativeDot < 0){
			targetLR = TargetLeftRight.Right; 
		}


		DetermineMorale();
		DetermineAction();
		Action();

	}

	//------------------------------------


	void SeekEnemy(){


		if( GameObject.Find("Player") ){

			hasEnemy = true;

			behaviourMode = BehaviourMode.Combat;

			target = GameObject.Find("Player");

		}else{

			hasEnemy = false;
			
			behaviourMode = BehaviourMode.Peaceful;
			
			target = null;

		}
	}


//--------------------------------------------------------
	void DetermineMorale(){

		if ( behaviourMode == BehaviourMode.Combat ){

			if ( transform.GetComponent<AIHealth>().aiHealth > 50f ){
				morale = Morale.Steady;
			}else if ( transform.GetComponent<AIHealth>().aiHealth < 50f ){
				morale = Morale.Wavering;
			}else if ( transform.GetComponent<AIHealth>().aiHealth < 20f ){
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

			Surrender();
			
		}else if( currentAction == CurrentAction.Travel){
			
			Travel();

		}


	}
//--------------------------------------------------------

	void Chase(){

		SailTo ( targetLocation, relativeDot );
		SailSpeed( NavalMovement.SailSet.max);

	}

	void Engage(){

		CombatSeamanship();
		SailSpeed( NavalMovement.SailSet.min);
	
	}

	void Withdraw(){
	
		SailToHeading( windDirection );
		SailSpeed( NavalMovement.SailSet.max);
	
	}

	void Surrender(){

		SailSpeed(NavalMovement.SailSet.no);

	}

	void Travel(){

	}


//--------------------------------------------------------

	void SailSpeed( NavalMovement.SailSet sail ){
		
		transform.GetComponent<NavalMovement>().sailSet = sail; 
		
	}


	void SailTo( Vector3 tarLoc, float relD ){

		moveTo = tarLoc;
		
		if( moveTo != null ){
			
			if(  relD > 0.02 ){
				
				transform.GetComponent<NavalMovement>().turnLeft  = true;
				transform.GetComponent<NavalMovement>().turnRight = false;
				
			}else if( relD < -0.02 ){
				
				transform.GetComponent<NavalMovement>().turnLeft  = false;
				transform.GetComponent<NavalMovement>().turnRight = true;
				
			}else{
				
				transform.GetComponent<NavalMovement>().turnLeft  = false;
				transform.GetComponent<NavalMovement>().turnRight = false;
				
			}
			
			
		}
		
	}

	void SailToHeading( float hdg ){

		//Vector3 heading = new Vector3(0, hdg, 0);

		if( transform.eulerAngles.y  > hdg-1 ){
			
			transform.GetComponent<NavalMovement>().turnLeft  = true;
			transform.GetComponent<NavalMovement>().turnRight = false;
			
		}else if( transform.eulerAngles.y  < hdg+1 ){
			
			transform.GetComponent<NavalMovement>().turnLeft  = false;
			transform.GetComponent<NavalMovement>().turnRight = true;
			
		}else{
			
			transform.GetComponent<NavalMovement>().turnLeft  = false;
			transform.GetComponent<NavalMovement>().turnRight = false;
			
		}


	}


	void CombatSeamanship(){



		if(  relativeDot < 0.98f && relativeDot > 0 ){

			if( forwardVelDot < 0 ){

				transform.GetComponent<NavalMovement>().turnLeft  = true;
				transform.GetComponent<NavalMovement>().turnRight = false;
			
			}else if(forwardVelDot > 0){

				transform.GetComponent<NavalMovement>().turnLeft  = false;
				transform.GetComponent<NavalMovement>().turnRight = true;
			
			}

		}else if( relativeDot > -0.98f && relativeDot < 0 ){

			if( forwardVelDot < 0 ){

				transform.GetComponent<NavalMovement>().turnLeft  = false;
				transform.GetComponent<NavalMovement>().turnRight = true;
			
			}else if(forwardVelDot > 0){

				transform.GetComponent<NavalMovement>().turnLeft  = true;
				transform.GetComponent<NavalMovement>().turnRight = false;
			
			}

		}else{
			
			transform.GetComponent<NavalMovement>().turnLeft  = false;
			transform.GetComponent<NavalMovement>().turnRight = false;
			
		}


		AimAndFireCannons();

	}
	

	//-----------------------------------------------------
	void AimAndFireCannons(){

		float aimForm	= (( rangeToTarget *9.81f)/(100f*100f)) ; 
		float aimThetaR	= 0.5f * Mathf.Asin( aimForm ) ;
		float aimTheta	= aimThetaR * (360/(2*Mathf.PI));
		//print (aimTheta);
		
		foreach( Transform child in transform.FindChild("Sloop/Nodes/LeftGuns") ){
			
			if( child.GetComponentInChildren<CannonFire>() ){
				
				child.GetComponentInChildren<CannonFire>().transform.localEulerAngles = new Vector3(  -aimTheta , child.GetComponentInChildren<CannonFire>().transform.localEulerAngles.y , 0);
				
			}
			
		}foreach( Transform child in transform.FindChild("Sloop/Nodes/RightGuns") ){
			
			if( child.GetComponentInChildren<CannonFire>() ){
				
				child.GetComponentInChildren<CannonFire>().transform.localEulerAngles = new Vector3(  -aimTheta , child.GetComponentInChildren<CannonFire>().transform.localEulerAngles.y , 0);
				
			}
			
		}


		if( rangeToTarget <= maxWeaponsRange  &&  relativeDot > 0.95f && ( zRot > 355f || zRot < 5f) ){
			
			foreach( Transform child in transform.FindChild("Sloop/Nodes/LeftGuns") ){
				
				if(child.GetComponentInChildren<CannonFire>()){
					child.GetComponentInChildren<CannonFire>().doFire = true;
					//Debug.Log(child.name + " FIRED!!!");
				}
				
				
			}
			
			
		}else if( rangeToTarget <= maxWeaponsRange && relativeDot < -0.95f && ( zRot > 355f || zRot < 5f) ){
			
			foreach( Transform child in transform.FindChild("Sloop/Nodes/RightGuns") ){
				
				if(child.GetComponentInChildren<CannonFire>()){
					child.GetComponentInChildren<CannonFire>().doFire = true;
					//Debug.Log(child.name + " FIRED!!!");
				}
				
				
			}
	
		}
	}





}
