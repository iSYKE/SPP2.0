using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemList : MonoBehaviour {
	
	
	public List<Item> gameItems = new List<Item>(); 
	
	void Start(){
		//Weapons IDs #: 0-9
		gameItems.Add(new Item(0, "Short Cannon", "Basic ship cannon", "Cannon1", 200f, true, Item.ItemType.Cannon, Item.ItemStorageType.GunNode, 100f, 5f));

		//Projectiles IDs #: 10-19 
		gameItems.Add(new Item(10, "Shot", "Cannonball", "Shot", 5f, false, Item.ItemType.Projectile, Item.ItemStorageType.Hold, 5f, 0f ));

		//Ships IDs 80-99

		gameItems.Add(new Item(80, "Sloop", "A Sloop", "Sloop", 150000f, true, Item.ItemType.Ship, Item.ItemStorageType.CurrentShip, 200, 0f ));
		gameItems.Add(new Item(81, "Brig", "A Brig", "Brig", 1500000f, true, Item.ItemType.Ship, Item.ItemStorageType.CurrentShip, 400, 0f ));

	}
	
	
}
