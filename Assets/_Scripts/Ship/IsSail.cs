using UnityEngine;
using System.Collections;

public class IsSail : MonoBehaviour {


	//THIS CLASS GOES ON A SAIL TO IDENTIFY IT AS SUCH

	float windHdg;
	float windStr;




	void FixedUpdate(){

		windHdg = GameObject.FindGameObjectWithTag("WorldController").GetComponent<WindSim>().windDirection;
		windStr = GameObject.FindGameObjectWithTag("WorldController").GetComponent<WindSim>().windStrength;

		HDGtoV3();

	}



	void HDGtoV3(){

		float windHdgR = windHdg * 2 * Mathf.PI / 360;

		float forceX =  windStr * Mathf.Sin( windHdgR );
		float forceZ =  windStr * Mathf.Cos( windHdgR );


		transform.GetComponent<Cloth>().externalAcceleration = new Vector3 ( forceX, 0, forceZ );



	}



}
