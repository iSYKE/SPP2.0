using UnityEngine;
using System.Collections;

public class InGameMenuController : MonoBehaviour {

	public RectTransform playingGameMenuRect;
	bool inGameMenuIsOpen = false;
	bool paused;
	
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
			Application.LoadLevel("MainMenu");
		}

		if (command == "ExitGame") {
			Application.Quit();
		}
	}
}
