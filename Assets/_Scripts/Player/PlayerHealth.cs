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
	public Texture2D hull0;
	public Texture2D hull25;
	public Texture2D hull50;
	public Texture2D hull75;
	public Texture2D hull100;

	public bool isRepaired = true;
	public bool isSmoking = true;


	//AudioSource playerAudio;
	bool isDead;

	GameObject sinking;
	BuoyancyForce buoyancyForce;

	void OnLevelWasLoaded(int level) {
		if (level == 1) {
			hullCurrentHealth = hullStartingHealth;
			sinking = GameObject.FindGameObjectWithTag ("Player");
			shipHealthImage = GameObject.Find("Canvas/HealthUI/ShipHealthRawImage").GetComponent<RawImage>();




			//hull0 = Resources.Load("/HUDImages/Hull0");
//			hull25 = Resources.Load("/HUDImages/Hull25");
//			hull50 = Resources.Load("/HUDImages/Hull50");
//			hull75 = Resources.Load("/HUDImages/Hull75");
//			hull100 = Resources.Load("/HUDImages/Hull100");
			shipHealthImage.texture = (Texture)hull0;
			HealthNumberText = GameObject.Find("Canvas/HealthUI/HealthNumberText").GetComponent<Text>();
			GameOverText = GameObject.Find("Canvas/GameOver/GameOverText").GetComponent<Text>();
			GameOverText.text = "";
		}
	}

	void Start()
	{
		//playerAudio = GetComponent<AudioSource> ();

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

		if ((hullCurrentHealth / hullStartingHealth) * 100 <= 75)
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
		GameOverText.text = "GAME OVER";
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<NavalCameraMovement> ().enabled = false;
		GameObject.FindGameObjectWithTag ("Player").GetComponent<NavalMovement>().sailSet = NavalMovement.SailSet.no;

		//buoyancyForce = sinking.GetComponent<BuoyancyForce> ();
		//buoyancyForce.Density = 1200f;

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
