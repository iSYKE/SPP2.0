using UnityEngine;
using System.Collections;

public class ShipDamageSmokeDie : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {

		if( transform.parent.GetComponent<CharacterShipStats>().isRepaired ){

			Destroy( transform.gameObject, 5f );
			transform.parent.GetComponent<CharacterShipStats>().isOnFire = false;
		
		}else if( transform.position.y < -8f ){

			Destroy( transform.gameObject);

		}

	}


}

