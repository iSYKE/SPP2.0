using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterInventory : MonoBehaviour {



	public List<Item> characterItems = new List<Item>();

	public Ship playerCurrentShip;
	public GameObject playerShip;



	void Start(){

		characterItems.Add( GameObject.FindGameObjectWithTag("WorldController").GetComponent<ItemList>().gameItems.Find( x => x.itemPrefabName == "Brig" ));



	}


}
