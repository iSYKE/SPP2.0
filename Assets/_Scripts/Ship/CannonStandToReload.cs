using UnityEngine;
using System.Collections;

public class CannonStandToReload : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void ReloadCannon(){

		transform.FindChild("Cannon1").GetComponent<CannonFire>().isReloaded = true;

	}


}
