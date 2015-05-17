using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipList : MonoBehaviour {
	
	
	public List<Ship> gameShips = new List<Ship>(); 
	
	void Start(){
			
		gameShips.Add(new Ship( 0, "Sloop", "A Sloop.", "Sloop", 100f, 10000f, 10f, 150000f, 600f, -1f, 4.5f, 2f, 0.4f,  400f, 200f, 60, 6, 6) );

		gameShips.Add(new Ship( 1, "Brig", "A Brig.", "Brig", 500f, 30000f, 30f, 450000, 550f, -0.7f, 12f, 4.5f, 0.35f,  1000f, 300f, 100, 12, 12) );
		
	}
	
	
}

