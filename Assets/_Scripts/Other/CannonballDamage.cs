using UnityEngine;
using System.Collections;

public class CannonballDamage : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//print(transform.GetComponent<Rigidbody>().velocity.magnitude);

	}

	void OnCollisionEnter(Collision collision) {

		foreach (ContactPoint contact in collision.contacts) {
			//Debug.DrawRay(contact.point, contact.normal, Color.yellow, 10f);

			if( contact.otherCollider.transform.parent){

				if ( contact.otherCollider.transform.parent.transform.GetComponent<AIHealth>() || contact.otherCollider.transform.parent.transform.GetComponent<PlayerHealth>()){

					Destroy (gameObject);

					if(contact.otherCollider.transform.parent.transform.GetComponent<AIHealth>()){

						contact.otherCollider.transform.parent.transform.GetComponent<AIHealth>().aiHealth -= 10;
					
					}else if(contact.otherCollider.transform.parent.transform.GetComponent<PlayerHealth>()){

						contact.otherCollider.transform.parent.transform.GetComponent<PlayerHealth>().TakeDamage( 10 );

					}

					Instantiate( Resources.Load("VFX/WoodSplinter") , contact.point , Quaternion.Euler( contact.normal) );

					Destroy (gameObject);


				}

			}


		}
				
	}


}
