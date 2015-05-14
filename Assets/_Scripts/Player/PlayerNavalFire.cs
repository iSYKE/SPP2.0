using UnityEngine;
using System.Collections;

public class PlayerNavalFire : MonoBehaviour {




	// Update is called once per frame
	void Update () {




		//Go through each child in Left and Right gun nodes and invoke firing command in the objects possessing the CannonFire cmponent, ie. cannons
		if( Input.GetKeyDown(KeyCode.Q) ){

			foreach( Transform child in transform.FindChild(string.Format("{0}/Nodes/LeftGuns", transform.GetComponent<CharacterInventory>().characterCurrentShip.shipPrefabName )) ){

				if(child.GetComponentInChildren<CannonFire>()){
					child.GetComponentInChildren<CannonFire>().doFire = true;
					//Debug.Log(child.name + " FIRED!!!");
				}
			}


		}else if( Input.GetKeyDown(KeyCode.E) ){
			
			foreach( Transform child in transform.FindChild(string.Format("{0}/Nodes/RightGuns", transform.GetComponent<CharacterInventory>().characterCurrentShip.shipPrefabName )) ){
				
				if(child.GetComponentInChildren<CannonFire>()){
					child.GetComponentInChildren<CannonFire>().doFire = true;
					//Debug.Log(child.name + " FIRED!!!");
				}
				
				
			}
			
			
			
		}


	}


}
	
