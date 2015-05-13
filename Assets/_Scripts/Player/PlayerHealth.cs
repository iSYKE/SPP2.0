using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using LostPolygon.DynamicWaterSystem;

public class PlayerHealth : MonoBehaviour {

	public float hullStartingHealth = 500;
	public float hullCurrentHealth;
	public Text HealthNumberText;
	public Text GameOverText;
	public RawImage shipHealthImage;
	public Texture hull0;
	public Texture hull25;
	public Texture hull50;
	public Texture hull75;
	public Texture hull100;

	public bool isRepaired = true;
	public bool isSmoking = true;


	//AudioSource playerAudio;
	bool isDead;

	GameObject sinking;
	BuoyancyForce buoyancyForce;



	void Start()
	{
		//playerAudio = GetComponent<AudioSource> ();
		hullCurrentHealth = hullStartingHealth;
		sinking = GameObject.FindGameObjectWithTag ("Player");
		buoyancyForce = sinking.GetComponent<BuoyancyForce> ();
		GameOverText.enabled = false;
		shipHealthImage.texture = hull0;
	}

	void Update()
	{


		HealthNumberText.text = (hullCurrentHealth.ToString() + " / " + hullStartingHealth.ToString());

		if( Input.GetKeyDown( KeyCode.H) && hullCurrentHealth <= 0.5f*hullStartingHealth ){
			
			QuickRepair();
			
		}//MUST BE CALLED BEFORE IsDamaged!

		IsDamaged();
		ShowDamage();




	
	}




	public void TakeDamage (int amount)
	{
		hullCurrentHealth -= amount;

		if ((hullCurrentHealth/hullStartingHealth) * 100 <= 75)
			shipHealthImage.texture = hull25;
		
		if ((hullCurrentHealth/hullStartingHealth) * 100 <= 50)
			shipHealthImage.texture = hull50;

		if ((hullCurrentHealth/hullStartingHealth) * 100 <= 25)
			shipHealthImage.texture = hull75;

		if (hullCurrentHealth <= 0)
			shipHealthImage.texture = hull100;

		//playerAudio.Play();

		if (hullCurrentHealth <= 0 && !isDead) {
			Death();
		}
	}

	void Death()
	{
					
		if( !isDead ){
				
			Quaternion rotUp = Quaternion.Euler( transform.eulerAngles.x - 90f, transform.eulerAngles.y, transform.eulerAngles.z );
				
			GameObject shipDamageSmoke;
			shipDamageSmoke = Instantiate( Resources.Load("VFX/ShipDeathPlume1") , transform.position , rotUp ) as GameObject;
			shipDamageSmoke.name = Resources.Load("VFX/ShipDeathPlume1").name;
			shipDamageSmoke.transform.SetParent( transform );
				
		}

		isDead = true;
		//playerAudio.clip = sinkingClip;
		//playerAudio.Play();

//		Quaternion sinkingRotation = this.transform.rotation;
//		sinkingRotation.z = 55f;
		GameOverText.enabled = true;
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<NavalCameraMovement> ().enabled = false;
		GameObject.FindGameObjectWithTag ("Player").GetComponent<NavalMovement>().sailSet = NavalMovement.SailSet.no;
		buoyancyForce.Density = 1000f;

	}

	void IsDamaged(){
		
		if ( hullCurrentHealth < hullStartingHealth ){
			
			isRepaired = false;
		
		}
		
	}

	void ShowDamage(){

		if ( !isSmoking && !isRepaired && hullCurrentHealth < 0.5f*hullStartingHealth){

			Quaternion rotUp = Quaternion.Euler( transform.eulerAngles.x - 90f, transform.eulerAngles.y, transform.eulerAngles.z );
			
			GameObject shipDamageSmoke;
			shipDamageSmoke = Instantiate( Resources.Load("VFX/ShipDamageSmoke") , transform.position , rotUp ) as GameObject;
			shipDamageSmoke.name = Resources.Load("VFX/ShipDamageSmoke").name;
			shipDamageSmoke.transform.SetParent( transform );

			isSmoking = true;

		}


	}
	
	void QuickRepair(){
		
		hullCurrentHealth = hullCurrentHealth + (0.2f*hullStartingHealth );
		
		isRepaired = true;
		isSmoking = false;
		
		
	}



}
