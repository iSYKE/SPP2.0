using UnityEngine;
using System.Collections;

public class AtGameLoad : MonoBehaviour {

	public Transform CameraTransform;

	void Start () {
		CameraTransform.position = new Vector3 (-49.9f, 30f, 74.7f);
		CameraTransform.eulerAngles = new Vector3 (10.1f, 105.9f, 0f);

		//CameraTransform.position = new Vector3 (-7.4f, 20.54f, 37.4f);
		//CameraTransform.eulerAngles = new Vector3 (14.02f, 133.7f, 0f);
	}
}
