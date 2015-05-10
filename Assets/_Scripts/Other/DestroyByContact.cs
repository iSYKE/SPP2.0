using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour 
{
	public GameObject explosion;

	void OnCollisionEnter(Collision other)
	{
		Instantiate (explosion, other.transform.position, other.transform.rotation);
		Destroy (gameObject);
	}
}
