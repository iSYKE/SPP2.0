using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	int currentWave = 0;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


		if( GameObject.Find("AI_Sloop") || GameObject.Find("AI_Brig") ){
			return;

		}else{

			GameObject.Find("Player").GetComponent<CharacterShipStats>().hullHealth = GameObject.Find("Player").GetComponent<CharacterShipStats>().maxHullHealth;
			GameObject.Find("Player").GetComponent<CharacterShipStats>().isRepaired = true;

			SpawnAI();

		}

	
	}




	void SpawnAI(){

		if( currentWave == 0 ){

			GameObject enemy1;
			enemy1 = Instantiate( Resources.Load( "AI_Sloop"), new Vector3( transform.position.x - 500, 3f, transform.position.z - 400 ), transform.rotation) as GameObject;
			enemy1.name = Resources.Load( "AI_Sloop" ).name;


		}else if(  currentWave > 0 && (currentWave%2) == 0){

			GameObject enemy1;
			enemy1 = Instantiate( Resources.Load( "AI_Brig"), new Vector3( transform.position.x - 500, 3f, transform.position.z - 400 ), transform.rotation ) as GameObject;
			enemy1.name = Resources.Load( "AI_Brig" ).name;

		}else if( currentWave > 0 && (currentWave%2) == 1){
			
			GameObject enemy1;
			enemy1 = Instantiate( Resources.Load( "AI_Sloop"), new Vector3( transform.position.x - 400, 3f, transform.position.z - 500 ), transform.rotation ) as GameObject;
			enemy1.name = Resources.Load( "AI_Sloop" ).name;

			GameObject enemy2;
			enemy2 = Instantiate( Resources.Load( "AI_Sloop"), new Vector3( transform.position.x - 500, 3f, transform.position.z - 400 ), transform.rotation ) as GameObject;
			enemy2.name = Resources.Load( "AI_Sloop" ).name;
			
		}

		currentWave++;



	}




}
