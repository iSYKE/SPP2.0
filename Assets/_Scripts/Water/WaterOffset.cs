using UnityEngine;
using System.Collections;

public class WaterOffset : MonoBehaviour {
	//This class centres the ocean on Player at preset intervals

	public float offsetTimer = 10.0f;
	private float timer = 0f;
	public float sizex;
	public float sizey;
	
	void Start (){
		Vector3 playerPos = GameObject.Find("Player").transform.position;
		transform.position = new Vector3(playerPos.x - sizex/2, 0, playerPos.z - sizey/2);
	}

	void Update (){
		if(timer >= offsetTimer){
			Vector3 playerPos 	= GameObject.Find("Player").transform.position;
			transform.position = new Vector3(playerPos.x - sizex/2, 0, playerPos.z - sizey/2);
			timer = 0f;
		}else{
			timer += Time.deltaTime;
		}
	
	}

}


