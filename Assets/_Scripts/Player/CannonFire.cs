using UnityEngine;
using System.Collections;

public class CannonFire : MonoBehaviour {

	// This is script goes on a cannon object and the cannon
	// can be made to fire by invoking the "doFire" command
	// from a player control script (eg: PlayerNavalFire).

	public bool doFire 		= false;
	public bool isReloaded 	= true;
	public AudioClip shotSound;

	public float currReTime = 0f;
	public float reTime;
	public string iName;
	public string projectileName;
	public string smokeName;

	void Start(){

		iName 	= transform.name;
		reTime 	= GameObject.FindGameObjectWithTag("WorldController").GetComponent<ItemList>().gameItems.Find(x => x.itemPrefabName == iName).itemRecharge;
		projectileName = "Shot";
		//smokeName = "CannonSmoke1";

	}

	void Update(){
		// Fire if reloaded
		if( doFire == true && isReloaded == true){

			float time1 = Random.Range(0f,1f);
			
			StartCoroutine(DelayShot(time1));


			isReloaded 	= false;
			doFire 		= false;
		}

		//Reload conditions
		if( isReloaded == false && currReTime < reTime ){

			currReTime += Time.deltaTime;
			doFire 		= false;

		}else if(isReloaded == false && currReTime > reTime ){

			currReTime = 0f;
			isReloaded = true;
			doFire 		= false;

		}
	}

	public void SpawnProjectile(){

		GameObject projectile;
		projectile = Instantiate( Resources.Load("Projectiles/"+projectileName) , transform.position, transform.rotation) as GameObject;
		projectile.name = GameObject.FindGameObjectWithTag("WorldController").GetComponent<ItemList>().gameItems.Find(x => x.itemPrefabName == projectileName).itemPrefabName;
		projectile.transform.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * 100f);

		GameObject smoke;
		smoke = Instantiate( Resources.Load("VFX/ShootingParticles") , transform.position , transform.rotation) as GameObject;
		smoke.name = Resources.Load("VFX/ShootingParticles").name;
		GameObject fire;
		fire = Instantiate( Resources.Load("VFX/CannonFire1") , transform.position , transform.rotation) as GameObject;
		fire.name = Resources.Load("VFX/CannonFire1").name;
	}

	public void PlayAudio()
	{
		AudioSource.PlayClipAtPoint(shotSound, new Vector3(0,0,0),1f/(1+((GameObject.Find("Main Camera").transform.position - transform.position).magnitude)));
	}


	IEnumerator DelayShot(float time) {
		yield return new WaitForSeconds(time);
		SpawnProjectile();
		PlayAudio();
	}




}
