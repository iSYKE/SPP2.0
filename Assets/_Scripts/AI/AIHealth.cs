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

			transform.GetComponent<BuoyancyForce>().Density = 1000f;

		}

	}


}
