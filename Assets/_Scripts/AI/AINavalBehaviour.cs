using UnityEngine;
using System.Collections;

public class AINavalBehaviour : MonoBehaviour {

	//--------------------------------
	public enum BehaviourMode{

		Peaceful, Combat

	}

	public BehaviourMode behaviourMode;

	//--------------------------------

	public enum Morale{
		Surrender, Wavering, Steady
	}

	public Morale morale;

	//--------------------------------


	// Situational Awareness Vars
	bool hasEnemy;

	public GameObject target;

	public Vector3 targetLocation;
	//Movement Vars


	public Vector3 moveTo;



	//Firing Vars
	public 	float rangeToTarget;
	private float maxWeaponsRange;
	private float minWeaponsRange;



	//------------------------------------
	void Start () {
	
		maxWeaponsRange = 400f;

		behaviourMode = BehaviourMode.Peaceful;

	}
	
	//------------------------------------
	void Update () {
		print ( Vector3.Dot( (transform.position - targetLocation).normalized, transform.right ) );
		SeekEnemy();

		if( hasEnemy ){

			targetLocation 	= target.transform.position;
			rangeToTarget 	= (transform.position - targetLocation).magnitude;

		}


		if( behaviourMode == BehaviourMode.Peaceful){


		}else if( behaviourMode == BehaviourMode.Combat){

			AimAndFireCannons();

		}



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


	void SailTo(){

		if( behaviourMode == BehaviourMode.Combat && targetLocation != null ){
			moveTo = targetLocation;
		}else{
			moveTo = Vector3.zero;
		}

		if( moveTo != null ){

			Vector3 headingVec = transform.position - moveTo;
			//DO STUFF HERE...

			if( transform.forward. != heading

		}



	}


	void MoveTowardsEnemy(){

		if( rangeToTarget > maxWeaponsRange){



		}

	}

	void CombatSeamanship(){



	}


	void AimAndFireCannons(){

		float aimForm	= (( rangeToTarget *9.81f)/(100f*100f)) ; 
		float aimThetaR	= 0.5f * Mathf.Asin( aimForm ) ;
		float aimTheta	= aimThetaR * (360/(2*Mathf.PI));
		//print (aimTheta);
		
		foreach( Transform child in transform.FindChild("Sloop/Nodes/LeftGuns") ){
			
			if( child.GetComponentInChildren<CannonFire>() ){
				
				child.GetComponentInChildren<CannonFire>().transform.eulerAngles = new Vector3(  -aimTheta , child.GetComponentInChildren<CannonFire>().transform.eulerAngles.y , 0);
				
			}
			
		}foreach( Transform child in transform.FindChild("Sloop/Nodes/RightGuns") ){
			
			if( child.GetComponentInChildren<CannonFire>() ){
				
				child.GetComponentInChildren<CannonFire>().transform.eulerAngles = new Vector3(  -aimTheta , child.GetComponentInChildren<CannonFire>().transform.eulerAngles.y , 0);
				
			}
			
		}

		if( rangeToTarget <= maxWeaponsRange  &&  Vector3.Dot( (transform.position - targetLocation).normalized, transform.right ) > 0.91f  ){
			
			foreach( Transform child in transform.FindChild("Sloop/Nodes/LeftGuns") ){
				
				if(child.GetComponentInChildren<CannonFire>()){
					child.GetComponentInChildren<CannonFire>().doFire = true;
					//Debug.Log(child.name + " FIRED!!!");
				}
				
				
			}
			
			
		}else if( rangeToTarget <= maxWeaponsRange && Vector3.Dot( (transform.position - targetLocation).normalized, transform.right ) < -0.91f ){
			
			foreach( Transform child in transform.FindChild("Sloop/Nodes/RightGuns") ){
				
				if(child.GetComponentInChildren<CannonFire>()){
					child.GetComponentInChildren<CannonFire>().doFire = true;
					//Debug.Log(child.name + " FIRED!!!");
				}
				
				
			}
			
			
			
		}



	}


}
