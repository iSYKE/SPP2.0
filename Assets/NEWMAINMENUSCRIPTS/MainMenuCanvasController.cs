using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuCanvasController : MonoBehaviour {

	Animator gameTitleAnim;

	GameObject playButtonObj;
	GameObject optionsButtonObj;
	GameObject controlButtonObj;
	GameObject shiplopediaButtonObj;
	GameObject creditsButtonObj;
	GameObject exitButtonObj;

	GameObject newGameButtonObj; 
	GameObject loadGameButtonObj;
	GameObject backButtonObj;

	GameObject startGameButtonObj;
	GameObject backButton2Obj;
	GameObject backButton3Obj;

	Animator OptionsMenuAnim;
	Animator ControlsMenuAnim;
	Animator ShiplopediaMenuAnim;
	Animator CreditsMenuAnim;
	Animator LoadGameMenuAnim;

	Text optionsButtonText;
	Text controlsButtonText;
	Text shiplopediaButtonText;
	Text creditsButtonText;

	public Texture[] shipPics;
	public RawImage shipPicHolder;
	Text shipTitleText;
	Text hullStrengthText;
	Text cargoVolumeText;
	Text maxCrewText;
	Text maxSpeedText;
	Text maxArmamentText;
	int maxGuns;

	public string activeResString;
	public Text activeResText;
	public Text resOption1Text;
	public Text resOption2Text;
	public Text resOption3Text;
	public Text resOption4Text;
	bool fullScreen = true;

	Color ambientDarkest = new Color (34f, 34f, 34f, 255f);
	Color ambientLightest = new Color (210f, 210f, 210f, 255f);

	bool OptionsMenuOut = false;
	bool ControlsMenuOut = false;
	bool ShiplopediaMenuOut = false;
	bool CreditsMenuOut = false;

	MainCameraMainMenu mainCameraMainMenu;
	public Ship selectedViewingShip;

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

		startGameButtonObj = transform.FindChild ("MainMenuButtonHolder/NewGameButtonHolder/StartGameButton").transform.gameObject;
		backButton2Obj = transform.FindChild ("MainMenuButtonHolder/NewGameButtonHolder/Back2Button").transform.gameObject;

		backButton3Obj = transform.FindChild ("MainMenuButtonHolder/LoadGameButtonHolder/Back3Button").transform.gameObject;

		optionsButtonText = transform.FindChild ("MainMenuButtonHolder/OptionsButton/Text").transform.gameObject.GetComponent<Text>();
		controlsButtonText = transform.FindChild ("MainMenuButtonHolder/ControlsButton/Text").transform.gameObject.GetComponent<Text>();
		shiplopediaButtonText = transform.FindChild ("MainMenuButtonHolder/ShiplopediaButton/Text").transform.gameObject.GetComponent<Text> ();
		creditsButtonText = transform.FindChild ("MainMenuButtonHolder/CreditsButton/Text").transform.gameObject.GetComponent<Text> ();

		OptionsMenuAnim = transform.FindChild ("RollOutMenuHolder/OptionsRawImage").transform.gameObject.GetComponent<Animator>();
		ControlsMenuAnim = transform.FindChild ("RollOutMenuHolder/ControlsRawImage").transform.gameObject.GetComponent<Animator>();
		ShiplopediaMenuAnim = transform.FindChild ("RollOutMenuHolder/ShiplopediaRawImage").transform.gameObject.GetComponent<Animator> ();
		CreditsMenuAnim = transform.FindChild ("CreditsRawImage").transform.gameObject.GetComponent<Animator> ();
		LoadGameMenuAnim = transform.FindChild ("RollOutMenuHolder/LoadGameRawImage").transform.gameObject.GetComponent<Animator> ();

		shipPicHolder = transform.FindChild ("RollOutMenuHolder/ShiplopediaRawImage/BoatRawImage").transform.gameObject.GetComponent<RawImage> ();
		shipTitleText = transform.FindChild ("RollOutMenuHolder/ShiplopediaRawImage/BoatRawImage/TitleText").transform.gameObject.GetComponent<Text> ();
		hullStrengthText = transform.FindChild ("RollOutMenuHolder/ShiplopediaRawImage/BoatRawImage/HullStrengthText").transform.gameObject.GetComponent<Text> ();
		cargoVolumeText = transform.FindChild ("RollOutMenuHolder/ShiplopediaRawImage/BoatRawImage/CargoVolumeText").transform.gameObject.GetComponent<Text> ();
		maxCrewText = transform.FindChild ("RollOutMenuHolder/ShiplopediaRawImage/BoatRawImage/MaxCrewText").transform.gameObject.GetComponent<Text> ();
		maxSpeedText = transform.FindChild ("RollOutMenuHolder/ShiplopediaRawImage/BoatRawImage/MaxSpeedText").transform.gameObject.GetComponent<Text> ();
		maxArmamentText = transform.FindChild ("RollOutMenuHolder/ShiplopediaRawImage/BoatRawImage/MaxArmamentText").transform.gameObject.GetComponent<Text> ();

		activeResText = transform.FindChild ("RollOutMenuHolder/OptionsRawImage/ResolutionHolder/ResolutionPanel/ActiveResButton/ActiveResButtonText").transform.gameObject.GetComponent<Text> ();
		resOption1Text = transform.FindChild ("RollOutMenuHolder/OptionsRawImage/ResolutionHolder/ResolutionPanel/ActiveResButton/SecondResPanel/ResOption1Button/ResOption1Text").transform.gameObject.GetComponent<Text> ();
		resOption2Text = transform.FindChild ("RollOutMenuHolder/OptionsRawImage/ResolutionHolder/ResolutionPanel/ActiveResButton/SecondResPanel/ResOption2Button/ResOption2Text").transform.gameObject.GetComponent<Text> ();
		resOption3Text = transform.FindChild ("RollOutMenuHolder/OptionsRawImage/ResolutionHolder/ResolutionPanel/ActiveResButton/SecondResPanel/ResOption3Button/ResOption3Text").transform.gameObject.GetComponent<Text> ();
		resOption4Text = transform.FindChild ("RollOutMenuHolder/OptionsRawImage/ResolutionHolder/ResolutionPanel/ActiveResButton/SecondResPanel/ResOption4Button/ResOption4Text").transform.gameObject.GetComponent<Text> ();

		mainCameraMainMenu = Camera.main.GetComponent<MainCameraMainMenu> ();
	}

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
			if (OptionsMenuOut == false) {
				OptionsMenuAnim.SetTrigger("OptionsMenuMoveOn");
				optionsButtonObj.GetComponent<Animator>().SetTrigger("OptionsButtonSelected");
				optionsButtonText.GetComponent<Text>().color = Color.red;

				playButtonObj.GetComponent<Button>().interactable = false;
				controlButtonObj.GetComponent<Button>().interactable = false;
				shiplopediaButtonObj.GetComponent<Button>().interactable = false;
				creditsButtonObj.GetComponent<Button>().interactable = false;

				OptionsMenuOut = true;
			} else if (OptionsMenuOut == true) {
				OptionsMenuAnim.SetTrigger("OptionsMenuMoveOff");
				optionsButtonText.GetComponent<Text>().color = Color.black;
				optionsButtonObj.GetComponent<Animator>().SetTrigger("OptionsButtonDeselected");

				playButtonObj.GetComponent<Button>().interactable = true;
				controlButtonObj.GetComponent<Button>().interactable = true;
				shiplopediaButtonObj.GetComponent<Button>().interactable = true;
				creditsButtonObj.GetComponent<Button>().interactable = true;

				OptionsMenuOut = false;
			}
		}

		if (buttonCommand == "Controls") {
			if (ControlsMenuOut == false) {
				ControlsMenuAnim.SetTrigger("ControlsMenuMoveOn");
				controlButtonObj.GetComponent<Animator>().SetTrigger("ControlsButtonSelected");
				controlsButtonText.GetComponent<Text>().color = Color.red;

				playButtonObj.GetComponent<Button>().interactable = false;
				optionsButtonObj.GetComponent<Button>().interactable = false;
				shiplopediaButtonObj.GetComponent<Button>().interactable = false;
				creditsButtonObj.GetComponent<Button>().interactable = false;

				ControlsMenuOut = true;
			} else if (ControlsMenuOut == true) {
				ControlsMenuAnim.SetTrigger("ControlsMenuMoveOff");
				controlButtonObj.GetComponent<Animator>().SetTrigger("ControlsButtonDeselected");
				controlsButtonText.GetComponent<Text>().color = Color.black;

				playButtonObj.GetComponent<Button>().interactable = true;
				optionsButtonObj.GetComponent<Button>().interactable = true;
				shiplopediaButtonObj.GetComponent<Button>().interactable = true;
				creditsButtonObj.GetComponent<Button>().interactable = true;

				ControlsMenuOut = false;
			}
		}

		if (buttonCommand == "Shiplopedia") {
			if(ShiplopediaMenuOut == false) {
				ShiplopediaMenuAnim.SetTrigger("ShiplopediaMenuMoveOn");
				shiplopediaButtonObj.GetComponent<Animator>().SetTrigger("ShiplopediaButtonSelected");
				shiplopediaButtonText.GetComponent<Text>().color = Color.red;

				playButtonObj.GetComponent<Button>().interactable = false;
				optionsButtonObj.GetComponent<Button>().interactable = false;
				controlButtonObj.GetComponent<Button>().interactable = false;
				creditsButtonObj.GetComponent<Button>().interactable = false;

				ShiplopediaMenuOut = true;

			} else if (ShiplopediaMenuOut == true) {
				ShiplopediaMenuAnim.SetTrigger("ShiplopediaMenuMoveOff");
				shiplopediaButtonObj.GetComponent<Animator>().SetTrigger("ShiplopediaButtonDeselected");
				shiplopediaButtonText.GetComponent<Text>().color = Color.black;

				playButtonObj.GetComponent<Button>().interactable = true;
				optionsButtonObj.GetComponent<Button>().interactable = true;
				controlButtonObj.GetComponent<Button>().interactable = true;
				creditsButtonObj.GetComponent<Button>().interactable = true;

				ShiplopediaMenuOut = false;
			}
		}

		if (buttonCommand == "Credits") {
			if (CreditsMenuOut == false) {
				CreditsMenuAnim.SetTrigger("CreditsMenuMoveOn");
				creditsButtonObj.GetComponent<Animator>().SetTrigger("CreditsButtonSelected");
				//creditsButtonText.GetComponent<Text>().color = Color.red;
				creditsButtonText.color = Color.red;

				playButtonObj.GetComponent<Button>().interactable = false;
				optionsButtonObj.GetComponent<Button>().interactable = false;
				controlButtonObj.GetComponent<Button>().interactable = false;
				shiplopediaButtonObj.GetComponent<Button>().interactable = false;

				CreditsMenuOut = true;
			} else if (CreditsMenuOut == true) {
				CreditsMenuAnim.SetTrigger("CreditsMenuMoveOff");
				creditsButtonObj.GetComponent<Animator>().SetTrigger("CreditsButtonDeselected");
				//creditsButtonText.GetComponent<Text>().color = Color.red;
				creditsButtonText.color = Color.black;
				
				playButtonObj.GetComponent<Button>().interactable = true;
				optionsButtonObj.GetComponent<Button>().interactable = true;
				controlButtonObj.GetComponent<Button>().interactable = true;
				shiplopediaButtonObj.GetComponent<Button>().interactable = true;
				
				CreditsMenuOut = false;
			}
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

				startGameButtonObj.GetComponent<Animator>().SetTrigger("StartGameButtonMoveOn");
				backButton2Obj.GetComponent<Animator>().SetTrigger("BackButton2MoveOn");
			}
		}

		if (buttonCommand == "LoadGame") {
			if (mainCameraMainMenu.MoveOutOfMainMenu == false) {
				mainCameraMainMenu.LoadGameAngle = true;

				backButtonObj.GetComponent<Animator>().SetTrigger("BackButtonMoveOff");
				loadGameButtonObj.GetComponent<Animator>().SetTrigger("LoadGameButtonMoveOff");
				newGameButtonObj.GetComponent<Animator>().SetTrigger("NewGameButtonMoveOff");

				backButton3Obj.GetComponent<Animator>().SetTrigger("BackButton3MoveOn");
				LoadGameMenuAnim.SetTrigger("LoadGameMenuOn");
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

	public void StartGameButtonCall(string buttonCommand) {
		if (mainCameraMainMenu.NewGameAngle == false) {
			if (buttonCommand == "StartGame") {

			}

			if (buttonCommand == "Back") {
					mainCameraMainMenu.MoveOutOfMainMenu = true;

					startGameButtonObj.GetComponent<Animator>().SetTrigger("StartGameButtonMoveOff");
					backButton2Obj.GetComponent<Animator>().SetTrigger("BackButton2MoveOff");

					newGameButtonObj.GetComponent<Animator>().SetTrigger("NewGameButtonMoveOn");
					loadGameButtonObj.GetComponent<Animator>().SetTrigger("LoadGameButtonMoveOn");
					backButtonObj.GetComponent<Animator>().SetTrigger("BackButtonMoveOn");
			}
		}
	}

	public void LoadGameButtonCall(string buttonCommand) {
		if (mainCameraMainMenu.LoadGameAngle == false) {
			if (buttonCommand == "Back") {
				mainCameraMainMenu.MoveOutOfMainMenu = true;

				backButton3Obj.GetComponent<Animator> ().SetTrigger ("BackButton3MoveOff");
				LoadGameMenuAnim.SetTrigger("LoadGameMenuOff");

				newGameButtonObj.GetComponent<Animator> ().SetTrigger ("NewGameButtonMoveOn");
				loadGameButtonObj.GetComponent<Animator> ().SetTrigger ("LoadGameButtonMoveOn");
				backButtonObj.GetComponent<Animator> ().SetTrigger ("BackButtonMoveOn");
			}
		}
	}

	public void shiplopediaActions(string ship) {
		selectedViewingShip = GameObject.Find("_WorldController").GetComponent<ShipList>().gameShips.Find( x => x.shipName == ship);

		shipPicHolder.color = new Color (255f, 255f, 255f, 255f);
		shipPicHolder.texture = shipPics [selectedViewingShip.shipID];

		shipTitleText.text = selectedViewingShip.shipName;
		hullStrengthText.text = selectedViewingShip.shipMaxHull + " Units";
		cargoVolumeText.text = selectedViewingShip.shipCargoVolume.ToString () + " m\x00b3";
		maxCrewText.text = selectedViewingShip.shipMaxCrew.ToString () + " Men";
		maxSpeedText.text = "8 Knots";
		maxGuns = selectedViewingShip.shipMaxGunsLeft + selectedViewingShip.shipMaxGunsRight;
		maxArmamentText.text = maxGuns.ToString () + " Cannons";
	}

	public void ResolutionButtonCalls(string resolution) {
		if (resolution == "1920x1080") {
			activeResText.text = resOption1Text.text;
			Screen.SetResolution(1920, 1080, fullScreen);
		}
		if (resolution == "1600x1024") {
			activeResText.text = resOption2Text.text;
			Screen.SetResolution(1600, 1024, fullScreen);
		}
		if (resolution == "1360x768") {
			activeResText.text = resOption3Text.text;
			Screen.SetResolution(1360, 768, fullScreen);
		}
		if (resolution == "1024x768") {
			activeResText.text = resOption4Text.text;
			Screen.SetResolution(1024, 768, fullScreen);
		}
	}

	public void FullScreenToggle(bool change) {
		Screen.fullScreen = !Screen.fullScreen;
		if (fullScreen) {
			fullScreen = false;
		} else if (fullScreen == false) {
			fullScreen = true;
		}
	}

	public void BrightnessSet(float value) {


		RenderSettings.ambientLight = Color.Lerp (ambientDarkest, ambientLightest, value);
	}
}