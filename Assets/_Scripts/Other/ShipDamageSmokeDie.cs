using UnityEngine;
using System.Collections;

public class ShipDamageSmokeDie : MonoBehaviour {

	public AudioClip fireSound;

	void Start() {
		AudioSource.PlayClipAtPoint(fireSound, new Vector3(0,0,0),1f/(1+((GameObject.Find("Main Camera").transform.position - transform.position).magnitude)));
	}

	void Update () {

		if( transform.parent.GetComponent<CharacterShipStats>().isRepaired ){

			Destroy( transform.gameObject, 5f );
			transform.parent.GetComponent<CharacterShipStats>().isOnFire = false;
		
		}else if( transform.position.y < -8f ){

			Destroy( transform.gameObject);

		}
	}


}

