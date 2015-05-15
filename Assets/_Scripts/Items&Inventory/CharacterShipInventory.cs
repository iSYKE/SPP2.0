using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterShipInventory : MonoBehaviour {

	public List<Item> characterShipInventory = new List<Item>();
	//makeCargo class in the future here...

	// Use this for initialization
	void Start () {
	
		for(int i = 0; i <= 23; i++){
			characterShipInventory.Add( GameObject.FindGameObjectWithTag("WorldController").GetComponent<ItemList>().gameItems.Find(x=>x.itemPrefabName == "Cannon1") );
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}





}
