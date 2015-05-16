using UnityEngine;
using System.Collections;

public class InGameMenuController : MonoBehaviour {

	public RectTransform playingGameMenuRect;
	bool inGameMenuIsOpen = false;
	bool paused;

	GameObject player;
	GameObject worldController;
	GameObject HUDCanvas;

	void Start() {
		playingGameMenuRect = transform.FindChild ("InGameMenu").GetComponent<RectTransform> ();
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
		if (command == "MainMenu") {
			player = GameObject.Find("Player");
			worldController = GameObject.Find("_WorldController");
			HUDCanvas = GameObject.Find("HUDCanvas");

			Destroy(player);
			Destroy(worldController);
			Destroy(HUDCanvas);

			Application.LoadLevel("MainMenu");

			//StartCoroutine(LoadMainMenuCoroutine());
		}

		if (command == "ExitGame") {
			Application.Quit();
		}
	}

	IEnumerator LoadMainMenuCoroutine() {
		yield return new WaitForSeconds (.5f);
		Application.LoadLevel("MainMenu");
	}
}
