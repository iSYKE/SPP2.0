using UnityEngine;
using System.Collections;

public class WindPointer : MonoBehaviour {


	void Update () {
		transform.rotation = Quaternion.Euler(0, GameObject.FindGameObjectWithTag("WorldController").GetComponent<WindSim>().windDirection ,0);
	}

}
