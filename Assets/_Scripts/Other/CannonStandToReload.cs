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

		if(transform.parent.transform.parent.name == "LeftGuns"){
			transform.root.GetComponent<CharacterShipStats>().gunCountUnloadedLeft--;
		}else if(transform.parent.transform.parent.name == "RightGuns"){
			transform.root.GetComponent<CharacterShipStats>().gunCountUnloadedRight--;
		}

	}


}
