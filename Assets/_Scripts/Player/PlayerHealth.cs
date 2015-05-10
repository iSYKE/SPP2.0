using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using LostPolygon.DynamicWaterSystem;

public class PlayerHealth : MonoBehaviour {

	public int hullStartingHealth = 100;
	public int hullCurrentHealth;
	public Slider healthSlider;
	public Text HealthNumberText;
	public Text GameOverText;
	public Image damageImage;
	//public AudioClip sinkingClip;
	public float flashSpeed = 5f;
	public Color flashColor = new Color(1f, 0f, 0f, 0.1f);

	//AudioSource playerAudio;
	bool isDead;
	bool damaged;

	GameObject sinking;
	BuoyancyForce buoyancyForce;



	void Awake()
	{
		//playerAudio = GetComponent<AudioSource> ();
		hullCurrentHealth = hullStartingHealth;
		sinking = GameObject.FindGameObjectWithTag ("Player");
		buoyancyForce = sinking.GetComponent<BuoyancyForce> ();
		GameOverText.enabled = false;
	}

	void Update()
	{
		if (damaged) {
			damageImage.color = flashColor;
		} else {
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		HealthNumberText.text = (hullCurrentHealth.ToString() + " / " + hullStartingHealth.ToString());
		damaged = false;
	}

	public void TakeDamage (int amount)
	{
		damaged = true;
		hullCurrentHealth -= amount;
		healthSlider.value = hullCurrentHealth;
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
