using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHUD : MonoBehaviour {

	public Text SpeedText;
	public RectTransform NeedleTransform;
	//public Camera miniMapCamera;

	float speed;
	float direction;

	bool isMiniMapOpen = false;

	void OnLevelWasLoaded(int level) {
		if (level == 1) {
			SpeedText = GameObject.Find ("Canvas/CompassParent/SpeedText").GetComponent<Text> ();
			NeedleTransform = GameObject.Find ("NeedleRawImage").GetComponent<RectTransform> ();
			//miniMapCamera = GameObject.Find ("MapCamera").GetComponent<Camera> ();
		}
	}

	void Start () 
	{

	}

	void Update () 
	{
		SetSpeed ();
		SetCompassDirection ();
		//MiniMap ();
	}

	void SetSpeed()
	{
		speed = transform.GetComponent<Rigidbody> ().velocity.magnitude;
		speed /= 1.94f;

		int speedInt = (int)speed;

		//SpeedText.text = speedInt + " Knots";
	}

	void SetCompassDirection() {
		//NeedleTransform.eulerAngles = new Vector3 (0f, 0f, -transform.eulerAngles.y);
	}

//	void MiniMap() {
//		if (Input.GetKeyDown (KeyCode.M)) {
//			if (isMiniMapOpen == false) {
//				miniMapCamera.enabled = true;
//				isMiniMapOpen = true;
//			} else if(isMiniMapOpen == true) {
//				miniMapCamera.enabled = false;
//				isMiniMapOpen = false;
//			}
//		}
//	}
}
