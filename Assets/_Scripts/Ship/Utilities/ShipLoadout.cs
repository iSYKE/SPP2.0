using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipLoadout : MonoBehaviour {

	public List<GameObject> leftGuns  = new List<GameObject>();
	public List<GameObject> rightGuns = new List<GameObject>();

	//UPGRADES LATER?

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}


	//---------------------------------------------------
	//SIMPLE ADD GUN, TEMPORARY, LATER WILL BE BASED OFF OF THE UI.
	public void AddGuns(){

		leftGuns.ForEach( delegate(GameObject obj) {
			if(	transform.GetComponent<CharacterShipInventory>().characterShipInventory.Exists(x => x.itemType == Item.ItemType.Cannon) ){

				transform.GetComponent<CharacterShipInventory>().characterShipInventory.Remove(
					transform.GetComponent<CharacterShipInventory>().characterShipInventory.Find(x=>x.itemType == Item.ItemType.Cannon));

				GameObject cannon;
				cannon = Instantiate( Resources.Load("Guns/CannonStand"), obj.transform.position, obj.transform.rotation) as GameObject;
				cannon.name = Resources.Load("Guns/CannonStand").name;
				cannon.transform.parent = obj.transform;

				//Add guns to gun count in ships stats:
				transform.GetComponent<CharacterShipStats>().gunCurrentCountLeft ++;

			}else{
				print("No guns in inventory.");
			}
			
		});

		rightGuns.ForEach( delegate(GameObject obj) {
			if(	transform.GetComponent<CharacterShipInventory>().characterShipInventory.Exists(x => x.itemType == Item.ItemType.Cannon) ){
				
				transform.GetComponent<CharacterShipInventory>().characterShipInventory.Remove(
					transform.GetComponent<CharacterShipInventory>().characterShipInventory.Find(x=>x.itemType == Item.ItemType.Cannon));
				
				GameObject cannon;
				cannon = Instantiate( Resources.Load("Guns/CannonStand"), obj.transform.position, obj.transform.rotation) as GameObject;
				cannon.name = Resources.Load("Guns/CannonStand").name;
				cannon.transform.parent = obj.transform;
				
				//Add guns to gun count in ships stats:
				transform.GetComponent<CharacterShipStats>().gunCurrentCountRight ++;

			}else{
				print("No guns in inventory.");
			}
			
		});
				


	}


	//USED FOR KEEPING TRACK OF GUN NODES
	public void ListGuns(){
		leftGuns.Clear();
		rightGuns.Clear();
		ListLeftGuns();
		ListRightGuns();
	}

	
	void ListLeftGuns(){

		foreach( Transform child in transform.FindChild(string.Format("{0}/Nodes/LeftGuns", transform.GetComponent<CharacterInventory>().characterCurrentShip.shipPrefabName )) ){
			
			if( child ){
				
				leftGuns.Add( child.gameObject );
				
			}	
		}

	}

	void ListRightGuns(){
		
		foreach( Transform child in transform.FindChild(string.Format("{0}/Nodes/RightGuns", transform.GetComponent<CharacterInventory>().characterCurrentShip.shipPrefabName )) ){
			
			if( child ){
				
				rightGuns.Add( child.gameObject );
				
			}	
		}
		
	}


	/*Adding gun method:
	void AddGunMethod( GameObject go ){

		if( !transform.FindChild("CannonStand")){
			GameObject cannon;
			cannon = Instantiate( Resources.Load("Guns/CannonStand"), go.transform.position, go.transform.rotation );
			cannon.name = Resources.Load("Guns/CannonStand").name;

		}

	}*/

}
