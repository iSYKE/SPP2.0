using UnityEngine;
using System.Collections;

public class ExitMainMenu : MonoBehaviour {

	public Transform CameraTransform;

	void Update() {
		Vector3 targetPosition = new Vector3 (-7.4f, 20.54f, 37.4f);
		Vector3 targetEuler = new Vector3 (14.02f, 133.7f, 0f);

		CameraTransform.position = Vector3.Lerp (CameraTransform.position, targetPosition, Time.deltaTime);
		CameraTransform.eulerAngles = Vector3.Lerp (CameraTransform.eulerAngles, targetEuler, Time.deltaTime);
	}
}
