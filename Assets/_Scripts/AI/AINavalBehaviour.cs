using UnityEngine;
using System.Collections;

public class AINavalBehaviour : MonoBehaviour {

	public enum BehaviourMode{

		Peaceful, Combat

	}

	BehaviourMode behaviourMode;

	public GameObject target;

	bool hasEnemy;

	public Vector3 moveTo;
	public Vector3 targetLocation;

	public 	float rangeToTarget;
	private float maxWeaponsRange;
	private float minWeaponsRange;

	// Use this for initialization
	void Start () {
	
		behaviourMode = BehaviourMode.Peaceful;

	}
	
	// Update is called once per frame
	void Update () {

		SeekEnemy();

		if( hasEnemy ){

			targetLocation 	= target.transform.position;
			rangeToTarget 	= (transform.position - targetLocation).magnitude;

		}


		if( behaviourMode == BehaviourMode.Peaceful){


		}else if( behaviourMode == BehaviourMode.Combat){



		}



	}






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



	}


	void MoveTowardsEnemy(){



	}

	void CombatSeamanship(){



	}


	void AimAndFireCannons(){

		float aimForm	= (( rangeToTarget *9.81f)/(100f*100f)) ; 
		float aimThetaR	= 0.5f * Mathf.Asin( aimForm ) ;
		float aimTheta	= aimThetaR * (360/(2*Mathf.PI));
		print (aimTheta);
		
		foreach( Transform child in transform.FindChild("Sloop/Nodes/LeftGuns") ){
			
			if( child.GetComponentInChildren<CannonFire>() ){
				
				child.GetComponentInChildren<CannonFire>().transform.eulerAngles = new Vector3(  -aimTheta , child.GetComponentInChildren<CannonFire>().transform.eulerAngles.y , 0);
				
			}
			
		}foreach( Transform child in transform.FindChild("Sloop/Nodes/RightGuns") ){
			
			if( child.GetComponentInChildren<CannonFire>() ){
				
				child.GetComponentInChildren<CannonFire>().transform.eulerAngles = new Vector3(  -aimTheta , child.GetComponentInChildren<CannonFire>().transform.eulerAngles.y , 0);
				
			}
			
		}

		if( rangeToTarget <= maxWeaponsRange  ){
			
			foreach( Transform child in transform.FindChild("Sloop/Nodes/LeftGuns") ){
				
				if(child.GetComponentInChildren<CannonFire>()){
					child.GetComponentInChildren<CannonFire>().doFire = true;
					//Debug.Log(child.name + " FIRED!!!");
				}
				
				
			}
			
			
		}else if( rangeToTarget <= maxWeaponsRange   ){
			
			foreach( Transform child in transform.FindChild("Sloop/Nodes/RightGuns") ){
				
				if(child.GetComponentInChildren<CannonFire>()){
					child.GetComponentInChildren<CannonFire>().doFire = true;
					//Debug.Log(child.name + " FIRED!!!");
				}
				
				
			}
			
			
			
		}



	}


}
