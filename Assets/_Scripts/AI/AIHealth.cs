using UnityEngine;
using System.Collections;
using LostPolygon.DynamicWaterSystem;

public class AIHealth : MonoBehaviour {


	public float aiHealth = 200f;


	void FixedUpdate () {
	
		Death();

	}

	void Death(){

		if( aiHealth <= 0 ){
			print (transform.name +" IS DEAD");
			transform.GetComponent<BuoyancyForce>().Density = 1300f;
			transform.GetComponent<NavalMovement>().isAlive = false;

			Destroy (gameObject, 45f);

		}

	}


}
