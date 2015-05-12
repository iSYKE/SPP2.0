using UnityEngine;
using System.Collections;

public class SmokeWhileUnrepaired : MonoBehaviour {


	
	void Update() {

		Smoke();
			
	}


	void Smoke(){

		if( transform.GetComponent<PlayerHealth>() ){

			if( transform.GetComponent<PlayerHealth>().isRepaired == true ){
				transform.GetComponent<ParticleSystem>().loop = false;
				Destroy( gameObject, 10f );

			} 

		}else if( transform.GetComponent<AIHealth>() ){
			
			if( transform.GetComponent<AIHealth>().isRepaired == true ){
				transform.GetComponent<ParticleSystem>().loop = false;
				Destroy( gameObject, 10f );
				
			}
		}else if( transform.position.y < -5f ){
			transform.GetComponent<ParticleSystem>().loop = false;
			Destroy( gameObject, 10f );
		}
	
	
	}


}
