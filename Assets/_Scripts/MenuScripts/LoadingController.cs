using UnityEngine;
using System.Collections;

public class LoadingController : MonoBehaviour {

	public Canvas Canvas1;
	public Canvas Canvas2;
	public Canvas Canvas3;

	int randomNumber;

	void Awake() {
		Canvas1 = GameObject.Find ("Canvas1").GetComponent<Canvas> ();
		Canvas2 = GameObject.Find ("Canvas2").GetComponent<Canvas> ();
		Canvas3 = GameObject.Find ("Canvas3").GetComponent<Canvas> ();
	}

	void Update () {
		randomNumber = Random.Range (1, 3);

		if (randomNumber == 1)
			Canvas1.GetComponent<Canvas> ().enabled = true;
		if (randomNumber == 2)
			Canvas2.GetComponent<Canvas> ().enabled = true;
		if (randomNumber == 3)
			Canvas3.GetComponent<Canvas> ().enabled = true;

		StartCoroutine (Loading ());
	}

	IEnumerator Loading() {
		yield return new WaitForSeconds(10f);
		Application.LoadLevel ("MainMenu");
	}
}
