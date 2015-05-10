using UnityEngine;
using System.Collections;

public class NavalMovement : MonoBehaviour {


	public enum Wind {
		left, right
	}
	public Wind wind;

	GameObject courseSail;

	public enum SailSet {
		no,
		min,
		max
	}
	
	public SailSet	sailSet;

	public float sailNoMod 		= 0f;
	public float sailMinMod 	= 0.5f;
	public float sailMaxMod		= 1f;

	public float currSailSetMod;


	public Quaternion 	shipDir;
	public Quaternion	windDir;

	public float	windHdg;

	public float 	shipVel;
	public float 	windStrength;
	public float 	sailCharCoef; // Comparative to the area of the sails... bigger == more force.
	public float 	sailIntoWind; 
	public float	sailIntoWindModifier; //Depends on type of rigging, essentially how much into the wind can a ship sail...

	public float 	turningForce;
	public float	turningForceCoeff;

	public bool turnRight;
	public bool turnLeft;




	// Use this for initialization
	void Start () {

		sailSet = SailSet.no;
		sailCharCoef 		= 100000f;
		turningForceCoeff 	= 1000000f;


		courseSail = GameObject.Find("CourseSail").gameObject;



	}
	
	// Update is called once per frame
	void FixedUpdate () {

		windHdg = GameObject.FindGameObjectWithTag("WorldController").GetComponent<WindSim>().windDirection;
		LRwind();



		windStrength = GameObject.FindGameObjectWithTag("WorldController").GetComponent<WindSim>().windStrength;

		windDir = Quaternion.Euler( 0, GameObject.FindGameObjectWithTag("WorldController").GetComponent<WindSim>().windDirection, 0 );
		shipDir = transform.rotation;
		//Calculate abs value of quaternion dot prduct between boat and the wind.
		sailIntoWind = Mathf.Abs(Quaternion.Dot(shipDir, windDir));

		//print(Quaternion.Dot(shipDir, windDir));

		WindForceOnShip();



		if( turnRight == true ){

			TurnForceOnShipRight();

		}else if( turnLeft == true){

			TurnForceOnShipLeft();

		}


		if( sailSet == SailSet.no){

			currSailSetMod = sailNoMod;

			print (sailSet);

		}else if (sailSet == SailSet.min){

			currSailSetMod = sailMinMod;

			print (sailSet);

			courseSail.SetActive(false);
		
		}else if(sailSet == SailSet.max){

			currSailSetMod = sailMaxMod;

			print (sailSet);

			courseSail.SetActive(true);

		}

		//print (transform.GetComponent<Rigidbody>().velocity.magnitude +"     "+ transform.GetComponent<Rigidbody>().angularVelocity.magnitude);

			
	}

	//---------------------------------------------------------//



	void WindForceOnShip(){

		Vector3 vec = new Vector3( 0, 0, 1);

		float windEffect = windStrength * sailCharCoef * sailIntoWind * currSailSetMod ;
		transform.GetComponent<Rigidbody>().AddForceAtPosition( (windEffect * transform.forward) ,  transform.position /*+ Vector3.up*/ );

		Wind windLR = wind;

		if( windLR == Wind.left){

			transform.GetComponent<Rigidbody>().AddRelativeTorque(  -0.2f * (1-sailIntoWind) * vec * windEffect );

		}else if( windLR == Wind.right){
			
			transform.GetComponent<Rigidbody>().AddRelativeTorque(  0.2f * (1-sailIntoWind)* vec * windEffect );

		}


	}

	//Turns the Ship.
	void TurnForceOnShipLeft(){

		//transform.FindChild("Sloop/Rudder").transform.localEulerAngles = new Vector3(0,45,0);

		float velocity = transform.GetComponent<Rigidbody>().velocity.magnitude;

		Vector3 vec = new Vector3(0, 1, 0);

		turningForce =  ( (2*velocity + 5)/(velocity + 1) ) * turningForceCoeff;

		transform.GetComponent<Rigidbody>().AddRelativeTorque(  -1 * vec * turningForce );

	}
	void TurnForceOnShipRight(){

		//transform.FindChild("Sloop/Rudder").transform.localEulerAngles = new Vector3(0,-45,0);


		float velocity = transform.GetComponent<Rigidbody>().velocity.magnitude;

		Vector3 vec = new Vector3( 0, 1, 0);

		turningForce = ( (velocity + 5)/(velocity + 1) ) * turningForceCoeff;
		
		transform.GetComponent<Rigidbody>().AddRelativeTorque(   1 * vec * turningForce );

	}





	//Determining wind direction relative ot the ship:
	
	void LRwind(){
		
		float parentHdg = transform.rotation.eulerAngles.y;
		
		float parentHdgR 	= parentHdg * 2 * Mathf.PI / 360;
		float windHdgR 		= windHdg * 2 * Mathf.PI / 360;
		
		
		
		//FIRST QUADRANT
		if( Mathf.Cos(parentHdgR) > 0 && Mathf.Sin(parentHdgR) > 0 ){
			
			if(  Mathf.Sin(windHdgR) < 0  &&  ((windHdg+180)%360) > parentHdg  ){
				
				wind = Wind.right;
				//print (wind);
				
			}else if(  Mathf.Sin(windHdgR) > 0  &&  (windHdg) < parentHdg ){
				
				wind = Wind.right;
				//print (wind);
				
			}else {
				
				wind = Wind.left;
				//print (wind);
			}
			
			
			
			//SECOND QUADRANT
			
		}else if( Mathf.Cos(parentHdgR) < 0 && Mathf.Sin(parentHdgR) > 0 ){
			
			if( Mathf.Sin(windHdgR) < 0  &&  ((windHdg+180)%360) > parentHdg    ){
				
				wind = Wind.right;
				//print (wind);
				
			}else if(  Mathf.Sin(windHdgR) > 0  &&  (windHdg) < parentHdg ){
				
				wind = Wind.right;
				//print (wind);
				
			}else {
				
				wind = Wind.left;
				//print (wind);
			}
			
			
			
			
			
			//THIRD QUADRANT
			
		}else if( Mathf.Cos(parentHdgR) < 0 && Mathf.Sin(parentHdgR) < 0 ){
			
			if( Mathf.Sin(windHdgR) < 0  &&  windHdg > parentHdg    ){
				
				wind = Wind.left;
				//print (wind);
				
			}else if(  Mathf.Sin(windHdgR) > 0  &&  (windHdg) < ((parentHdg+180)%360) ){
				
				wind = Wind.left;
				//print (wind);
				
			}else {
				
				wind = Wind.right;
				//print (wind);
			}
			
			
			//FOURTH QUADRANT
			
		}else if( Mathf.Cos(parentHdgR) > 0 && Mathf.Sin(parentHdgR) < 0 ){
			
			if( Mathf.Sin(windHdgR) < 0  &&  windHdg > parentHdg    ){
				
				wind = Wind.left;
				//print (wind);
				
			}else if(  Mathf.Sin(windHdgR) > 0  &&  (windHdg) < ((parentHdg+180)%360) ){
				
				wind = Wind.left;
				//print (wind);
				
			}else {
				
				wind = Wind.right;
				//print (wind);
			}
			
		}
	}






}
