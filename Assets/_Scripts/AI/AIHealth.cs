using UnityEngine;
using System.Collections;
using LostPolygon.DynamicWaterSystem;

public class AIHealth : MonoBehaviour {

	public float aiMaxHealth;
	public float aiHealth = 200f;

	public bool isRepaired = true;
	public bool isSmoking = false;


	void Start(){
		aiMaxHealth = 500f;

		isRepaired = true;
		isSmoking = false;
	}

	void FixedUpdate () {
	
		Death();
		IsDamaged();
		ShowDamage();


	}

	void Death(){

		if( aiHealth <= 0 ){

			if( transform.GetComponent<NavalMovement>().isAlive == true ){

				Quaternion rotUp = Quaternion.Euler( transform.eulerAngles.x - 90f, transform.eulerAngles.y, transform.eulerAngles.z );
				
				GameObject shipDamageSmoke;
				shipDamageSmoke = Instantiate( Resources.Load("VFX/ShipDeathPlume1") , transform.position , rotUp ) as GameObject;
				shipDamageSmoke.name = Resources.Load("VFX/ShipDeathPlume1").name;
				shipDamageSmoke.transform.SetParent( transform );

			}


			print (transform.name +" IS DEAD");
			transform.GetComponent<BuoyancyForce>().Density = 1300f;
			transform.GetComponent<NavalMovement>().isAlive = false;

			Destroy (gameObject, 45f);

			


		}

	}

	void IsDamaged(){

		if ( aiHealth < aiMaxHealth){

			isRepaired = false;
		}

	}

	void ShowDamage(){
		
		if ( !isSmoking && !isRepaired && aiHealth < 0.5f * aiMaxHealth){

			Quaternion rotUp = Quaternion.Euler( transform.eulerAngles.x - 90f, transform.eulerAngles.y, transform.eulerAngles.z );

			GameObject shipDamageSmoke;
			shipDamageSmoke = Instantiate( Resources.Load("VFX/ShipDamageSmoke") , transform.position , rotUp ) as GameObject;
			shipDamageSmoke.name = Resources.Load("VFX/ShipDamageSmoke").name;
			shipDamageSmoke.transform.SetParent( transform );
			
			isSmoking = true;
			
		}
	}
		


	void QuickRepair(){

		aiHealth = aiHealth + (0.2f * aiMaxHealth);

		isRepaired = true;
		isSmoking = false;


	}




}
