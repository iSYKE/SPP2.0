using UnityEngine;
using System.Collections;

public class AftSailWindTrack : MonoBehaviour {




	public float	desiredRot;
	public float	currentRot;
	public float	windHdg;
	
	public float 	deflectCoeff;
	public float 	minDeflectCoeff;
	public float 	maxDeflectCoeff;
	
	public Vector3	zeroVector;
	
	NavalMovement.Wind wind; 
	
	
	
	void Start () {
		
		maxDeflectCoeff = 0.92f;
		minDeflectCoeff = 0.34f;
		zeroVector = new Vector3 (0, 0 , 0);
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {


		wind = transform.GetComponentInParent<NavalMovement>().wind;
		
		deflectCoeff = transform.GetComponentInParent<NavalMovement>().sailIntoWind;
		
		windHdg = GameObject.FindGameObjectWithTag("WorldController").GetComponent<WindSim>().windDirection;
		currentRot = transform.rotation.eulerAngles.y;
		desiredRot = windHdg;
	
		if( wind == NavalMovement.Wind.left){
			
			LeftWind();
			
		}else if( wind == NavalMovement.Wind.right){
			
			RightWind ();
			
		}


	}


	//---------------------------------------------------------

	//Wind from left:
	void LeftWind(){
		
		if( currentRot != desiredRot  &&  deflectCoeff > (maxDeflectCoeff + 0.02) ){
			
			//print ("ROTATING SAIL 111");
			
			transform.localRotation = Quaternion.Lerp( transform.localRotation, Quaternion.Euler( 0, 320 ,0), 1*Time.deltaTime );
			
			
			
		}else if (transform.localEulerAngles != new Vector3 (0 , 0 , 0) && deflectCoeff < (minDeflectCoeff + 0.02) ) {
			
			transform.localRotation = Quaternion.Lerp( transform.localRotation, Quaternion.Euler( 0,0,0), 1*Time.deltaTime );
			
			
		}else if ( deflectCoeff < maxDeflectCoeff &&  deflectCoeff > minDeflectCoeff ){
			
			transform.localRotation = Quaternion.Lerp( transform.localRotation, Quaternion.Euler( 0, 320 ,0), 1*Time.deltaTime );
			
		}
	}
	
	
	
	
	//Wind from right:
	void RightWind(){
		
		if( currentRot != desiredRot  &&  deflectCoeff > (maxDeflectCoeff + 0.02) ){
			
			//print ("ROTATING SAIL 222");
			
			transform.localRotation = Quaternion.Lerp( transform.localRotation, Quaternion.Euler( 0, 40 ,0), 1*Time.deltaTime );
			
			
			
		}else if (transform.localEulerAngles != new Vector3 (0 , 0 , 0) && deflectCoeff < (minDeflectCoeff + 0.02) ) {
			
			transform.localRotation = Quaternion.Lerp( transform.localRotation, Quaternion.Euler( 0,0,0), 1*Time.deltaTime );
			
			
		}else if ( deflectCoeff < maxDeflectCoeff &&  deflectCoeff > minDeflectCoeff ){
			
			transform.localRotation = Quaternion.Lerp( transform.localRotation, Quaternion.Euler( 0, 40 ,0) , 1*Time.deltaTime );
			
		}
		
		
	}
	
	
}
