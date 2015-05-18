using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InGameMenuController : MonoBehaviour {

	public RectTransform playingGameMenuRect;
	bool inGameMenuIsOpen = false;
	bool paused;

	GameObject player;
	GameObject worldController;
	GameObject HUDCanvas;

	RectTransform saveGameMenuRect;
	public RectTransform loadGameMenuRect;
	public RectTransform controlsMenuRect;

	void Start() {
		playingGameMenuRect = transform.FindChild ("InGameMenu").GetComponent<RectTransform> ();

		saveGameMenuRect = transform.FindChild ("SaveGameMenuRawImage").GetComponent<RectTransform> ();
		loadGameMenuRect = transform.FindChild ("LoadGameMenuRawImage").GetComponent<RectTransform> ();
		controlsMenuRect = transform.FindChild ("ControlsRawImage").GetComponent<RectTransform> ();
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if(inGameMenuIsOpen == false) {
				playingGameMenuRect.anchoredPosition = new Vector3(0f, 0f, 0f);
				GameObject.Find("Main Camera").GetComponent<NavalCameraMovement>().enabled = false;
				Time.timeScale = 0;
				inGameMenuIsOpen = true;
			} else if (inGameMenuIsOpen == true) {
				playingGameMenuRect.anchoredPosition = new Vector3(2000f, 0f, 0f);
				GameObject.Find("Main Camera").GetComponent<NavalCameraMovement>().enabled = true;
				Time.timeScale = 1;
				inGameMenuIsOpen = false;
			}
		}
	}

	public void InGameMenuButtons(string command) {
		if (command == "Resume") {
			playingGameMenuRect.anchoredPosition = new Vector3(2000f, 0f, 0f);
			GameObject.Find("Main Camera").GetComponent<NavalCameraMovement>().enabled = true;
			Time.timeScale = 1;
			inGameMenuIsOpen = false;
		}
		if (command == "SaveGame") {
			saveGameMenuRect.anchoredPosition = new Vector3(0f, 0f, 0f);
		}
		if (command == "LoadGame") {
			loadGameMenuRect.anchoredPosition = new Vector3(0f, 0f, 0f);
		}
		if (command == "Controls") {
			controlsMenuRect.anchoredPosition = new Vector3(0f, 0f, 0f);
		}
		if (command == "MainMenu") {
			player = GameObject.Find("Player");
			worldController = GameObject.Find("_WorldController");
			HUDCanvas = GameObject.Find("HUDCanvas");

			Destroy(player);
			Destroy(worldController);
			Destroy(HUDCanvas);

			Application.LoadLevel("MainMenu");
		}
		if (command == "ExitGame") {
			Application.Quit();
		}
		if (command == "Back") {
			saveGameMenuRect.anchoredPosition = new Vector3(-1500f, 0f, 0f);
			loadGameMenuRect.anchoredPosition = new Vector3(-1500f, 0f, 0f);
			controlsMenuRect.anchoredPosition = new Vector3(-1500f, 0f, 0f);
		}
	}

	IEnumerator TimerCoroutine() {
		yield return new WaitForSeconds (2f);
		//Time.timeScale = 0;
	}
}
