using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuController : MonoBehaviour {

	public GameObject playButtonObj;
	public GameObject exitButtonObj;
	public GameObject optionsButtonObj;
	public GameObject controlButtonObj;
	public GameObject creditsButtonObj;
	public GameObject gameTitle;

	public GameObject player;

	public GameObject backButtonObj;
	public GameObject startGameButtonObj;

	public GameObject ShipSelectionMenuObj;
	public GameObject WeaponSelectionMenuObj;

	public RectTransform optionsImageRect;
	public RectTransform controlsImageRect;
	public RectTransform creditsImageRect;
	//public RectTransform shipSecletionImageRect;

	public Text playText;
	public Text optionsText;
	public Text controlText;
	public Text creditsText;
	public Text exitText;

	bool optionsPanel = false;
	bool controlsPanel = false;
	bool creditsPanel = false;

	float currentTime;
	float targetTime;

	ExitMainMenu exitMainMenu;
	ReturnToMainMenu returnToMainMenu;

	//PlayerHealth playerHealth;
	PlayerControl playerControl;
	ManualCannonFire manualCannonFire;

	void Awake() {
		playButtonObj = GameObject.Find ("MainMenuCanvas/MainMenuButtons/Play Button");
		exitButtonObj = GameObject.Find ("MainMenuCanvas/MainMenuButtons/Exit Game Button");
		optionsButtonObj = GameObject.Find ("MainMenuCanvas/MainMenuButtons/Options Button");
		controlButtonObj = GameObject.Find ("MainMenuCanvas/MainMenuButtons/Controls Button");
		creditsButtonObj = GameObject.Find ("MainMenuCanvas/MainMenuButtons/Credits Button");
		gameTitle = GameObject.Find ("MainMenuCanvas/GameTitle");

		backButtonObj = GameObject.Find ("MainMenuCanvas/ShipSelectionMenuButtons/Back Button");
		startGameButtonObj = GameObject.Find ("MainMenuCanvas/ShipSelectionMenuButtons/StartGameButton");

		ShipSelectionMenuObj = GameObject.Find ("MainMenuCanvas/ShipSelectionMenu");
		WeaponSelectionMenuObj = GameObject.Find ("MainMenuCanvas/WeaponSelectionMenu");

		optionsImageRect = GameObject.Find ("MainMenuCanvas/OptionsRawImage").GetComponent<RectTransform> ();
		controlsImageRect = GameObject.Find ("MainMenuCanvas/ControlsRawImage").GetComponent<RectTransform> ();
		creditsImageRect = GameObject.Find ("MainMenuCanvas/CreditsRawImage").GetComponent<RectTransform> ();

		playText = GameObject.Find ("MainMenuCanvas/MainMenuButtons/Play Button/PlayButtonText").GetComponent<Text> ();
		optionsText = GameObject.Find ("MainMenuCanvas/MainMenuButtons/Options Button/OptionsButtonText").GetComponent<Text> ();
		controlText = GameObject.Find ("MainMenuCanvas/MainMenuButtons/Controls Button/ControlsButtonText").GetComponent<Text> ();
		creditsText = GameObject.Find ("MainMenuCanvas/MainMenuButtons/Credits Button/CreditsButtonText").GetComponent<Text> ();
		exitText = GameObject.Find ("MainMenuCanvas/MainMenuButtons/Exit Game Button/ExitGameButtonText").GetComponent<Text> ();
	}

	public void MainPanelChange (string panelToChangeTo) 
	{
		if (panelToChangeTo == "Play") {
			ResetTheMenu();
			RemoveMainMenu();

			foreach( Transform child in GameObject.Find("Player").transform.FindChild("Sloop/Nodes/LeftGuns").transform ){
				
				if(child.GetComponentInChildren<CannonFire>()){
					child.GetComponentInChildren<CannonFire>().doFire = true;
					//Debug.Log(child.name + " FIRED!!!");
				}
			}

		}
		if (panelToChangeTo == "Options") {
			if(optionsPanel == false) {
				optionsPanel = true;
				optionsImageRect.anchoredPosition = new Vector3(450f, 20f, 0f);
				controlButtonObj.GetComponent<Button>().interactable = false;
				creditsButtonObj.GetComponent<Button>().interactable = false;
				playButtonObj.GetComponent<Button>().interactable = false;
			} else if(optionsPanel == true) {
				optionsPanel = false;
				optionsImageRect.anchoredPosition = new Vector3(2000f, 20f, 0f);
				optionsText.GetComponent<Text>().color = Color.black;
				controlButtonObj.GetComponent<Button>().interactable = true;
				creditsButtonObj.GetComponent<Button>().interactable = true;
				playButtonObj.GetComponent<Button>().interactable = true;
			}
		}
		if (panelToChangeTo == "Controls") {
			if(controlsPanel == false) {
				controlsPanel = true;
				controlsImageRect.anchoredPosition = new Vector3(450f, 20f, 0f);
				controlText.GetComponent<Text>().color = Color.red;
				optionsButtonObj.GetComponent<Button>().interactable = false;
				creditsButtonObj.GetComponent<Button>().interactable = false;
				playButtonObj.GetComponent<Button>().interactable = false;
			} else if(controlsPanel == true) {
				controlsPanel = false;
				controlsImageRect.anchoredPosition = new Vector3(2000f, 20f, 0f);
				controlText.GetComponent<Text>().color = Color.black;
				optionsButtonObj.GetComponent<Button>().interactable = true;
				creditsButtonObj.GetComponent<Button>().interactable = true;
				playButtonObj.GetComponent<Button>().interactable = true;
			}
		}
		if (panelToChangeTo == "Credits") {
			if(creditsPanel == false){
				creditsPanel = true;
				creditsImageRect.anchoredPosition = new Vector3(-10f, 10f, 0f);
				creditsText.GetComponent<Text>().color = Color.red;
				controlButtonObj.GetComponent<Button> ().interactable = false;
				optionsButtonObj.GetComponent<Button> ().interactable = false;
				playButtonObj.GetComponent<Button>().interactable = false;
			} else if(creditsPanel == true){
				creditsPanel = false;
				creditsImageRect.anchoredPosition = new Vector3(2000f, 10f, 0f);
				creditsText.GetComponent<Text>().color = Color.black;
				controlButtonObj.GetComponent<Button>().interactable = true;
				optionsButtonObj.GetComponent<Button>().interactable = true;
				playButtonObj.GetComponent<Button>().interactable = true;
			}
		}
		if (panelToChangeTo == "Exit") {
			exitText.GetComponent<Text>().color = Color.red;
			Application.Quit();
		}
		if (panelToChangeTo == "Back") {
			ReturningToMainMenu();
		}
		if (panelToChangeTo == "StartGame") {
			Application.LoadLevel("scene01");
		}
	}

	void RemoveMainMenu()
	{
		exitMainMenu = GetComponent<ExitMainMenu> ();
		exitMainMenu.enabled = true;

		gameTitle.GetComponent<Animator> ().SetTrigger ("GameTitleSlideOff");

		exitButtonObj.GetComponent<Animator> ().SetTrigger ("ExitButtonSlideOff");
		creditsButtonObj.GetComponent<Animator> ().SetTrigger ("CreditsButtonSlideOff");
		controlButtonObj.GetComponent<Animator> ().SetTrigger ("ControlsButtonSlideOff");
		optionsButtonObj.GetComponent<Animator> ().SetTrigger ("OptionsButtonSlideOff");
		playButtonObj.GetComponent<Animator> ().SetTrigger ("Pressed");

		backButtonObj.GetComponent<Animator> ().SetTrigger ("BackButtonSlideIn");
		startGameButtonObj.GetComponent<Animator> ().SetTrigger ("StartGameSlideIn");

		ShipSelectionMenuObj.GetComponent<Animator> ().SetTrigger ("MenuSlideIn");
	}

	void ReturningToMainMenu()
	{
		returnToMainMenu = GetComponent<ReturnToMainMenu> ();
		returnToMainMenu.enabled = true;
		exitMainMenu = GetComponent<ExitMainMenu> ();
		exitMainMenu.enabled = false;

		gameTitle.GetComponent<Animator> ().SetTrigger ("GameTitleSlideIn");

		startGameButtonObj.GetComponent<Animator> ().SetTrigger ("StartGameSlideOff");
		backButtonObj.GetComponent<Animator> ().SetTrigger ("BackButtonSlideOff");

		ShipSelectionMenuObj.GetComponent<Animator> ().SetTrigger ("MenuSlideOff");
		WeaponSelectionMenuObj.GetComponent<Animator> ().SetTrigger ("WeaponMenuSlideOff");

		playButtonObj.GetComponent<Animator> ().SetTrigger ("PlayButtonSlideIn");
		optionsButtonObj.GetComponent<Animator> ().SetTrigger ("OptionsButtonSlideIn");
		controlButtonObj.GetComponent<Animator> ().SetTrigger ("ControlsButtonSlideIn");
		creditsButtonObj.GetComponent<Animator> ().SetTrigger ("CreditsButtonSlideIn");
		exitButtonObj.GetComponent<Animator> ().SetTrigger ("ExitButtonSlideIn");
	}

	void ResetTheMenu()
	{
		returnToMainMenu = GetComponent<ReturnToMainMenu> ();
		returnToMainMenu.enabled = false;
	}
}