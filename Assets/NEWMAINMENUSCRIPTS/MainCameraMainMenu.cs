using UnityEngine;
using System.Collections;

public class MainCameraMainMenu : MonoBehaviour {

	public Transform CameraTransform;
	public Transform ShipTransform;

	public bool MoveOutOfMainMenu = false;
	public bool MoveBackToMainMenu = false;
	public bool NewGameAngle = false;
	public bool LoadGameAngle = false;
	
	void Start () {
		Camera.main.transform.position = new Vector3 (-49.9f, 30f, 74.7f);
		Camera.main.transform.eulerAngles = new Vector3 (10.1f, 105.9f, 0f);
		
		ShipTransform = GameObject.Find ("Player").GetComponent<Transform> ();
		ShipTransform.position = new Vector3 (0f, 0f, -60f);
		
		DontDestroyOnLoad (GameObject.Find ("Player"));
		DontDestroyOnLoad (GameObject.Find ("HUDCanvas"));
		DontDestroyOnLoad (GameObject.Find ("_WorldController"));
		
		//Zoomed In Coords For The Ship Selection View
		//CameraTransform.position = new Vector3 (-7.4f, 20.54f, 37.4f);
		//CameraTransform.eulerAngles = new Vector3 (14.02f, 133.7f, 0f);
	}
	
	void Update() {
		Vector3 targetPosition = new Vector3 (0f, 0f, 0f);
		
		ShipTransform.position = Vector3.Lerp (ShipTransform.position, targetPosition, (Time.deltaTime * .2f));

		if (MoveOutOfMainMenu) {
			RotateInForSecondaryMenu();
			StartCoroutine(CameraMoveInCoroutine(4.13f));
		}
		if (MoveBackToMainMenu) {
			RotateBackOutToMainMenu();
			StartCoroutine(CameraMoveOutCoroutine(4.13f));
		}
		if (NewGameAngle) {
			NewGameCameraAngle();
			StartCoroutine(NewGameCameraMoveCoroutine(5.7f));
		}
		if (LoadGameAngle) {
			LoadGameCameraAngle();
			StartCoroutine(LoadGameCameraMoveCoroutine(4.13f));
		}
	}

	void RotateInForSecondaryMenu() {
		Vector3 targetPosition = new Vector3 (-7.4f, 20.54f, 37.4f);
		Vector3 targetEuler = new Vector3 (14.02f, 133.7f, 0f);

		transform.position = Vector3.Lerp (transform.position, targetPosition, Time.deltaTime);
		transform.eulerAngles = Vector3.Lerp (transform.eulerAngles, targetEuler, Time.deltaTime);
	}

	void RotateBackOutToMainMenu() {
		Vector3 targetPosition = new Vector3 (-49.9f, 30f, 74.7f);
		Vector3 targetEuler = new Vector3 (10.1f, 105.9f, 0f);
		
		transform.position = Vector3.Lerp (transform.position, targetPosition, Time.deltaTime);
		transform.eulerAngles = Vector3.Lerp (transform.eulerAngles, targetEuler, Time.deltaTime);
	}

	void NewGameCameraAngle() {
		Vector3 targetPosition = new Vector3 (-8f, 20f, -36f);
		Vector3 targetEuler = new Vector3 (14f, 11f, 0f);

		transform.position = Vector3.Lerp (transform.position, targetPosition, Time.deltaTime);
		transform.eulerAngles = Vector3.Lerp (transform.eulerAngles, targetEuler, Time.deltaTime);
	}

	void LoadGameCameraAngle() {
		Vector3 targetPosition = new Vector3 (-14f, 9f, 9f);
		Vector3 targetEuler = new Vector3 (14f, 111f, 0f);

		transform.position = Vector3.Lerp (transform.position, targetPosition, Time.deltaTime);
		transform.eulerAngles = Vector3.Lerp (transform.eulerAngles, targetEuler, Time.deltaTime);
	}

	IEnumerator CameraMoveInCoroutine(float time) {
		yield return new WaitForSeconds (time);
		MoveOutOfMainMenu = false;
	}

	IEnumerator CameraMoveOutCoroutine(float time) {
		yield return new WaitForSeconds (time);
		MoveBackToMainMenu = false;
	}

	IEnumerator NewGameCameraMoveCoroutine(float time) {
		yield return new WaitForSeconds (time);
		NewGameAngle = false;
	}

	IEnumerator LoadGameCameraMoveCoroutine(float time) {
		yield return new WaitForSeconds (time);
		LoadGameAngle = false;
	}
}
