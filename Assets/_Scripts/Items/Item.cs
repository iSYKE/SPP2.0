using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item{
	//Base attributes:
	public int 		itemID;
	public string 	itemName;
	public string 	itemDescription;
	public string 	itemPrefabName;
	//Item use attribute:

	public float  	itemMass;

	public bool isEquippable;
	//Item... 

	public ItemType itemType;
	public ItemStorageType itemStorageType;

	public enum ItemType{
		Cannon,
		Projectile,
		Rigging
	}

	public enum ItemStorageType{
		GunNode,
		Hold
	}

	public float itemStrength;
	public float itemRecharge;


	public Item(int id, string iname, string idesc, string iprefabname, float imass, bool isequip, ItemType itype, ItemStorageType iStype, float iStrength, float iRecharge){
		itemID = id;
		itemName = iname;
		itemDescription = idesc;
		itemPrefabName = iprefabname;
		itemMass = imass;
		isEquippable = isequip;
		itemType = itype;
		itemStorageType = iStype;
		itemStrength = iStrength;
		itemRecharge = iRecharge;
	}


}
