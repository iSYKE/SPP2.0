using UnityEngine;
using System.Collections;
using LostPolygon.DynamicWaterSystem;

public class ShipLoading : MonoBehaviour {

	void OnLevelWasLoaded(int level) {
		if (level == 0) {
			transform.GetComponent<CharacterInventory>().characterCurrentShip = null;
			transform.GetComponent<CharacterInventory>().FillCharCurrShip();
		}
	}

	void Awake() {
		DontDestroyOnLoad (transform.gameObject);
	}

	void Update() {

	}
}
