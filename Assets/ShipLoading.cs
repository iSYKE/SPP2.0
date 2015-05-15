using UnityEngine;
using System.Collections;
using LostPolygon.DynamicWaterSystem;

public class ShipLoading : MonoBehaviour {
	
	void OnLevelWasLoaded(int level) {
		if (level == 1) {
			Destroy(transform.GetComponent<BuoyancyForce>());
			Destroy(transform.GetComponent<CharacterInventory>().characterShip);
			transform.GetComponent<CharacterInventory>().characterCurrentShip = null;
			transform.GetComponent<CharacterInventory>().FillCharCurrShip();
		}
	}
	
	void Awake() {
		DontDestroyOnLoad (this);
	}
	
	void Update() {
	}
}
