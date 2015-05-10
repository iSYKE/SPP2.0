using UnityEngine;
using System.Collections;

public class PlayCannonFire : MonoBehaviour {

	bool fire;

	void Update()
	{
		fire = true;

		if (fire == true) {
			foreach( Transform child in transform.FindChild("Sloop/Nodes/LeftGuns") ){
				if(child.GetComponentInChildren<CannonFire>()){
					child.GetComponentInChildren<CannonFire>().doFire = true;
					Debug.Log(child.name + " FIRED!!!");
				}
			}
		}
	}
}