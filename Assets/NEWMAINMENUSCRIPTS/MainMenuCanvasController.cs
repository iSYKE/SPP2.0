using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuCanvasController : MonoBehaviour {

	public Animator gameTitleAnim;

	public GameObject playButtonObj;
	public GameObject optionsButtonObj;
	public GameObject controlButtonObj;
	public GameObject shiplopediaButtonObj;
	public GameObject creditsButtonObj;
	public GameObject exitButtonObj;

	public GameObject newGameButtonObj; 
	public GameObject loadGameButtonObj;
	public GameObject backButtonObj;

	MainCameraMainMenu mainCameraMainMenu;

	void Start () {
		playButtonObj = transform.FindChild ("MainMenuButtonHolder/PlayButton").transform.gameObject;
		optionsButtonObj = transform.FindChild ("MainMenuButtonHolder/OptionsButton").transform.gameObject;
		controlButtonObj = transform.FindChild ("MainMenuButtonHolder/ControlsButton").transform.gameObject;
		shiplopediaButtonObj = transform.FindChild ("MainMenuButtonHolder/ShiplopediaButton").transform.gameObject;
		creditsButtonObj = transform.FindChild ("MainMenuButtonHolder/CreditsButton").transform.gameObject;
		exitButtonObj = transform.FindChild ("MainMenuButtonHolder/ExitButton").transform.gameObject;

		gameTitleAnim = transform.FindChild ("GameTitleHolder").transform.gameObject.GetComponent<Animator> ();

		newGameButtonObj = transform.FindChild ("MainMenuButtonHolder/SecondaryMenuButtonHolder/NewGameButton").transform.gameObject;
		loadGameButtonObj = transform.FindChild ("MainMenuButtonHolder/SecondaryMenuButtonHolder/LoadGameButton").transform.gameObject; 
		backButtonObj = transform.FindChild ("MainMenuButtonHolder/SecondaryMenuButtonHolder/BackButton").transform.gameObject;

		mainCameraMainMenu = Camera.main.GetComponent<MainCameraMainMenu> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void MainMenuButtonCalls(string buttonCommand) {
		if (buttonCommand == "Play") {
			if(mainCameraMainMenu.MoveBackToMainMenu == false) {
				mainCameraMainMenu.MoveOutOfMainMenu = true;

				exitButtonObj.GetComponent<Animator>().SetTrigger("ExitButtonMoveOff");
				creditsButtonObj.GetComponent<Animator>().SetTrigger("CreditButtonMoveOff");
				shiplopediaButtonObj.GetComponent<Animator>().SetTrigger("ShiplopediaButtonMoveOff");
				controlButtonObj.GetComponent<Animator>().SetTrigger("ControlsButtonMoveOff");
				optionsButtonObj.GetComponent<Animator>().SetTrigger("OptionsButtonMoveOff");
				playButtonObj.GetComponent<Animator>().SetTrigger("PlayButtonMoveOff");

				gameTitleAnim.SetTrigger("GameTitleMoveOff");

				newGameButtonObj.GetComponent<Animator>().SetTrigger("NewGameButtonMoveOn");
				loadGameButtonObj.GetComponent<Animator>().SetTrigger("LoadGameButtonMoveOn");
				backButtonObj.GetComponent<Animator>().SetTrigger("BackButtonMoveOn");
			}
		}

		if (buttonCommand == "Options") {

		}

		if (buttonCommand == "Controls") {

		}

		if (buttonCommand == "Shiplopedia") {

		}

		if (buttonCommand == "Credits") {

		}

		if (buttonCommand == "Exit") {
			Application.Quit();
		}
	}

	public void SecondaryMenuButtonCalls(string buttonCommand) {
		if (buttonCommand == "NewGame") {
			if (mainCameraMainMenu.MoveOutOfMainMenu == false) {
				mainCameraMainMenu.NewGameAngle = true;

				backButtonObj.GetComponent<Animator>().SetTrigger("BackButtonMoveOff");
				loadGameButtonObj.GetComponent<Animator>().SetTrigger("LoadGameButtonMoveOff");
				newGameButtonObj.GetComponent<Animator>().SetTrigger("NewGameButtonMoveOff");
			}
		}

		if (buttonCommand == "LoadGame") {
			if (mainCameraMainMenu.MoveOutOfMainMenu == false) {
				mainCameraMainMenu.LoadGameAngle = true;

				backButtonObj.GetComponent<Animator>().SetTrigger("BackButtonMoveOff");
				loadGameButtonObj.GetComponent<Animator>().SetTrigger("LoadGameButtonMoveOff");
				newGameButtonObj.GetComponent<Animator>().SetTrigger("NewGameButtonMoveOff");
			}
		}

		if (buttonCommand == "Back") {
			if (mainCameraMainMenu.MoveOutOfMainMenu == false) {
				mainCameraMainMenu.MoveBackToMainMenu = true;

				backButtonObj.GetComponent<Animator>().SetTrigger("BackButtonMoveOff");
				loadGameButtonObj.GetComponent<Animator>().SetTrigger("LoadGameButtonMoveOff");
				newGameButtonObj.GetComponent<Animator>().SetTrigger("NewGameButtonMoveOff");

				gameTitleAnim.SetTrigger("GameTitleMoveOn");

				playButtonObj.GetComponent<Animator>().SetTrigger("PlayButtonMoveOn");
				optionsButtonObj.GetComponent<Animator>().SetTrigger("OptionsButtonMoveOn");
				controlButtonObj.GetComponent<Animator>().SetTrigger("ControlsButtonMoveOn");
				shiplopediaButtonObj.GetComponent<Animator>().SetTrigger("ShiplopediaButtonMoveOn");
				creditsButtonObj.GetComponent<Animator>().SetTrigger("CreditButtonMoveOn");
				exitButtonObj.GetComponent<Animator>().SetTrigger("ExitButtonMoveOn");
			}
		}
	}
}
