using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using LostPolygon.DynamicWaterSystem;

public class PlayerHealth : MonoBehaviour {

	public int hullStartingHealth = 100;
	public int hullCurrentHealth;
	public Text HealthNumberText;
	public Text GameOverText;
	public RawImage shipHealthImage;
	public Texture hull0;
	public Texture hull25;
	public Texture hull50;
	public Texture hull75;
	public Texture hull100;

	//AudioSource playerAudio;
	bool isDead;

	GameObject sinking;
	BuoyancyForce buoyancyForce;



	void Awake()
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
	}

	public void TakeDamage (int amount)
	{
		hullCurrentHealth -= amount;

		if (hullCurrentHealth == 150)
			shipHealthImage.texture = hull25;
		
		if (hullCurrentHealth == 100)
			shipHealthImage.texture = hull50;

		if (hullCurrentHealth == 50)
			shipHealthImage.texture = hull75;

		if (hullCurrentHealth == 0)
			shipHealthImage.texture = hull100;

		//playerAudio.Play();

		if (hullCurrentHealth <= 0 && !isDead) {
			Death();
		}
	}

	void Death()
	{
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
}
