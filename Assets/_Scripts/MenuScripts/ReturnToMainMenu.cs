using UnityEngine;
using System.Collections;

public class ReturnToMainMenu : MonoBehaviour {

	public Transform CameraTransform;

	void Update () {
		CameraTransform = GameObject.Find ("Main Camera").GetComponent<Transform> ();

		Vector3 targetPosition = new Vector3 (-49.9f, 30f, 74.7f);
		Vector3 targetEuler = new Vector3 (10.1f, 105.9f, 0f);
		
		CameraTransform.position = Vector3.Lerp (CameraTransform.position, targetPosition, Time.deltaTime);
		CameraTransform.eulerAngles = Vector3.Lerp (CameraTransform.eulerAngles, targetEuler, Time.deltaTime);
	}
}
