using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterInventory : MonoBehaviour {

	public List<Item> characterInventory = new List<Item>();

	void Start(){

		characterInventory.Add( GameObject.FindGameObjectWithTag("WorldController").GetComponent<ItemList>().gameItems.Find( x => x.itemPrefabName == "Brig" ));

	}


}
