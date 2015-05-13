using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipList: MonoBehaviour {
	
	
	public List<Ship> gameShips = new List<Ship>(); 
	
	void Start(){
			
		gameShips.Add(new Ship( 0, "Sloop", "A Sloop.", "Sloop", 100f, 10000f, 10f, 150000f, 500f, 5f, 5f, 0.4f,  400f, 200f, 60) );
		gameShips.Add(new Ship( 10, "Brig", "A Brig.", "Brig", 500f, 30000f, 30f, 450000f, 500f, 12f, 6f, 0.4f,  1000f, 300f, 100) );
		
	}
	
	
}

