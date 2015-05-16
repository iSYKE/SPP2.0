using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDController : MonoBehaviour {

	public GameObject HUDHolder;
	public GameObject player;
	public GameObject WorldController;

	public Text CharHealthText;
	public Text PortActRangeText;
	public Text StarboardActRangeText;
	public Text LeftCannonLoadText;
	public Text RightCannonLoadText;
	public Text SpeedText;
	public Text WindSpeedText;

	public RawImage ShipOnFireRawImage;
	public RawImage CompassNeedleRawImage;

	public RectTransform NeedleTransform;

	public float speed;
	bool IsLevelLoadedUp = false;

	CharacterShipStats characterShipStats;
	PlayerControl playerControl;
	WindSim windSim;

	void OnLevelWasLoaded(int level) {
		if (level == 1) {
			// Sets Location of the HUD
			HUDHolder = transform.FindChild("HUDHolder").transform.gameObject;
			HUDHolder.GetComponent<RectTransform>().anchoredPosition = new Vector2(20f, 20f);

			// Sets the Players Health
			player = GameObject.Find("Player");
			characterShipStats = player.GetComponent<CharacterShipStats>();
			CharHealthText = transform.FindChild("HUDHolder/HullUI/HealthUIText").transform.gameObject.GetComponent<Text>();
			CharHealthText.text = characterShipStats.hullHealth + "/" + characterShipStats.maxHullHealth;

			// Sail UI Code here

			// Crew UI Code here

			// Sets The Players Port Side Cannon Range To Default
			playerControl = player.GetComponent<PlayerControl>();
			PortActRangeText = transform.FindChild("HUDHolder/PortFiringAngle/PortActualRangeText").transform.gameObject.GetComponent<Text>();
			PortActRangeText.text = playerControl.portAimRange.ToString() + " M";

			// Sets The Players Starboard Side Cannon Range To Default
			StarboardActRangeText = transform.FindChild("HUDHolder/StarboardFiringAngle/StarboardActualRangeText").transform.gameObject.GetComponent<Text>();
			StarboardActRangeText.text = playerControl.starBoardAimRange.ToString() + "M";

			// World Map Code Here

			// Ship Stats Code Here

			// Char Stats Code Here

			// Sets The On Fire Indicator To Transparent On Load
			ShipOnFireRawImage = transform.FindChild("HUDHolder/ShipUI/ShipOnFireRawImage").transform.gameObject.GetComponent<RawImage>();
			ShipOnFireRawImage.color = new Color(255f, 111f, 0f, 0f);

			// Sets The Cannons Reloaded On Port Side To Default
			LeftCannonLoadText = transform.FindChild("HUDHolder/CannonsLoadedIndicator/LeftCannonsLoaded").transform.gameObject.GetComponent<Text>();
			LeftCannonLoadText.text = (characterShipStats.gunMaxCountLeft - characterShipStats.gunCountUnloadedLeft).ToString() + "/"  + characterShipStats.gunMaxCountLeft.ToString();

			// Sets The Cannons Reloaded On Starboard Side To Default
			RightCannonLoadText = transform.FindChild("HUDHolder/CannonsLoadedIndicator/RightCannonsLoaded").transform.gameObject.GetComponent<Text>();
			RightCannonLoadText.text = (characterShipStats.gunMaxCountRight - characterShipStats.gunCountUnloadedRight).ToString() + "/" + characterShipStats.gunMaxCountRight.ToString();

			// Compass Code
			NeedleTransform = transform.FindChild("HUDHolder/CompassUI/CompassNeedleRawImage").transform.gameObject.GetComponent<RectTransform>();
			NeedleTransform.eulerAngles = new Vector3 (0f, 0f, -player.GetComponent<Transform>().eulerAngles.y);

			// Sets The Players Speed To Default
			SpeedText = transform.FindChild("HUDHolder/RightSideIndicators/SpeedText").transform.gameObject.GetComponent<Text>();
			speed = player.GetComponent<Rigidbody>().velocity.magnitude;
			speed /= 1.94f;

			int speedInt = (int)speed;
			SpeedText.text = speedInt.ToString() + " Knots";

			// Wind Speed Code
			WorldController = GameObject.Find("_WorldController");
			windSim = WorldController.GetComponent<WindSim>();

			WindSpeedText = transform.FindChild("HUDHolder/RightSideIndicators/WindSpeedText").transform.gameObject.GetComponent<Text>();
			WindSpeedText.text = windSim.windStrength.ToString() + " Knots";

			IsLevelLoadedUp = true;
		}
	}

	void Awake() {
		DontDestroyOnLoad ( transform.gameObject );
	}

	void Update () {
		if (IsLevelLoadedUp) {
			PlayersHealth ();
			//SailStatus ();
			//CrewStatus ();
			UpdateCannonRanges ();
			UpdateCannonsReloaded ();
			UpdateCompassDirection ();
			UpdatePlayerSpeed ();
			UpdateWindSpeed ();
		}
	}

	void PlayersHealth() {
		CharHealthText.text = characterShipStats.hullHealth + "/" + characterShipStats.maxHullHealth;
		if ((characterShipStats.hullHealth / characterShipStats.maxHullHealth) * 100 <= 50) {
			ShipOnFireRawImage.color = new Color(255f, 111f, 0f, 255f);
		} else if ((characterShipStats.hullHealth / characterShipStats.maxHullHealth) * 100 >= 50) {
			ShipOnFireRawImage.color = new Color(255f, 111f, 0f, 0f);
		}
	}

//	void SailStatus() {
//
//	}

//	void CrewStatus() {
//
//	}

	void UpdateCannonRanges() {
		PortActRangeText.text = playerControl.portAimRange.ToString() + " M";
		StarboardActRangeText.text = playerControl.starBoardAimRange.ToString() + "M";
	}

	void UpdateCannonsReloaded() {
		LeftCannonLoadText.text = (characterShipStats.gunMaxCountLeft - characterShipStats.gunCountUnloadedLeft).ToString() + "/"  + characterShipStats.gunMaxCountLeft.ToString();
		RightCannonLoadText.text = (characterShipStats.gunMaxCountRight - characterShipStats.gunCountUnloadedRight).ToString() + "/" + characterShipStats.gunMaxCountRight.ToString();
	}

	void UpdateCompassDirection(){
		NeedleTransform.eulerAngles = new Vector3 (0f, 0f, -player.GetComponent<Transform>().eulerAngles.y);
	}

	void UpdatePlayerSpeed(){
		speed = player.GetComponent<Rigidbody>().velocity.magnitude;
		speed /= 1.94f;
		
		int speedInt = (int)speed;
		SpeedText.text = speedInt.ToString() + " Knots";
	}

	void UpdateWindSpeed(){
		WindSpeedText.text = windSim.windStrength.ToString() + " Knots";
	}

	public void HUDButtonsControls (string ButtonCommand) {
		if (ButtonCommand == "PortUp") {
			if(playerControl.portAimRange < 375f) {
				playerControl.portAimRange += 25f;
			} else { playerControl.portAimRange = 400f; }
		}

		if (ButtonCommand == "PortDown") {
			if(playerControl.portAimRange > 50f) {
				playerControl.portAimRange -= 25f;
			} else { playerControl.portAimRange = 50f; }
		}

		if (ButtonCommand == "StarboardUp") {
			if (playerControl.starBoardAimRange < 375f) {
				playerControl.starBoardAimRange += 25f;
			} else { playerControl.starBoardAimRange = 400f; }
		}

		if (ButtonCommand == "StarboardDown") {
			if (playerControl.starBoardAimRange > 50f) {
				playerControl.starBoardAimRange -= 25f;
			} else { playerControl.starBoardAimRange = 50f; }
		}

		playerControl.RangeCannonsLeft ();
		playerControl.RangeCannonsRight ();
	}
}
