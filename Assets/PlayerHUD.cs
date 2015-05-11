using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHUD : MonoBehaviour {

	public Text SpeedText;
	public RectTransform NeedleTransform;

	float speed;
	float direction;

	void Awake () 
	{
		SpeedText = GameObject.Find ("SpeedText").GetComponent<Text> ();
		NeedleTransform = GameObject.Find ("NeedleRawImage").GetComponent<RectTransform> ();
	}

	void Update () 
	{
		SetSpeed ();
		SetCompassDirection ();
	}

	void SetSpeed()
	{
		speed = transform.GetComponent<Rigidbody> ().velocity.magnitude;
		speed /= 1.94f;

		int speedInt = (int)speed;

		SpeedText.text = speedInt + " Knots";
	}

	void SetCompassDirection() {
		NeedleTransform.eulerAngles = new Vector3 (0f, 0f, -transform.eulerAngles.y);
	}
}
