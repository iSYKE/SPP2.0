using UnityEngine;
using System.Collections;

public class WindSim : MonoBehaviour {

	public float	windStrength 	= 0f;
	public float	windDirection 	= 20f; 


	// Use this for initialization
	void Start () {
	
		print(windDirection +"@"+windStrength);

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if( windDirection >= 360f){
			windDirection = 0;
		}else if( windDirection < 0f ){
			windDirection = 359.99f;
		}

	}
}
