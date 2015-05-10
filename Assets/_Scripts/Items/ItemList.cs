﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemList : MonoBehaviour {
	
	
	public List<Item> gameItems = new List<Item>(); 
	
	void Start(){
		//Weapons IDs #: 00-09
		gameItems.Add(new Item(00, "Short Cannon", "Basic ship cannon", "Cannon1", 200f, true, Item.ItemType.Cannon, Item.ItemStorageType.GunNode, 100f, 5f));

		//Projectiles IDs #: 10-19 
		gameItems.Add(new Item(11, "Shot", "Cannonball", "Shot", 5f, false, Item.ItemType.Projectile, Item.ItemStorageType.Hold, 5f, 0f ));
	}
	
	
}
