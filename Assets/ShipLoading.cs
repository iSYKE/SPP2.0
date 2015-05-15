using UnityEngine;
using System.Collections;
using LostPolygon.DynamicWaterSystem;

public class ShipLoading : MonoBehaviour {
	
	void OnLevelWasLoaded(int level) {
		if (level == 1) {
			Destroy(transform.GetComponent<BuoyancyForce>());

			Destroy(transform.GetComponent<CharacterInventory>().characterShip);
			print ("Destroyed Ship");
			transform.GetComponent<CharacterInventory>().characterCurrentShip = null;
			print ("Nulled Ship");
			transform.GetComponent<CharacterInventory>().FillCharCurrShip();
			print ("ReFilled Ship");
		
		}
	}
	
	void Awake() {
		DontDestroyOnLoad ( transform.gameObject );
	}
	
	void Update() {
	}
}
